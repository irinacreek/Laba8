using System;
using System.IO;
using System.Text;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Task1
{
    class Subtitles
    {
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public string Position { get; set; }
        public string ColorText { get; set; }
        public string Text { get; set; }
    }

    class Task1
    {
        static Timer timer;
        static int Timer = 0;
        static List<Subtitles> subtitles = new List<Subtitles>();
        static int widthScreen = 60;
        static int heightScreen = 30;

        static void ReadFile()
        {
            foreach (string str in File.ReadAllLines(@"Subtitles.txt"))
            {
                Subtitles subtitle = new Subtitles();

                string[] startTime = str.Substring(0, 5).Split(':');
                string[] endTime = str.Substring(8, 5).Split(':');

                subtitle.StartTime = 60 * int.Parse(startTime[0]) + int.Parse(startTime[1]);
                subtitle.EndTime = 60 * int.Parse(endTime[0]) + int.Parse(endTime[1]);

                if (str[14] == '[')
                {
                    subtitle.ColorText = str.Substring(str.IndexOf(',') + 2, str.IndexOf(']') - str.IndexOf(',') - 2);

                    subtitle.Position = str.Substring(str.IndexOf('[') + 1, str.IndexOf(',') - str.IndexOf('[') - 1);

                    subtitle.Text = str.Substring((str.IndexOf("]") + 2));
                }
                else
                {
                    subtitle.ColorText = "White";
                    subtitle.Position = "Bottom";
                    subtitle.Text = str.Substring(14);
                }
                subtitles.Add(subtitle);
            }
        }

        static void PrintScreen()
        {
            for (int i = 0; i < widthScreen; i++)
                Console.Write("-");
            Console.WriteLine();
            for (int i = 0; i < heightScreen - 2; i++)
            {
                Console.Write("|");
                for (int j = 0; j < widthScreen - 2; j++)
                    Console.Write(" ");
                Console.WriteLine("|");
            }
            for (int i = 0; i < widthScreen; i++)
                Console.Write("-");
        }

        static void StartTimer()
        {
            timer = new Timer(1000);
            timer.Elapsed += CheckTime;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        static void CheckTime(Object source, ElapsedEventArgs e)
        {
            foreach (Subtitles subtitle in subtitles)
            {
                if (subtitle.StartTime == Timer)
                {
                    PrintSubtitle(subtitle);
                }
                if (subtitle.EndTime == Timer)
                {
                    DeleteSubtitle(subtitle);
                }
            }
            Timer++;
        }

        static void PrintSubtitle(Subtitles subtitle)
        {
            SetPosition(subtitle);
            СhangeColorText(subtitle);
            Console.WriteLine(subtitle.Text);
        }

        static void СhangeColorText(Subtitles subtitle)
        {
            switch (subtitle.ColorText)
            {
                case "Red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "Green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "Blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "White":
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    break;
            }
        }

        static void SetPosition(Subtitles subtitle)
        {
            switch (subtitle.Position)
            {
                case "Top":
                    Console.SetCursorPosition((widthScreen - subtitle.Text.Length) / 2, 1);
                    break;
                case "Bottom":
                    Console.SetCursorPosition((widthScreen - subtitle.Text.Length) / 2, heightScreen);
                    break;
                case "Right":
                    Console.SetCursorPosition(widthScreen - subtitle.Text.Length - 1, heightScreen / 2);
                    break;
                case "Left":
                    Console.SetCursorPosition(2, heightScreen / 2);
                    break;
                default:
                    break;
            }
        }

        static void DeleteSubtitle(Subtitles subtitle)
        {
            SetPosition(subtitle);
            for (int i = 0; i < subtitle.Text.Length; i++)
            {
                Console.Write(" ");
            }
        }

        static void Main(string[] args)
        {
            ReadFile();
            PrintScreen();
            StartTimer();
            Console.ReadKey();
        }


    }
}