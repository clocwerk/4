using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace _4._1
{
    class Person : IComparable<Person>
    {
        const int l_name = 20;
        private string name;
        private int number;
        private int bal1;
        private int bal2;
        private int bal3;
        private int bal4;
        private int bal5;
        private int bal;
        public Person()            // конструктор без параметрів         
        {
            name = "Anonimous";
            number = 0;
            bal1 = 0;
            bal2 = 0;
            bal3 = 0;
            bal4 = 0;
            bal5 = 0;
        }
        public Person(string s)                        // 2 конструктор з параметром         
        {
            name = s.Substring(0, l_name);
            number = Convert.ToInt32(s.Substring(l_name, 6));
            bal1 = Convert.ToInt32(s.Substring(l_name + 6, 3));
            bal2 = Convert.ToInt32(s.Substring(l_name + 9, 3));
            bal3 = Convert.ToInt32(s.Substring(l_name + 12, 3));
            bal4 = Convert.ToInt32(s.Substring(l_name + 15, 3));
            bal5 = Convert.ToInt32(s.Substring(l_name + 18));
            if (number < 0) throw new FormatException();
            if (bal1 < 0) throw new FormatException();
            if (bal2 < 0) throw new FormatException();
            if (bal3 < 0) throw new FormatException();
            if (bal4 < 0) throw new FormatException();
            if (bal5 < 0) throw new FormatException();
        }
        public override string ToString()         // 3 перевантажений метод       
        {
            return string.Format("Name: {0,20} Number: {1} bal1: {2} bal2: {3} bal3: {4} bal4: {5} bal5: {6}", name, number, bal1, bal2, bal3, bal4, bal5);
        }

        // --------------------  властивості класу --------------------------         
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Number
        {
            get { return number; }
            set
            {
                if (value > 0) number = value;
                else throw new FormatException();
            }
        }
        public int Bal
        {
            get
            {
                bal = ((bal1 + bal2 + bal3 + bal4 + bal5) / 5);
                return bal;
            }
            set
            {
                if (value > 0) bal = value;
                else throw new FormatException();
            }
        }

        // ------------------  операції класу ---------------------------         
        public int CompareTo(Person other)
        {

            // Alphabetic sort if salary is equal. [A to Z]
            if (this.Bal == other.Bal)
            {

                return this.Name.CompareTo(other.Name);
            }
            // Default to salary sort. [High to low]
            return other.Bal.CompareTo(this.Bal);
        }
    };



    class Program
    {
        static void Main(string[] args)
        {
            List<Person> list = new List<Person>();
            int n = 0;
            StreamReader f = new StreamReader("dbase.txt");
            string s;
            int i = 0;
            try
            {
                while ((s = f.ReadLine()) != null)
                {
                    list.Add(new Person(s));
                    ++i;
                }
                n = i - 1;
                list.Sort();
                list.Reverse();

                foreach (var element in list)
                {
                    Console.WriteLine(element);
                }
                f.Close();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Check the correct name and path to the file!");
                return;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Very large file!");
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message); return;
            }
            int n_pers = 0;
            double mean_pay = 0;
            for (int k = 0; k <= n; ++k)
            {
                Person pers = list[k];
                n_pers++;
                if (pers.Bal < 50)
                {
                    mean_pay++;
                }
            }
            Console.WriteLine("Percentage of students with unsatisfactory grades: {0:F2} %", (mean_pay / n_pers) * 100);
            Console.ReadKey();
        }
    }
}
