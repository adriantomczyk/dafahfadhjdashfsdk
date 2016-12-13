using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungCancerBayesNetwork
{
    class CancerData
    {
        public CancerData(Int16 cancerClass, Int16 [] attributes)
        {
            this.cancerClass = cancerClass;
            this.attributes = attributes;
        }

        private Int16 cancerClass { get; set; }
        private Int16 [] attributes { get; set;}

        public static double [] countProbabilityDistributionForChildVertex(List<CancerData> data, Int32 elementIndex, Int32 [] childAttributesIndexes)
        {
            double[] result = new double[4 * 4 * childAttributesIndexes.Length];
            foreach(CancerData element in data)
            {
                Int32 attrIndex = 0;
                foreach (Int32 index in childAttributesIndexes)
                {
                    Int32 elementValue = element.attributes[elementIndex];
                    Int32 attributeValue = element.attributes[index];
                    if (elementValue>=0 && attributeValue>= 0)
                    {
                        result[(attrIndex*16)+(attributeValue*4)+elementValue]++;
                    }
                    attrIndex++;
                }
            }
            for(int i = 0; i < result.Length; i++)
            {
                result[i] /= data.Count;
            }
            return result;
        }
        public static double[] countProbabilityForParentVertex(List<CancerData> data, Int32 elementIndex)
        {
            double[] result = new double[4];
            foreach (CancerData element in data)
            {
                if (element.attributes[elementIndex] >= 0)
                {
                    result[element.attributes[elementIndex]]++;
                }
            }
            for (int i = 0; i < result.Length; i++)
            {
                result[i] /= data.Count;
            }
            return result;
        }
        public override String ToString()
        {
            String result = "";
            result += "Class: " + cancerClass.ToString()+"\n";
            result += "Attributes: " + String.Join(",",attributes);
            return result;
        }
    }
}
