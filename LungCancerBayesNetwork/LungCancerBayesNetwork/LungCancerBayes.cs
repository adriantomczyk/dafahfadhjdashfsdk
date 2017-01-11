using Smile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungCancerBayesNetwork
{
    public class LungCancerBayes
    {
        private List<CancerData> Data { get; set; }
        private List<CancerData> Ldata { get; set; }
        private List<CancerData> Tdata { get; set; }
        private Network Net { get; set; }
        private List<BayesStructureSchema> Schema { get; set; }
        //private List<string> AvailableStates { get; set; }

        public LungCancerBayes(List<CancerData> cancerData)
        {
            Data = cancerData;
            this.Init();
        }

        private void Init()
        {
            DataSegmentator.generateDataSegmentation(this.Data, 0.2);
            this.Ldata = DataSegmentator.learningData;
            this.Tdata = DataSegmentator.testData;
            this.Net = new Network();

            Schema = new List<BayesStructureSchema>();
            Schema.Add(new BayesStructureSchema("c6", new List<string>() { "p3", "p26", "p38" }));
            Schema.Add(new BayesStructureSchema("c2", new List<string>() { "p3", "p26", "p38" }));

            Schema.Add(new BayesStructureSchema("c20", new List<string>() { "p13", "p29", "p41" }));
            Schema.Add(new BayesStructureSchema("c56", new List<string>() { "p13", "p29", "p41" }));

            Schema.Add(new BayesStructureSchema("c19", new List<string>() { "p15", "p37", "p42" }));

            //AvailableStates = new List<string>() { "s0", "s1", "s2", "s3"};
        }

        private void SetNodeStates(string nodeId)
        {
            for (int i = 0; i < 4; i++)
            {
                Net.AddOutcome(nodeId, "s" + i.ToString());
            }
            Net.DeleteOutcome(nodeId, 0);
            Net.DeleteOutcome(nodeId, 0);
        }

        public void CreateStructre()
        {
            double[] childProbability;
            double[] parentProbability;

            //Left
            parentProbability = CancerData.countProbabilityForParentVertex(Ldata, 3);
            Net.AddNode(Network.NodeType.Cpt, "p3");
            SetNodeStates("p3");
            Net.SetNodeDefinition("p3", parentProbability);

            parentProbability = CancerData.countProbabilityForParentVertex(Ldata, 26);
            Net.AddNode(Network.NodeType.Cpt, "p26");
            SetNodeStates("p26");
            Net.SetNodeDefinition("p26", parentProbability);

            parentProbability = CancerData.countProbabilityForParentVertex(Ldata, 38);
            Net.AddNode(Network.NodeType.Cpt, "p38");
            SetNodeStates("p38");
            Net.SetNodeDefinition("p38", parentProbability);

            //Middle
            parentProbability = CancerData.countProbabilityForParentVertex(Ldata, 13);
            Net.AddNode(Network.NodeType.Cpt, "p13");
            SetNodeStates("p13");
            Net.SetNodeDefinition("p13", parentProbability);

            parentProbability = CancerData.countProbabilityForParentVertex(Ldata, 29);
            Net.AddNode(Network.NodeType.Cpt, "p29");
            SetNodeStates("p29");
            Net.SetNodeDefinition("p29", parentProbability);

            parentProbability = CancerData.countProbabilityForParentVertex(Ldata, 41);
            Net.AddNode(Network.NodeType.Cpt, "p41");
            SetNodeStates("p41");
            Net.SetNodeDefinition("p41", parentProbability);

            //Right
            parentProbability = CancerData.countProbabilityForParentVertex(Ldata, 15);
            Net.AddNode(Network.NodeType.Cpt, "p15");
            SetNodeStates("p15");
            Net.SetNodeDefinition("p15", parentProbability);

            parentProbability = CancerData.countProbabilityForParentVertex(Ldata, 37);
            Net.AddNode(Network.NodeType.Cpt, "p37");
            SetNodeStates("p37");
            Net.SetNodeDefinition("p37", parentProbability);

            parentProbability = CancerData.countProbabilityForParentVertex(Ldata, 42);
            Net.AddNode(Network.NodeType.Cpt, "p42");
            SetNodeStates("p42");
            Net.SetNodeDefinition("p42", parentProbability);

            //Left
            childProbability = CancerData.countProbabilityDistributionForChildVertex(Ldata, 6, SetIndexes(3, 26, 38));
            Net.AddNode(Network.NodeType.Cpt, "c6");

            SetNodeStates("c6");
            Net.AddArc("p3", "c6");
            Net.AddArc("p26", "c6");
            Net.AddArc("p38", "c6");
            
            Net.SetNodeDefinition("c6", childProbability);

            childProbability = CancerData.countProbabilityDistributionForChildVertex(Ldata, 2, SetIndexes(3, 26, 38));
            Net.AddNode(Network.NodeType.Cpt, "c2");

            Net.AddArc("p3", "c2");
            Net.AddArc("p26", "c2");
            Net.AddArc("p38", "c2");

            SetNodeStates("c2");
            Net.SetNodeDefinition("c2", childProbability);

            //Middle
            childProbability = CancerData.countProbabilityDistributionForChildVertex(Ldata, 20, SetIndexes(13, 29, 41));
            Net.AddNode(Network.NodeType.Cpt, "c20");

            Net.AddArc("p13", "c20");
            Net.AddArc("p29", "c20");
            Net.AddArc("p41", "c20");

            SetNodeStates("c20");
            Net.SetNodeDefinition("c20", childProbability);

            childProbability = CancerData.countProbabilityDistributionForChildVertex(Ldata, 56, SetIndexes(13, 29, 41));
            Net.AddNode(Network.NodeType.Cpt, "c56");

            Net.AddArc("p13", "c56");
            Net.AddArc("p29", "c56");
            Net.AddArc("p41", "c56");

            SetNodeStates("c56");
            Net.SetNodeDefinition("c56", childProbability);

            //Right
            childProbability = CancerData.countProbabilityDistributionForChildVertex(Ldata, 19, SetIndexes(15, 37, 42));
            Net.AddNode(Network.NodeType.Cpt, "c19");

            Net.AddArc("p15", "c19");
            Net.AddArc("p37", "c19");
            Net.AddArc("p42", "c19");

            SetNodeStates("c19");
            Net.SetNodeDefinition("c19", childProbability);


            Net.WriteFile("lungcancer.xdsl");
        }

        public List<BayesResult> GetResults()
        {
            List<BayesResult> result = new List<BayesResult>();
            Net.UpdateBeliefs();
            foreach (BayesStructureSchema child in Schema)
            {
                for(int i=0; i<4; i++)
                {
                    Net.SetEvidence(child.ChildId, "s" + i.ToString());
                    Net.UpdateBeliefs();
                    foreach (string parent in child.Parents)
                    {
                        for(int j=0; j<4; ++j)
                        {
                            //var f = Net.GetNodeValue(parent);
                            double probability = (Net.GetNodeValue(parent))[j];
                            result.Add(new BayesResult(parent, "s" + j.ToString(), child.ChildId, "s" + i.ToString(), probability));
                        }
                    }
                    Net.ClearEvidence(child.ChildId);
                }

            }
            return result; 
        }

        public void PrintResult(List<BayesResult> result)
        {
            foreach(BayesResult b in result)
            {
                Console.WriteLine(b.ToString());
            }
        }




        private Int32[] SetIndexes(int idx1, int idx2, int idx3)
        {
            return new Int32[] { idx1, idx2, idx3 };
        }
    }

}
