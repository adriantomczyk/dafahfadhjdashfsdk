using LungCancerBayesNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungCancerBayesNetwork.Helpers
{
    class DataSegmentator
    {
        static public List<CancerData> learningData { get; private set; }
        static public List<CancerData> testData { get; private set; }
        static public void generateDataSegmentation(List<CancerData> data, double proportions)
        {
            learningData = new List<CancerData>();
            testData = new List<CancerData>();
            Random rand = new Random();
            do
            {
                Int32 number = rand.Next(0,data.Count);
                CancerData d = data.ElementAt(number);
                if (learningData.Count > 0)
                {
                    double dataProportions = (double)testData.Count / (double)learningData.Count;
                    if (dataProportions > proportions)
                    {
                        learningData.Add(d);
                    }
                    else
                    {
                        testData.Add(d);
                    }
                }
                else
                {
                    learningData.Add(d);
                }
                data.RemoveAt(number);
            } while (learningData.Count + testData.Count < data.Count);
        } 
    }
}
