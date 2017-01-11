using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungCancerBayesNetwork
{
    public class BayesStructureSchema
    {
        public string ChildId { get; set; }
        public List<string> Parents { get; set; }

        public BayesStructureSchema(string childId, List<string> parents)
        {
            this.ChildId = childId;
            this.Parents = parents;
        }
    }
}
