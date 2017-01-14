using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungCancerBayesNetwork.Models;
using LungCancerBayesNetwork.Helpers;

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
            bayes.CreateDefaultStructre();
            bayes.PrintResult(bayes.GetDefaultResult());

            List<BayesBranch> structure = new List<BayesBranch>();
 
            structure.Add(new BayesBranch
            {
                Layers = new List<List<int>>()
                    {
                        new List<int>() {3,26,38},
                        new List<int>() {6,2}
                    }
            });

            structure.Add(new BayesBranch
            {
                Layers = new List<List<int>>()
                    {
                        new List<int>() {13,29,41},
                        new List<int>() {20,56}
                    }
            });

            structure.Add(new BayesBranch
            {
                Layers = new List<List<int>>()
                    {
                        new List<int>() {15,37,42},
                        new List<int>() {19}
                    }
            });

            bayes.Clear();
            bayes.SetFileName("test2");
            bayes.CreateStructre(structure);
            List<int> outputLayers = new List<int>() { 1 };
            bayes.PrintResult(bayes.GetResult(structure, outputLayers));

            structure = new List<BayesBranch>();

            structure.Add(new BayesBranch
            {
                Layers = new List<List<int>>()
                    {
                        new List<int>() {3,26,38}
                    }
            });

            structure.Add(new BayesBranch
            {
                Layers = new List<List<int>>()
                    {
                        new List<int>() {13,29,41}
                    }
            });

            structure.Add(new BayesBranch
            {
                Layers = new List<List<int>>()
                    {
                        new List<int>() {15,37,42}
                    }
            });

            bayes.Clear();
            bayes.SetFileName("test3");
            bayes.CreateStructre(structure); 
            bayes.PrintResult(bayes.GetResult(structure, new List<int>() { 1 }));


            structure = new List<BayesBranch>();

            structure.Add(new BayesBranch
            {
                Layers = new List<List<int>>()
                    {
                        new List<int>() {3,26,38,7},
                        new List<int>() {6,2,30},
                        new List<int>() {25,27}
                    }
            });

            structure.Add(new BayesBranch
            {
                Layers = new List<List<int>>()
                    {
                        new List<int>() {13,29,41},
                        new List<int>() {20,56,44},
                        new List<int>() {11}
                    }
            });

            structure.Add(new BayesBranch
            {
                Layers = new List<List<int>>()
                    {
                        new List<int>() {15,37,42},
                        new List<int>() {19,39,18},
                        new List<int>() {4}
                    }
            });

            bayes.Clear();
            bayes.SetFileName("test4");
            bayes.CreateStructre(structure);
            bayes.PrintResult(bayes.GetResult(structure, new List<int>() { 1 }));

            Console.ReadLine();
        }
    }
}
