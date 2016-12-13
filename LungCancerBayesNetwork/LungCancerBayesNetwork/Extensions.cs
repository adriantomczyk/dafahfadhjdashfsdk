using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungCancerBayesNetwork
{
    public static class Extensions
    {
        public static List<int> Clone(this List<int> list)
        {
            return new List<int>(list);
        }
    }
}
