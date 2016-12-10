﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungCancerBayesNetwork
{
    class Program
    {
        static List<String> getFileData(String path)
        {
            List<String> data = new List<String>();
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    do
                    {
                        data.Add(reader.ReadLine());
                    } while (!reader.EndOfStream);
                    return data;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("FileNotFound");
                System.Console.WriteLine(e.Message);
                return data;
            }

        }
        static List<CancerData> getDataFromFile(String path)
        {
            List<CancerData> data = new List<CancerData>();
            List<String> fileData = getFileData(path);
            foreach(String s in fileData)
            {
                if (s.Length > 0)
                {
                    String[] values = s.Split(',');
                    Int16 cancerClass = Int16.Parse(values[0]);
                    Int16[] attributes = new Int16[values.Length - 1];
                    for (int i = 1; i < values.Length; ++i)
                    {
                        try
                        {
                            attributes[i - 1] = Int16.Parse(values[i]);
                        }
                        catch (Exception e)
                        {
                            attributes[i - 1] = -1;//Obsługa znaków zapytania w pliku
                        }

                    }
                    data.Add(new CancerData(cancerClass, attributes));
                }

            }
            return data;
        }
        static void Main(string[] args)
        {
            LibraryTest libTest = new LibraryTest();
            libTest.JustTest();
            //System.Console.Write(getDataFromFile("lung-cancer.data.txt").ElementAt(20).ToString());
            //System.Console.ReadKey();
        }
    }
}
