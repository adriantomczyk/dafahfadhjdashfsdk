using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungCancerBayesNetwork.Models
{
    public class CancerData
    {
        public CancerData(Int16 cancerClass, Int16 [] attributes)
        {
            this.cancerClass = cancerClass;
            this.attributes = attributes;
        }

        public Int16 cancerClass { get; set; }
        public Int16 [] attributes { get; set;}

        public override String ToString()
        {
            String result = "";
            result += "Class: " + cancerClass.ToString()+"\n";
            result += "Attributes: " + String.Join(",",attributes);
            return result;
        }
    }
}
