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
        private Int16 cancerClass { get; }
        private Int16 [] attributes { get; }
        public String ToString()
        {
            String result = "";
            result += "Class: " + cancerClass.ToString()+"\n";
            result += "Attributes: " + String.Join(",",attributes);
            return result;
        }
    }
}
