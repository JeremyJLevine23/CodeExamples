using System;

namespace ZumbaGym
{
    class Program
    {
        public class Zumba
        {
            // initizalize data
            // weekly attendance in 2D array
            int[,] weeklyClassAttendance;
            int totalClassAttendance;
            int classPrice = 4;
            // daily attendance in 1D array
            int[] dailyClassAttendance;
            // daily revenue in 1D array
            int[] dailyClassRevenue;
            // weekly time slot attendance in 1D array
            int[] classAttendance;
            // weekly time slot revenue in 1D array
            int[] weeklyClassRevenue;
            // per time-slot revenue in 1D array
            int[] perTimeSlotRevenue;

            // methods
            public Zumba(int[,] attendance)
            {
                weeklyClassAttendance = attendance;
                int row = attendance.GetLength(0);
                int col = attendance.GetLength(1);
                dailyClassAttendance = new int[row];
                classAttendance = new int[col];
                dailyClassRevenue = new int[row];
                perTimeSlotRevenue = new int[col];
            }

            //display method for Zumba class information
            public void DisplayZumba()
            {
                //weekly attendance 
                Console.WriteLine("The weekly attendance is {0}", GetWeeklyAttendance());
                //weekly revenue
                Console.WriteLine("The weekly revenue is {0:C2}", GetWeeklyRevenue());
                //per session attendance
                Console.WriteLine("The per session attendance is: ");
                GetPerSlotAttendance();
                //traversing through classAttendance to get data to display to user
                foreach (int i in classAttendance)
                {
                    Console.Write("{0} \t", i);
                }
                //space
                Console.WriteLine();
                //per day zumba class attendance
                Console.WriteLine("The per day zumba class attendance is: ");
                GetPerDayAttendance();
                //traversing through dailyClassAttendance to get data to display to user
                foreach (int i in dailyClassAttendance)
                {
                    Console.Write("{0} \t", i);
                }
                //space
                Console.WriteLine();
                //daily zumba revenue
                Console.WriteLine("The daily revenue is: ");
                GetPerDayRevenue();
                //traversing through dailyClassRevenue to get data to display to user
                foreach (int i in dailyClassRevenue)
                {
                    Console.Write("{0:C2} \t", i);
                }
                //space
                Console.WriteLine();
                //per class revenue
                Console.WriteLine("The per class revenue is: ");
                GetPerSlotRevenue();
                //traversing through perTimeSlotRevenue to get data to display to user
                foreach (int i in perTimeSlotRevenue)
                {
                    Console.Write("{0:C2} \t", i);
                }
                //space
                Console.WriteLine();

            }

            public void RunZumba()
            {
                // need to ask user for yes or no
                // do-while loop
                do
                {
                    //ask user if they want to see zumba class information
                    Console.WriteLine("Do you want to see the zumba class information? (yes/no)");
                    string userInput = Console.ReadLine();
                    if (userInput.ToLower() == "yes")
                    {
                        DisplayZumba();
                    }
                    //ask user if they want to continue on
                    Console.WriteLine("Do you want to continue on? (yes/no)");
                    userInput = Console.ReadLine();
                    if (userInput.ToLower() != "yes")
                    {
                        Console.WriteLine("Goodbye!");
                        break;
                    }

                }
                while (true);
            }

            //weekly attendance method
            public int GetWeeklyAttendance()
            {
                //traversing through the weeklyClassAttendance and adding it to a 1D totalClassAttendance array
                foreach (int a in weeklyClassAttendance)
                {
                    totalClassAttendance += a;
                }

                return totalClassAttendance;
            }

            //per slot attendance method
            public int[] GetPerSlotAttendance()
            {
                //running through the rows and columns in weekly attendance to add total class attednace total
                for (int k = 0; k < weeklyClassAttendance.GetLength(1); k++)
                {
                    for (int l = 0; l < weeklyClassAttendance.GetLength(0); l++)
                    {
                        //to get sum of columns for per time slot attendance
                        classAttendance[k] += weeklyClassAttendance[l, k];
                    }
                }

                return classAttendance;
            }

            //get per day attendance method
            public int[] GetPerDayAttendance()
            {
                //running through the rows and columns to get the total per day attendance 
                for (int j = 0; j < weeklyClassAttendance.GetLength(0); j++)
                {
                    for (int t = 0; t < weeklyClassAttendance.GetLength(1); t++)
                    {
                        //sum of rows to get total daily attendance
                        dailyClassAttendance[j] += weeklyClassAttendance[j, t];
                    }
                }

                return dailyClassAttendance;
            }

            //weekly revenue method
            public double GetWeeklyRevenue()
            {
                //weekly revenue calculation
                return totalClassAttendance * classPrice;
            }

            //per time slot revenue method
            public int[] GetPerSlotRevenue()
            {
                //running through the rows and columns to get the total per time slot revenue
                for (int a = 0; a < weeklyClassAttendance.GetLength(1); a++)
                {
                    for (int b = 0; b < weeklyClassAttendance.GetLength(0); b++)
                    {
                        //adding up the columns to get the total time slot revenue
                        perTimeSlotRevenue[a] += weeklyClassAttendance[b, a] * classPrice;
                    }
                }
                return perTimeSlotRevenue;
            }

            //per day revenue method
            public int[] GetPerDayRevenue()
            {
                //running through the rows and colums to get the daily revenue
                for (int i = 0; i < weeklyClassAttendance.GetLength(0); i++)
                {
                    for (int j = 0; j < weeklyClassAttendance.GetLength(1); j++)
                    {
                        //adding up the rows to get the total daily revenue
                        dailyClassRevenue[i] += weeklyClassAttendance[i, j] * classPrice;
                    }
                }
                return dailyClassRevenue;
            }
        //main method
            static void Main(string[] args)
            {
                //creating zumba array with zumba class methods
                Zumba zumba = new Zumba(
                    new int[,]
                    {
                        {8,10,15,20 },
                        {11,15,17,18 },
                        {14,12,22,20 },
                        {9,14,17,12 },
                        {10,12,21,22 },
                        {12,12,7,5 },

                    });
                //calling the RunZumba() method
                zumba.RunZumba();
            }
        }
    }
}