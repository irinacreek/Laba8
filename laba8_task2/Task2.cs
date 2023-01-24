using System;
using System.Collections;

namespace Task2
{
    public class Task2
    {
        static void Main()
        {
            Console.WriteLine("Введите дату и время в формате гггг-мм-дд чч:мм");
            string enterDate = Console.ReadLine();

            enterDate = ChangeDate(enterDate);

            int length = File.ReadAllLines("file2.txt").Length - 1;
            Bills[] bills = new Bills[length];
            int count = 0;
            int start = 0;

            foreach (string line in File.ReadLines("file2.txt"))
            {
                if (start == 0)
                {
                    start = int.Parse(line);
                    continue;
                }
                Distribution(line, bills, count);
                count++;
            }
            Sort(bills, new CompareDate());
            Console.WriteLine(CountSum(enterDate, start, length, bills));
        }
        public static void Sort(Array bills, IComparer comparer)
        {
            for (int i = bills.Length - 1; i > 0; i--)
                for (int j = 1; j <= i; j++)
                {
                    object obj1 = bills.GetValue(j - 1);
                    object obj2 = bills.GetValue(j);
                    if (comparer.Compare(obj1, obj2) < 0)
                    {
                        object temporary = bills.GetValue(j);
                        bills.SetValue(bills.GetValue(j - 1), j);
                        bills.SetValue(temporary, j - 1);
                    }
                }
        }
        public static string ChangeDate(string enterDate)
        {
            enterDate = enterDate.Replace(":", "");
            enterDate = enterDate.Replace("-", "");
            enterDate = enterDate.Replace(" ", "");
            return enterDate;
        }
        public static void Distribution(string line, Bills[] bills, int count)
        {
            line = ChangeDate(line);
            string[] lines = line.Split('|');
            bills[count] = new Bills();
            if (lines.Length == 3)
            {
                bills[count].enterDate = long.Parse(lines[0]);
                bills[count].start = int.Parse(lines[1]);
                bills[count].transaction = lines[2];
            }
            else
            {
                bills[count].enterDate = long.Parse(lines[0]);
                bills[count].transaction = lines[1];
            }
        }
        public static object CountSum(string enterDate, int start, int length, Bills[] bills)
        {
            bool firstTime = false;
            int lastNumber = 0;

            while (start > 0)
            {
                if (bills[length - 1].enterDate == long.Parse(enterDate))
                {
                    firstTime = true;
                }
                else if (firstTime && bills[length - 1].enterDate != long.Parse(enterDate))
                {
                    break;
                }

                if (bills[length - 1].transaction == "in")
                {
                    start += bills[length - 1].start;
                    lastNumber = bills[length - 1].start;
                }
                if (bills[length - 1].transaction == "out")
                {
                    start -= bills[length - 1].start;
                    lastNumber = bills[length - 1].start * (-1);
                }
                if (bills[length - 1].transaction == "revert") start -= lastNumber;
                length--;
            }
            if (start < 0) return "Ошибка";
            else return "На Вашем счете " + start;
        }
    }

    public class Bills
    {
        public long enterDate;
        public int start = 0;
        public string transaction;
    }

    class CompareDate : IComparer
    {
        public int Compare(object obj1, object obj2)
        {
            var firstBill = (Bills)obj1;
            var secondBill = (Bills)obj2;
            return firstBill.enterDate.CompareTo(secondBill.enterDate);
        }
    }
}