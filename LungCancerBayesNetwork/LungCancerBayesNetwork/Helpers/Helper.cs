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
        public static double[] CountProbabilityDistributionForChildVertex(List<CancerData> data, Int32 elementIndex, List<int> childAttributesIndexes)
        {
            double[] result = new double[(int)Math.Pow(4, childAttributesIndexes.Count + 1)];
            foreach (CancerData element in data)
            {
                int sum = element.attributes[elementIndex - 1] * (int)Math.Pow(4, childAttributesIndexes.Count);
                for (int i = 0; i < childAttributesIndexes.Count; i++)
                {
                    sum += element.attributes[childAttributesIndexes[i] - 1] * (int)Math.Pow(4, childAttributesIndexes.Count - 1 - i);
                }
                if (sum >= 0) result[sum]++;
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

        public static double[] CountProbabilityDistributionForResult(List<CancerData> data, List<int> childAttributesIndexes)
        {
            double[] result = new double[3 * (int)Math.Pow(4, childAttributesIndexes.Count)];
            foreach (CancerData element in data)
            {
                int sum = (element.cancerClass - 1) * 3 * (int)Math.Pow(4, childAttributesIndexes.Count - 1);
                for (int i = 0; i < childAttributesIndexes.Count; i++)
                {
                    sum += element.attributes[childAttributesIndexes[i] - 1] * (int)Math.Pow(4, childAttributesIndexes.Count - i - 1);
                }
                if (sum >= 0) result[sum]++;
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
