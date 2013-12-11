using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SQLite;

namespace usda2sqlite
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists("input/NUT_DATA.txt"))
            {
                Console.WriteLine("Please ensure input folder exists and contains SR ascii files.");
                return;
            }
            SQLiteConnection con = new SQLiteConnection("usda.s3db");
            con.CreateTable<android_metadata>();
            var lang = new android_metadata(); lang.locale = "en_US";
            con.Insert(lang);
            CreateTable<food>(con,"input/FOOD_DES.txt");
            CreateTable<nutrient_data>(con, "input/NUT_DATA.txt");
            CreateTable<nutrient_def>(con, "input/NUTR_DEF.txt");
            CreateTable<weight>(con, "input/WEIGHT.txt");
            CreateTable<food_grp>(con, "input/FD_GROUP.txt");
            con.Close();
        }

        private static void CreateTable<T>(SQLiteConnection con, string inputFile) where T : class, IData, new()
        {

            var re = con.CreateTable<T>();
            
            var file = new StreamReader(inputFile);
            string line = "";
            con.CreateTable<T>();
           // T[] items = new T[7500];
            List<T> items = new List<T>();
            int cnt = 0;
            while ((line = file.ReadLine()) != null)
            {
                if (cnt == 7500)
                {
                    //var result = con.InsertAll(items);
                    //cnt = 0;
                }
                T f = new T();
                f.build(line);
                items.Add(f);
                //items[cnt] = f;

                cnt++;
               
            }
            var result = con.InsertAll(items);
            Console.WriteLine(items.Count + " Added");
        }
             
    }
   
}
