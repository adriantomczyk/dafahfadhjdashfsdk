using System;
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
            List<CancerData> data = getDataFromFile("lung-cancer.data.txt");

            LungCancerBayes bayes = new LungCancerBayes(data);
            bayes.CreateStructre();
            
            
            /*DataSegmentator.generateDataSegmentation(data, 0.2);
            List<CancerData> ldata = DataSegmentator.learningData;
            List<CancerData> tdata = DataSegmentator.testData;
            System.Console.WriteLine(tdata.ElementAt(0));
            System.Console.WriteLine(tdata.ElementAt(1));
            System.Console.WriteLine(tdata.ElementAt(2));
            Int32[] indexes = { 3,26,38 };
            double [] result = CancerData.countProbabilityDistributionForChildVertex(tdata, 6, indexes);
            for (int i=0;i<1;i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        System.Console.Write(result[i * 16 + j * 4 + k] + " ");
                    }
                    System.Console.Write("|");
                }
                System.Console.WriteLine();
            }
            result = CancerData.countProbabilityForParentVertex(tdata,5);
            for(int i = 0; i < 4; i++)
            {
                System.Console.Write(result[i] + " ");
            }
            System.Console.ReadKey();
            //LibraryTest libTest = new LibraryTest();
            //libTest.JustTest();
            //System.Console.Write(getDataFromFile("lung-cancer.data.txt").ElementAt(20).ToString());
            //System.Console.ReadKey();*/
        }
    }
}
