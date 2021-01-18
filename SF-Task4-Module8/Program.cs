using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
    static class function
    {
        static public T[] Diserialise<T>(string path)
        {
            T[] list = null;
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    list = (T[])formatter.Deserialize(fs);
                }
            }
            return list;
        }
    }
    class programm
    {
        static void Main()
        {
            ///Дисериализация бинарного потока
            Student[] list = function.Diserialise<Student>(@"D:\Students.dat");
            ///Создание  корневого директория
            Directory.CreateDirectory("D:\\Student");
            ///Создания списков формата .txt с перечислением студентов и их датой рождения
            foreach (Student s in list)
            {
                if (!File.Exists($"D:\\Student\\{s.Group}.txt"))
                {
                    using (StreamWriter sw = File.CreateText($"D:\\Student\\{s.Group}.txt"))
                    {
                        sw.WriteLine("Имя:" + s.Name + " Дата рождения:" + s.DateOfBirth);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(($"D:\\Student\\{s.Group}.txt")))
                    {
                        sw.WriteLine("Имя:" + s.Name + " Дата рождения:" + s.DateOfBirth);
                    }
                }
                Console.WriteLine($"Имя:{s.Name} Группа:{s.Group} ДР:{s.DateOfBirth}") ;
            }
        }
    }
}