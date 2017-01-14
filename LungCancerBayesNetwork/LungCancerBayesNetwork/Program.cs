using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungCancerBayesNetwork.Models;

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
            List<CancerData> data = getDataFromFile("lung-cancer.data.txt");

            LungCancerBayes bayes = new LungCancerBayes(data);
            bayes.CreateStructre();
            //bayes.PrintResult(bayes.GetResults());
            bayes.PrintResult(bayes.GetResult());
            Console.ReadLine();
        }
    }
}
