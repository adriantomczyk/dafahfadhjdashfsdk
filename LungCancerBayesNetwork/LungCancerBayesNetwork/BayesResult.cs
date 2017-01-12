using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungCancerBayesNetwork
{
    public class BayesResult
    {
        public double Probability { get; set; }
        public string ParentNodeId { get; set; }
        public string ParentStateId { get; set; }
        public string ChildNodeId { get; set; }
        public string ChildStateId { get; set; }

        public BayesResult(string parentId, string parentState, string childId, string childState, double probability)
        {
            this.ParentNodeId = parentId;
            this.ParentStateId = parentState;
            this.ChildNodeId = childId;
            this.ChildStateId = childState;
            this.Probability = probability;
        }

        public override string ToString()
        {
 	        return "P(" + ParentNodeId + "=" + ParentStateId + " | " + ChildNodeId + "=" + ChildStateId + ") = " + Probability;
        }
        
    }

    public class BayesResult2
    {
        public int Good { get; set; }
        public int Bad { get; set; }
    }
}
