using System;
using System.IO;
using static System.Console;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Init vars
            int sumRange = 0;
            string readedFile = string.Empty;
            int convertedIntFile;
            int answer = 0;

            // Init paths
            string inputFilePath = $"Input.txt";
            string outputFilePath = $"Output.txt";

            // Init random
            Random rnd = new Random(DateTime.Now.Millisecond);

            // Give random number МАХ 10^4
            sumRange = rnd.Next(-10000, 10000);

            // Delete files if exists
            if (File.Exists(inputFilePath))
                File.Delete(inputFilePath);
            if (File.Exists(outputFilePath))
                File.Delete(outputFilePath);

            // Create new input file and write random number in txt file
            using (FileStream fileStream = new FileStream(inputFilePath, FileMode.OpenOrCreate))
            {
                // Encoding string in bytes array
                byte[] result = System.Text.Encoding.Default.GetBytes(sumRange.ToString());
                // Write bytes in txt file
                fileStream.Write(result, 0, result.Length);
            }

            // Show formatted number
            WriteLine($"Sum range is '1' - '{sumRange:N0}'");

            // Open and read txt file
            using (FileStream fileStream = File.OpenRead(inputFilePath))
            {
                // init byte array
                byte[] array = new byte[fileStream.Length];
                // Read number from file
                fileStream.Read(array, 0, array.Length);

                // Engoding byte array to string
                readedFile = System.Text.Encoding.Default.GetString(array);

                // Convert string to int
                convertedIntFile = Convert.ToInt32(readedFile);
            }

            // !!!!!!!!!!!!!Count up sum WRONG DECISION!!!!!!!!!!!!!
            //for (int i = 1; i <= convertedIntFile; i++)
            //{
            //    answer += i;
            //}
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            // Count up sum CORRECT DECISION
            if (convertedIntFile > 0)
                answer = (1 + convertedIntFile) * convertedIntFile / 2;
            else
                answer = (-1 + convertedIntFile) * convertedIntFile / 2;

            // Show formatted sum in console
            WriteLine($"Sum is: '{answer:N0}'");

            // Write sum in output txt file
            using (FileStream fileStream = new FileStream(outputFilePath, FileMode.OpenOrCreate))
            {
                // Init bayt array and encode output from int to byte
                byte[] result = System.Text.Encoding.Default.GetBytes(answer.ToString());
                // Write in output file
                fileStream.Write(result, 0, result.Length);
            }
        }
    }
}

//В этой задаче можно воспользоваться форумулой арифметической прогрессии: Sn = (a1+an)*n/2. Но начинающие программисты часто забывают эту формулу и в этом случае прибегают к
//вычислению этой суммы с помощью цикла и решают эту задачу, используя следующий алгоритм:

//read(n);
//s=0;
//for i=1..n{
//s=s+i;
//}
//write(s);

//Но сложность этой задачи не в вычислении этой суммы и приведенный выше алгоритм не проходит на тесте! Оказывается, здесь очень внимательно нужно прочитать условия задачи,
//особенно ограничения на число N. Дело в том, что это число может быть отрицательным!!!
