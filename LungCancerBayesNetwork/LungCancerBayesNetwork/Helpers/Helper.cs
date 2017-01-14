using LungCancerBayesNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungCancerBayesNetwork.Helpers
{
    public static class Helper
    {
        public static double[] CountProbabilityDistributionForChildVertex(List<CancerData> data, Int32 elementIndex, Int32[] childAttributesIndexes)
        {
            double[] result = new double[(int)Math.Pow(4, childAttributesIndexes.Length + 1)];
            foreach (CancerData element in data)
            {
                //Int32 attrIndex = 0;
                //foreach (Int32 index in childAttributesIndexes)
                //{
                //    Int32 elementValue = element.attributes[elementIndex-1];
                //    Int32 attributeValue = element.attributes[index-1];
                //    if (elementValue>=0 && attributeValue>= 0)
                //    {
                //        result[]++;
                //        result[(attrIndex * 16) + (attributeValue * 4) + elementValue]++;
                //    }
                //    attrIndex++;
                //}
                int x = element.attributes[elementIndex - 1] * 64;
                int y = element.attributes[childAttributesIndexes[0] - 1] * 16;
                int z = element.attributes[childAttributesIndexes[1] - 1] * 4;
                int t = element.attributes[childAttributesIndexes[2] - 1];
                if (x > 0 && y > 0 && z > 0 && t > 0) result[x + y + z + t]++;
            }
            for (int i = 0; i < result.Length; i++)
            {
                result[i] /= data.Count;
            }
            for (int i = 0; i < result.Length; i++)
            {
                result[i] += 0.01;
            }
            return result;
        }

        public static double[] CountProbabilityDistributionForResult(List<CancerData> data, Int32[] childAttributesIndexes)
        {
            double[] result = new double[3 * (int)Math.Pow(4, childAttributesIndexes.Length)];
            foreach (CancerData element in data)
            {
                int x = (element.cancerClass - 1) * 48;
                int y = element.attributes[childAttributesIndexes[0] - 1] * 16;
                int z = element.attributes[childAttributesIndexes[1] - 1] * 4;
                int t = element.attributes[childAttributesIndexes[2] - 1];
                if (x > 0 && y > 0 && z > 0 && t > 0) result[x + y + z + t]++;
                //    Int32 attrIndex = 0;
                //    foreach (Int32 index in childAttributesIndexes)
                //    {
                //        Int32 elementValue = element.cancerClass;
                //        Int32 attributeValue = element.attributes[index-1];
                //        if (elementValue >= 0 && attributeValue >= 0)
                //        {
                //            result[(attrIndex * 12) + (attributeValue * 4) + elementValue]++;
                //        }
                //        attrIndex++;
                //    }
            }

            for (int i = 0; i < result.Length; i++)
            {
                result[i] /= data.Count;
            }
            for (int i = 0; i < result.Length; i++)
            {
                result[i] += 0.01;
            }
            return result;
        }

        public static double[] CountProbabilityForParentVertex(List<CancerData> data, Int32 elementIndex)
        {
            double[] result = new double[4];
            foreach (CancerData element in data)
            {
                if (element.attributes[elementIndex - 1] >= 0)
                {
                    result[element.attributes[elementIndex - 1]]++;
                }
            }
            for (int i = 0; i < result.Length; i++)
            {
                result[i] /= data.Count;
            }
            for (int i = 0; i < result.Length; i++)
            {
                result[i] += 0.01;
            }
            return result;
        }
    }
}
