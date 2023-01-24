using System;

namespace Task3
{
    class Task3
    {
        static void Main()
        {
            string[] array = new string[] { "code", "doce", "ecod", "framer", "frame" };
            string[] changedArray = DeleteAnagrams(array);
            foreach (string s in changedArray)
                Console.WriteLine(s);
        }

        private static string[] DeleteAnagrams(string[] array)
        {
            List<string> list = new()
            {
                array[0]
            };

            for (int i = 1; i < array.Length; i++)
            {
                if (!IsAnagram(array[i], array[i - 1]))
                    list.Add(array[i]);
            }

            list.Sort();
            return list.ToArray();
        }

        private static bool IsAnagram(string word1, string word2)
        {
            bool result = true;
            char[] chars1 = word1.ToCharArray();
            char[] chars2 = word2.ToCharArray();
            Array.Sort(chars1);
            Array.Sort(chars2);
            if (chars1.Length != chars2.Length)
                return false;
            else
            {
                for (int i = 0; i < chars1.Length; i++)
                {
                    if (chars1[i] != chars2[i])
                        result = false;
                }
            }
            return result;
        }
    }
}