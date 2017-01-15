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
            foreach (String s in fileData)
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
            //##########################################################################Structure1
            LungCancerBayes bayes = new LungCancerBayes(data);
            bayes.CreateDefaultStructre();
            bayes.PrintResult(bayes.GetDefaultResult());
            LungCancerBayes.SendResultToFile(bayes.GetDefaultResult(), "test1");
            //##########################################################################Structure2
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
            LungCancerBayes.SendResultToFile(bayes.GetResult(structure, new List<int>() { 1 }), "test1");
            //##########################################################################Structure2

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
            LungCancerBayes.SendResultToFile(bayes.GetResult(structure, new List<int>() { 1 }), "test2");
            //##########################################################################Structure3


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
            LungCancerBayes.SendResultToFile(bayes.GetResult(structure, new List<int>() { 1 }), "test3");
            //##########################################################################Structure4
            structure = new List<BayesBranch>();

            structure.Add(new BayesBranch
            {
                Layers = new List<List<int>>()
                    {
                        new List<int>() {3},
                        new List<int>() {6},
                        new List<int>() {25}
                    }
            });

            structure.Add(new BayesBranch
            {
                Layers = new List<List<int>>()
                    {
                        new List<int>() {13},
                        new List<int>() {20},
                        new List<int>() {11}
                    }
            });

            structure.Add(new BayesBranch
            {
                Layers = new List<List<int>>()
                    {
                        new List<int>() {15},
                        new List<int>() {19},
                        new List<int>() {4}
                    }
            });
            structure.Add(new BayesBranch
            {
                Layers = new List<List<int>>()
                    {
                        new List<int>() {38},
                        new List<int>() {30},
                        new List<int>() {27}
                    }
            });

            bayes.Clear();
            bayes.SetFileName("test4");
            bayes.CreateStructre(structure);
            bayes.PrintResult(bayes.GetResult(structure, new List<int>() { 1 }));
            LungCancerBayes.SendResultToFile(bayes.GetResult(structure, new List<int>() { 1 }), "test4");
            bayes.PrintResult(bayes.GetResult(structure, new List<int>() { 1, 2 }));
            LungCancerBayes.SendResultToFile(bayes.GetResult(structure, new List<int>() { 1, 2 }), "test4 2 layers");
            //##########################################################################Structure5
            structure = new List<BayesBranch>();

            structure.Add(new BayesBranch
            {
                Layers = new List<List<int>>()
                    {
                        new List<int>() {2},
                        new List<int>() {29}
                    }
            });

            structure.Add(new BayesBranch
            {
                Layers = new List<List<int>>()
                    {
                        new List<int>() {19},
                        new List<int>() {15}
                    }
            });

            structure.Add(new BayesBranch
            {
                Layers = new List<List<int>>()
                    {
                        new List<int>() {26},
                        new List<int>() {13}
                    }
            });

            bayes.Clear();
            bayes.SetFileName("test5");
            bayes.CreateStructre(structure);
            bayes.PrintResult(bayes.GetResult(structure, new List<int>() { 1 }));
            LungCancerBayes.SendResultToFile(bayes.GetResult(structure, new List<int>() { 1 }), "test5");
            bayes.PrintResult(bayes.GetResult(structure, new List<int>() { 1, 2 }));
            LungCancerBayes.SendResultToFile(bayes.GetResult(structure, new List<int>() { 1, 2 }), "test5 2 layers");
            //##########################################################################Structure6
            structure = new List<BayesBranch>();

            structure.Add(new BayesBranch
            {
                Layers = new List<List<int>>()
                    {
                        new List<int>() {29},
                        new List<int>() {2}

                    }
            });

            structure.Add(new BayesBranch
            {
                Layers = new List<List<int>>()
                    {
                        new List<int>() {15},
                        new List<int>() {19}

                    }
            });

            structure.Add(new BayesBranch
            {
                Layers = new List<List<int>>()
                    {
                        new List<int>() {13},
                        new List<int>() {26}

                    }
            });

            bayes.Clear();
            bayes.SetFileName("test6");
            bayes.CreateStructre(structure);
            bayes.PrintResult(bayes.GetResult(structure, new List<int>() { 1 }));
            LungCancerBayes.SendResultToFile(bayes.GetResult(structure, new List<int>() { 1 }), "test6");
            bayes.PrintResult(bayes.GetResult(structure, new List<int>() { 1, 2 }));
            LungCancerBayes.SendResultToFile(bayes.GetResult(structure, new List<int>() { 1, 2 }), "test6 2 layers");
            Console.ReadLine();
        }
    }
}
