using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        bool clickDown = true;
        int currentPage = 1;
        string whatSemester;
        string year;
        string subject;
        string courseName;
        string sectionNum;
        string courseNum;
        string courseAbv;
        string main = "Main";

        public class Item
        {
            public string ItemLine1 { get; set; }
            public string ItemLine2 { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            List<Item> list = new List<Item>();
            StreamReader inFile;
            string inLine;

            if (File.Exists("USIINFOterms.txt"))
            {
                try
                {
                    inFile = new StreamReader("USIINFOterms.txt");
                    while ((inLine = inFile.ReadLine()) != null)
                    {
                        if (inLine.IndexOf("<value>") >= 0)
                        {
                            int start = inLine.IndexOf("<value>") + 7;
                            int len = inLine.IndexOf("</value>") - start;
                            Item item = new Item();
                            item.ItemLine1 = inLine.Substring(start, len);
                            inLine = inFile.ReadLine();
                            start = inLine.IndexOf("<value>") + 7;
                            len = inLine.IndexOf("</value>") - start;
                            item.ItemLine2 = inLine.Substring(start, len);
                            list.Add(item);
                        }
                        Console.WriteLine(inLine);
                    }
                }
                catch (System.IO.IOException exc)
                {
                    Console.WriteLine("Error");
                }
            }

            Dispatcher.BeginInvoke(new Action(() => ListBox1.ItemsSource = list));

            //WebClient wc = new WebClient();
            //wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
            //wc.DownloadStringAsync(new Uri("http://www.usi.edu/webservices/iphone/USIINFOterms.xml"));

        }

        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            PageTitle.Text = e.Result;
        }

        private void page2()
        {
            List<Item> list = new List<Item>();
            StreamReader inFile;
            string inLine;
            if (File.Exists(whatSemester))
            {
                try
                {
                    inFile = new StreamReader(whatSemester);
                    while ((inLine = inFile.ReadLine()) != null)
                    {
                        if (inLine.IndexOf("<row>") >= 0)
                        {
                            inLine = inFile.ReadLine();
                            inLine = inFile.ReadLine();

                            int start = inLine.IndexOf("<value>") + 7;
                            int end = inLine.IndexOf("</value>") - start;
                            Item item = new Item();
                            item.ItemLine2 = inLine.Substring(start, end);

                            inLine = inFile.ReadLine();
                            start = inLine.IndexOf("<value>") + 7;
                            end = inLine.IndexOf("</value>") - start;
                            item.ItemLine1 = inLine.Substring(start, end);

                            if (!list.Any(x => x.ItemLine2 == item.ItemLine2))
                                list.Add(item);
                        }
                        Console.WriteLine(inLine);
                    }
                }
                catch (System.IO.IOException exc)
                {
                    Console.WriteLine("You have an Error!");
                }
            }
            Dispatcher.BeginInvoke(new Action(() => ListBox1.ItemsSource = list));
        }

        private void page3()
        {
            List<Item> list = new List<Item>();
            StreamReader inFile;
            string inLine;

            if (File.Exists(whatSemester))
            {
                try
                {
                    inFile = new StreamReader(whatSemester);
                    while ((inLine = inFile.ReadLine()) != null)
                    {
                        if (inLine.IndexOf("<row>") >= 0)
                        {
                            inLine = inFile.ReadLine();
                            inLine = inFile.ReadLine();
                            int start = inLine.IndexOf("<value>") + 7;
                            int end = inLine.IndexOf("</value>") - start;

                            string s = inLine.Substring(start, end);
                            if (subject == s)
                            {

                                inLine = inFile.ReadLine();
                                inLine = inFile.ReadLine();
                                inLine = inFile.ReadLine();
                                inLine = inFile.ReadLine();

                                start = inLine.IndexOf("<value>") + 7;
                                end = inLine.IndexOf("</value>") - start;
                                Item item = new Item();
                                item.ItemLine1 = inLine.Substring(start, end);

                                inLine = inFile.ReadLine();
                                inLine = inFile.ReadLine();
                                start = inLine.IndexOf("<value>") + 7;
                                end = inLine.IndexOf("</value>") - start;
                                item.ItemLine2 = inLine.Substring(start, end);
                                if (!list.Any(x => x.ItemLine2 == item.ItemLine2))
                                    list.Add(item);
                            }
                        }
                        Console.WriteLine(inLine);
                    }
                }
                catch (System.IO.IOException exc)
                {
                    Console.WriteLine("You have an Error!");
                }
            }
            Dispatcher.BeginInvoke(new Action(() => ListBox1.ItemsSource = list));
        }

        private void page4()
        {
            StreamReader inFile;
            string inLine;
            List<Item> list = new List<Item>();

            if (File.Exists(whatSemester))
            {
                try
                {
                    inFile = new StreamReader(whatSemester);
                    while ((inLine = inFile.ReadLine()) != null)
                    {
                        if (inLine.IndexOf("<row>") >= 0)
                        {
                            inLine = inFile.ReadLine();
                            inLine = inFile.ReadLine();
                            int start = inLine.IndexOf("<value>") + 7;
                            int end = inLine.IndexOf("</value>") - start;
                            courseAbv = inLine.Substring(start, end);

                            inLine = inFile.ReadLine();
                            inLine = inFile.ReadLine();
                            start = inLine.IndexOf("<value>") + 7;
                            end = inLine.IndexOf("</value>") - start;
                            courseNum = inLine.Substring(start, end);

                            inLine = inFile.ReadLine();
                            start = inLine.IndexOf("<value>") + 7;
                            end = inLine.IndexOf("</value>") - start;
                            sectionNum = inLine.Substring(start, end);

                            inLine = inFile.ReadLine();
                            start = inLine.IndexOf("<value>") + 7;
                            end = inLine.IndexOf("</value>") - start;
                            if (String.Compare(inLine.Substring(start, end), courseName) == 0)
                            {
                                Item item1 = new Item();
                                item1.ItemLine1 = "Course Code: " + courseAbv + courseNum + "." + sectionNum;
                                list.Add(item1);

                                Item line2 = new Item();
                                line2.ItemLine1 = "Course Name: " + inLine.Substring(start, end);
                                list.Add(line2);

                                inLine = inFile.ReadLine();
                                start = inLine.IndexOf("<value>") + 7;
                                end = inLine.IndexOf("</value>") - start;
                                string firstName = inLine.Substring(start, end);

                                inLine = inFile.ReadLine();
                                start = inLine.IndexOf("<value>") + 7;
                                end = inLine.IndexOf("</value>") - start;
                                string lastName = inLine.Substring(start, end);

                                Item item3 = new Item();
                                item3.ItemLine1 = "Professor: " + lastName + ", " + firstName;
                                list.Add(item3);

                                inLine = inFile.ReadLine();
                                inLine = inFile.ReadLine();
                                start = inLine.IndexOf("<value>") + 7;
                                end = inLine.IndexOf("</value>") - start;
                                string courseLocation = inLine.Substring(start, end);
                                
                                inLine = inFile.ReadLine();
                                start = inLine.IndexOf("<value>") + 7;
                                end = inLine.IndexOf("</value>") - start;
                                string building = inLine.Substring(start, end);

                                inLine = inFile.ReadLine();
                                start = inLine.IndexOf("<value>") + 7;
                                end = inLine.IndexOf("</value>") - start;
                                string classNumber = inLine.Substring(start, end);

                                inLine = inFile.ReadLine();
                                start = inLine.IndexOf("<value>") + 7;
                                end = inLine.IndexOf("</value>") - start;
                                string mondayClass = inLine.Substring(start, end);

                                inLine = inFile.ReadLine();
                                start = inLine.IndexOf("<value>") + 7;
                                end = inLine.IndexOf("</value>") - start;
                                string tuesdayClass = inLine.Substring(start, end);

                                inLine = inFile.ReadLine();
                                start = inLine.IndexOf("<value>") + 7;
                                end = inLine.IndexOf("</value>") - start;
                                string wednesdayClass = inLine.Substring(start, end);

                                inLine = inFile.ReadLine();
                                start = inLine.IndexOf("<value>") + 7;
                                end = inLine.IndexOf("</value>") - start;
                                string thursdayClass = inLine.Substring(start, end);

                                inLine = inFile.ReadLine();
                                start = inLine.IndexOf("<value>") + 7;
                                end = inLine.IndexOf("</value>") - start;
                                string fridayClass = inLine.Substring(start, end);

                                inLine = inFile.ReadLine();
                                inLine = inFile.ReadLine();
                                inLine = inFile.ReadLine();
                                start = inLine.IndexOf("<value>") + 7;
                                end = inLine.IndexOf("</value>") - start;
                                string startTime = inLine.Substring(start, end);

                                inLine = inFile.ReadLine();
                                start = inLine.IndexOf("<value>") + 7;
                                end = inLine.IndexOf("</value>") - start;
                                string endTime = inLine.Substring(start, end);

                                Item item4 = new Item();
                                item4.ItemLine1 = "Meetings: " + mondayClass + " " + tuesdayClass + " " + wednesdayClass + " " + thursdayClass + " "+ fridayClass + " " + startTime + " " + endTime;
                                list.Add(item4);

                                Item item5 = new Item();
                                item5.ItemLine1 = "Room: " + courseLocation + " " + building + " " + classNumber;
                                list.Add(item5);

                                inLine = inFile.ReadLine();
                                start = inLine.IndexOf("<value>") + 7;
                                end = inLine.IndexOf("T00:00:00") - start;
                                string courseStartDate = inLine.Substring(start, end);

                                inLine = inFile.ReadLine();
                                start = inLine.IndexOf("<value>") + 7;
                                end = inLine.IndexOf("T00:00:00") - start;
                                string courseEndDate = inLine.Substring(start, end);

                                Item item6 = new Item();
                                item6.ItemLine1 = "Meets: " + courseStartDate + " " + courseEndDate;
                                list.Add(item6);

                                break;
                            }
                        }
                        Console.WriteLine(inLine);
                    }
                }
                catch (System.IO.IOException exc)
                {
                    Console.WriteLine("You have an Error!");
                }
            }
            Dispatcher.BeginInvoke(new Action(() => ListBox1.ItemsSource = list));
        }


        private void ListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e != null)
            {
                if (clickDown)
                {
                    IEnumerator ie = e.AddedItems.GetEnumerator();
                    ie.MoveNext();

                    if (currentPage == 1)
                    {
                        PageTitle.Text = "Fields";
                        year = ((Item)ie.Current).ItemLine2;
                        whatSemester = "USIINFO" + year + ".txt";
                        page2();
                        clickDown = false;
                        currentPage = 2;
                    }
                    else if (currentPage == 2)
                    {
                        PageTitle.Text = "Classes";
                        subject = ((Item)ie.Current).ItemLine2;
                        page3();
                        clickDown = false;
                        currentPage = 3;
                    }
                    else if (currentPage == 3)
                    {
                        PageTitle.Text = "Course Description";
                        courseName = ((Item)ie.Current).ItemLine1;
                        page4();
                        clickDown = false;
                        currentPage = 4;
                    }
                }
                else
                {
                    clickDown = true;
                }
            }
        }       

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage == 2)
            {
                PageTitle.Text = "Terms";
                List<Item> list = new List<Item>();
                StreamReader inFile;
                string inLine;

                if (File.Exists("USIINFOterms.txt"))
                {
                    try
                    {
                        inFile = new StreamReader("USIINFOterms.txt");
                        while ((inLine = inFile.ReadLine()) != null)
                        {
                            if (inLine.IndexOf("<value>") >= 0)
                            {
                                int start = inLine.IndexOf("<value>") + 7;
                                int len = inLine.IndexOf("</value>") - start;
                                Item item = new Item();
                                item.ItemLine1 = inLine.Substring(start, len);
                                inLine = inFile.ReadLine();
                                start = inLine.IndexOf("<value>") + 7;
                                len = inLine.IndexOf("</value>") - start;
                                item.ItemLine2 = inLine.Substring(start, len);
                                list.Add(item);
                            }
                            Console.WriteLine(inLine);
                        }
                    }
                    catch (System.IO.IOException exc)
                    {
                        Console.WriteLine("Error");
                    }
                }

                Dispatcher.BeginInvoke(new Action(() => ListBox1.ItemsSource = list));
                currentPage = 1;
            }
            else if (currentPage == 3)
            {
                PageTitle.Text = "Fields";
                page2();
                currentPage = 2;
            }
            else if (currentPage == 4)
            {
                PageTitle.Text = "Classes";
                page3();
                currentPage = 3;
            }
        }
    }
}
