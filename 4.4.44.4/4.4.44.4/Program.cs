using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace _4._4._44._4
{
    class Files : IComparable<Files>
    {             // 1         
        const int f_name = 20;
        private string name;
        private string extension;
        private int size;
        private string creation_date;
        private string attributes;
        public Files()
        {
            name = "Anonimous";
            extension = "";
            size = 0;
            creation_date = "";
            attributes = "";
        }
        public Files(string s)
        {
            name = s.Substring(0, f_name);
            extension = (s.Substring(f_name, 4));
            size = Convert.ToInt32(s.Substring(f_name + 4, 4));
            creation_date = (s.Substring(f_name + 8, 9));
            attributes = (s.Substring(f_name + 17));
            if (size < 0) throw new FormatException();

        }
        public override string ToString()
        {
            return string.Format("Name: {0,20} Extension: {1:20,24} Size: {2} Creation_date: {3:28,37} Attributes: {4:37,}", name, extension, size, creation_date, attributes);
        }
        public int Compare(string creation_date)
        {
            return (string.Compare(this.creation_date, 0, creation_date + " ", 0, creation_date.Length + 1, StringComparison.OrdinalIgnoreCase));
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Extension
        {
            get { return extension; }
            set { extension = value; }
        }
        public int Size
        {
            get { return size; }
            set
            {
                if (value > 0) size = value;
                else throw new FormatException();
            }
        }
        public string Creation_date
        {
            get { return creation_date; }
            set { creation_date = value; }
        }
        public string Attributes
        {
            get { return attributes; }
            set { attributes = value; }
        }


        // ------------------  операції класу ---------------------------         
        public int CompareTo(Files other)
        {

            // Alphabetic sort if salary is equal. [A to Z]
            if (this.Extension == other.Extension)
            {

                return this.Name.CompareTo(other.Name);
            }
            // Default to salary sort. [High to low]
            return other.Extension.CompareTo(this.Extension);
        }
    };



    class Program
    {
        static void Main(string[] args)
        {
            List<Files> list = new List<Files>();
            int n = 0;
            StreamReader f = new StreamReader("dbase.txt");
            string s;
            int i = 0;
            try
            {
                while ((s = f.ReadLine()) != null)
                {
                    list.Add(new Files(s));
                    ++i;
                }
                n = i - 1;
                list.Sort();


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

            Console.WriteLine("Enter text if you want to add a line in database: ");
            string names;
            while ((names = Console.ReadLine()) != "")
            {
                Console.WriteLine("Enter information:");
                string path = @"dbase.txt";
                if (!File.Exists(path))
                {
                    string createText = "Hello and Welcome your file were empty" + Environment.NewLine;
                    File.WriteAllText(path, createText);
                }
                string append = "\n";
                File.AppendAllText(path, append);
                string appendText;
                appendText = Console.ReadLine();
                File.AppendAllText(path, appendText);
                string readText = File.ReadAllText(path);
                Console.WriteLine(readText);
                Console.WriteLine("Text add to file");
            }


            Console.WriteLine("Enter text if you want to delete a line in database: ");
            string nam;
            while ((nam = Console.ReadLine()) != "")
            {
                string filePath = "dbase.txt";

                Console.WriteLine("Enter the line you want to delete: ");
                int numberOfLineToDelete = Convert.ToInt32(Console.ReadLine());
                string[] fileLines = File.ReadAllLines(filePath);
                fileLines[numberOfLineToDelete] = String.Empty; // deleting
                File.WriteAllLines(filePath, fileLines);
                string readText = File.ReadAllText(filePath);
                Console.WriteLine(readText);
                Console.WriteLine("Text delete to file");
            }

            Console.WriteLine("Enter text for which you want to know the information a line in database:");
            string name;
            while ((name = Console.ReadLine()) != "")
            {
                bool not_found = true;
                for (int k = 0; k <= n; ++k)
                {
                    Files pers = list[k];
                    if (pers.Compare(name) == 0)
                    {
                        Console.WriteLine(pers);
                        not_found = false;
                    }
                }
                if (not_found)
                    Console.WriteLine("There is no such information");
                Console.WriteLine("Enter the file creation_date for which you want to know the information Enter to complete");
            }
        }

    }
}

