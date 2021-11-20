using System;
using System.IO;

namespace Module8_FinalProject_2
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите URL директории, что бы узнать ее размер. Для выхода введите exit.");
                string directoryPath = Console.ReadLine();

                if (directoryPath == "exit")
                {
                    break;
                }
                if (string.IsNullOrEmpty(directoryPath) || string.IsNullOrWhiteSpace(directoryPath))
                {
                    Console.WriteLine("Путь не указан");
                    continue;
                }

                DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
                
                if (!directoryInfo.Exists)
                {
                    Console.WriteLine($"Директории по пути {directoryInfo.FullName} не существует.");
                    continue;
                }

                try
                {
                    long directorySize = GetDirectorySize(directoryInfo);
                    Console.WriteLine($"Размер директории {directorySize:N0} байт.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }


        private static long GetDirectorySize(DirectoryInfo directoryInfo)
        {
            long result = 0;
            if (directoryInfo == null)
                return result;

            DirectoryInfo[] dirInfos = directoryInfo.GetDirectories();
            long innerDirectorySize = 0;
            long fileSize = 0;
            foreach (DirectoryInfo di in dirInfos)
            {
                innerDirectorySize += GetDirectorySize(di);
            }
            
            FileInfo[] fileInfos = directoryInfo.GetFiles();

            foreach (FileInfo fi in fileInfos)
            {
                fileSize += fi.Length;
            }
            result = innerDirectorySize + fileSize;
            
            return result;
        }
    }
}
