using System;
using System.Collections.Generic;

namespace MessageDecoder
{
    class Program
    {
        /*
        public static char cipher(char ch, int key)
        {
            if (!char.IsLetter(ch))
            {
                return ch;
            }
            char d = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - d) % 26) + d);
        }


        public static string Encipher(string input, int key)
        {
            string output = string.Empty;
            foreach (char ch in input)
            {
                output += cipher(ch, key);
            }
            return output;
        }

        public static string Decipher(string input, int key)
        {
            return Encipher(input, 26 - key);
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Type a string to encrypt:");
            string UserString = Console.ReadLine();

            Console.WriteLine("\n");

            Console.Write("Enter your Key");
            int key = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n");

            Console.WriteLine("Encrypted Data");

            string cipherText = Encipher(UserString, key);
            Console.WriteLine(cipherText);
            Console.Write("\n");

            Console.WriteLine("Decrypted Data:");

            string t = Decipher(cipherText, key);
            Console.WriteLine(t);
            Console.Write("\n");




            Console.ReadKey();

        }
        */
        public class DecodingProcess
        {
            //this function will count occurrence fo each charcter in string and stores it in a dictionary
            public Dictionary<char, int> getOccurranceOfChars(string msg)
            {
                Dictionary<char, int> dict = new Dictionary<char, int>();
                for (int i = 0; i < msg.Length; i++)
                {
                    int count = 0;
                    if (!dict.ContainsKey(msg[i])) // checks for unique character
                    {
                        for (int j = i; j < msg.Length; j++)
                        {
                            if (msg[i] == msg[j])
                                count++;
                        }
                        dict.Add(msg[i], count);
                    }
                }
                return dict;
            }

            //returns character that is occurred max time in dictionary or in msg
            public char getMaxOccurredChar(Dictionary<char, int> dict)
            {
                char c = 'a';
                int max = 0;
                foreach (KeyValuePair<char, int> pair in dict)
                {
                    if (pair.Value > max)
                    {
                        max = pair.Value;
                        c = pair.Key;
                    }
                }
                Console.WriteLine("char " + c + " occurred maximum for " + max + " times");
                return c;
            }

            //computes key/shift based on max occurred character is E
            public Dictionary<char, char> ComputeKeyValue(int start)
            {
                Dictionary<char, char> dec = new Dictionary<char, char>();
                for (int i = 0; i < 26; i++)
                {
                    if (start > 90)
                    {
                        start = 65;
                    } 
                    dec.Add((char)start, (char)(i + 65));
                    start++;
                }
                return dec;
            }

            //decodes msg based on the decoding key
            public void decodeMsg(string msg, char[] dmsg, Dictionary<char, char> dec)
            {
                for (int i = 0; i < msg.Length; i++)
                {
                    dmsg[i] = dec[msg[i]];
                    Console.Write(dmsg[i]);
                }
            }

            //computes value for A
            public int getStartValue(char c)
            { 
                int s = 0, d = 0;
                if (c > 'E')
                {
                    d = c - 'E';
                    s = c - 4;
                }
                else
                {
                    d = 'E' - c;
                    s = c + 4;
                }

                Console.WriteLine("ASCII value difference is " + d);
                Console.WriteLine();
                return s;
            }

            public static void Main()

            {

                DecodingProcess decode = new DecodingProcess();
                Console.WriteLine("What is the message you want to decode?: ");
                String message = Console.ReadLine();
                Console.WriteLine();
                Dictionary<char, int> dict = decode.getOccurranceOfChars(message);
                Console.WriteLine("Your Encoded Message: ");
                Console.WriteLine(message);
                Console.WriteLine();
                char c = decode.getMaxOccurredChar(dict);
                int start = decode.getStartValue(c);
                Dictionary<char, char> dec = decode.ComputeKeyValue(start);
                char[] decodedmessage = new char[message.Length];
                Console.WriteLine("Your Decoded Message: ");
                decode.decodeMsg(message, decodedmessage, dec);
            }
        }
    }
}
