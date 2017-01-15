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
                Int32 number = rand.Next(0, data.Count);
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
        static public void generateDataSegmentation(List<CancerData> data, int numberOfLearningData, int numberOfTestData)
        {
            List<CancerData> lDataTemp = copyToArray(data);
            List<CancerData> tDataTemp = copyToArray(data);
            testData = new List<CancerData>();
            learningData = new List<CancerData>();
            Random rand = new Random();
            do
            {
                Int32 number = rand.Next(0, lDataTemp.Count);
                CancerData d = lDataTemp.ElementAt(number);
                learningData.Add(d);
                lDataTemp.RemoveAt(number);
            } while (learningData.Count < numberOfLearningData);
            do
            {
                Int32 number = rand.Next(0, tDataTemp.Count);
                CancerData d = tDataTemp.ElementAt(number);
                testData.Add(d);
                tDataTemp.RemoveAt(number);
            } while (testData.Count < numberOfTestData);
        }
        static private List<CancerData> copyToArray(List<CancerData> data)
        {
            List<CancerData> result = new List<CancerData>();
            foreach(CancerData element in data)
            {
                result.Add(element);
            }
            return result;
        }
    }
}
