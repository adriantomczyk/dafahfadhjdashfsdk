using LungCancerBayesNetwork.Helpers;
using LungCancerBayesNetwork.Models;
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
        private List<BayesNode> BayesStructure { get; set; }
        //private List<string> AvailableStates { get; set; }
        private string FileName { get; set; }

        private List<string> StructureAttributes { get; set; }

        public LungCancerBayes(List<CancerData> cancerData)
        {
            Data = cancerData;
            this.Init();
        }

        public LungCancerBayes(List<CancerData> cancerData, List<BayesNode> bayesStructure)
        {
            Data = cancerData;
            BayesStructure = bayesStructure;
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

            StructureAttributes = new List<string>() { "p3", "p26", "p38", "p13", "p29", "p41", "p15", "p37", "p42", "c6", "c2", "c20", "c56", "c19" };
        }

        public void SetFileName(string fileName)
        {
            this.FileName = fileName;
        }

        public void Clear()
        {
            this.Net = new Network();
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
        private void SetNodeStateParent(string nodeId)
        {
            for (int i = 1; i <= 3; i++)
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
            double[] resultProbability;

            //Left
            parentProbability = Helper.CountProbabilityForParentVertex(Ldata, 3);
            Net.AddNode(Network.NodeType.Cpt, "p3");
            SetNodeStates("p3");
            Net.SetNodeDefinition("p3", parentProbability);

            parentProbability = Helper.CountProbabilityForParentVertex(Ldata, 26);
            Net.AddNode(Network.NodeType.Cpt, "p26");
            SetNodeStates("p26");
            Net.SetNodeDefinition("p26", parentProbability);

            parentProbability = Helper.CountProbabilityForParentVertex(Ldata, 38);
            Net.AddNode(Network.NodeType.Cpt, "p38");
            SetNodeStates("p38");
            Net.SetNodeDefinition("p38", parentProbability);

            //Middle
            parentProbability = Helper.CountProbabilityForParentVertex(Ldata, 13);
            Net.AddNode(Network.NodeType.Cpt, "p13");
            SetNodeStates("p13");
            Net.SetNodeDefinition("p13", parentProbability);

            parentProbability = Helper.CountProbabilityForParentVertex(Ldata, 29);
            Net.AddNode(Network.NodeType.Cpt, "p29");
            SetNodeStates("p29");
            Net.SetNodeDefinition("p29", parentProbability);

            parentProbability = Helper.CountProbabilityForParentVertex(Ldata, 41);
            Net.AddNode(Network.NodeType.Cpt, "p41");
            SetNodeStates("p41");
            Net.SetNodeDefinition("p41", parentProbability);

            //Right
            parentProbability = Helper.CountProbabilityForParentVertex(Ldata, 15);
            Net.AddNode(Network.NodeType.Cpt, "p15");
            SetNodeStates("p15");
            Net.SetNodeDefinition("p15", parentProbability);

            parentProbability = Helper.CountProbabilityForParentVertex(Ldata, 37);
            Net.AddNode(Network.NodeType.Cpt, "p37");
            SetNodeStates("p37");
            Net.SetNodeDefinition("p37", parentProbability);

            parentProbability = Helper.CountProbabilityForParentVertex(Ldata, 42);
            Net.AddNode(Network.NodeType.Cpt, "p42");
            SetNodeStates("p42");
            Net.SetNodeDefinition("p42", parentProbability);

            //Left
            childProbability = Helper.CountProbabilityDistributionForChildVertex(Ldata, 6, SetIndexes(3, 26, 38));
            Net.AddNode(Network.NodeType.Cpt, "c6");

            SetNodeStates("c6");
            Net.AddArc("p3", "c6");
            Net.AddArc("p26", "c6");
            Net.AddArc("p38", "c6");
            
            Net.SetNodeDefinition("c6", childProbability);

            childProbability = Helper.CountProbabilityDistributionForChildVertex(Ldata, 2, SetIndexes(3, 26, 38));
            Net.AddNode(Network.NodeType.Cpt, "c2");

            Net.AddArc("p3", "c2");
            Net.AddArc("p26", "c2");
            Net.AddArc("p38", "c2");

            SetNodeStates("c2");
            Net.SetNodeDefinition("c2", childProbability);

            //Middle
            childProbability = Helper.CountProbabilityDistributionForChildVertex(Ldata, 20, SetIndexes(13, 29, 41));
            Net.AddNode(Network.NodeType.Cpt, "c20");

            Net.AddArc("p13", "c20");
            Net.AddArc("p29", "c20");
            Net.AddArc("p41", "c20");

            SetNodeStates("c20");
            Net.SetNodeDefinition("c20", childProbability);

            childProbability = Helper.CountProbabilityDistributionForChildVertex(Ldata, 56, SetIndexes(13, 29, 41));
            Net.AddNode(Network.NodeType.Cpt, "c56");

            Net.AddArc("p13", "c56");
            Net.AddArc("p29", "c56");
            Net.AddArc("p41", "c56");

            SetNodeStates("c56");
            Net.SetNodeDefinition("c56", childProbability);

            //Right
            childProbability = Helper.CountProbabilityDistributionForChildVertex(Ldata, 19, SetIndexes(15, 37, 42));
            Net.AddNode(Network.NodeType.Cpt, "c19");

            Net.AddArc("p15", "c19");
            Net.AddArc("p37", "c19");
            Net.AddArc("p42", "c19");

            SetNodeStates("c19");
            Net.SetNodeDefinition("c19", childProbability);

            //Result
            List<int> indexes = new List<int>() { 6, 2, 20, 56, 19 };
            resultProbability = Helper.CountProbabilityDistributionForResult(Ldata,indexes);
            Net.AddNode(Network.NodeType.Cpt, "result");

            Net.AddArc("c6", "result");
            Net.AddArc("c2", "result");
            Net.AddArc("c20", "result");
            Net.AddArc("c56", "result");
            Net.AddArc("c19", "result");

            SetNodeStateParent("result");
            Net.SetNodeDefinition("result", resultProbability);


            Net.WriteFile("lungcancer.xdsl");
        }

        public void CreateStructre(List<BayesNode> BayesStructure)
        {
            double[] probability;
            List<int> AddedVertexes = new List<int>();

            foreach (BayesNode node in BayesStructure)
            {
                for (int i = 0; i < node.Layers.Count; ++i)
                {
                    for (int j = 0; j < node.Layers[i].Count; ++j)
                    {
                        if (i == 0)
                        {
                            probability = Helper.CountProbabilityForParentVertex(Ldata, node.Layers[i][j]);
                            Net.AddNode(Network.NodeType.Cpt, "n" + node.Layers[i][j].ToString());
                            SetNodeStates("n" + node.Layers[i][j].ToString());
                            Net.SetNodeDefinition("n" + node.Layers[i][j].ToString(), probability);
                        }
                        else
                        {
                            probability = Helper.CountProbabilityDistributionForChildVertex(Ldata, 6, node.Layers[i - 1]);
                            Net.AddNode(Network.NodeType.Cpt, "n" + node.Layers[i][j].ToString());
                            SetNodeStates("n" + node.Layers[i][j].ToString());
                            foreach (int vertex in node.Layers[i - 1])
                            {
                                Net.AddArc("n" + vertex.ToString(), "n" + node.Layers[i][j].ToString());
                            }
                        }
                    }
                }
            }



            //Result
            List<int> indexes = new List<int>();


            Net.AddNode(Network.NodeType.Cpt, "result2");

            foreach (BayesNode node in BayesStructure)
            {
                foreach (int vertex in node.Layers[node.Layers.Count-1])
                {
                    indexes.Add(vertex);
                    Net.AddArc("n" + vertex.ToString(), "result2");
                }
            }


            probability = Helper.CountProbabilityDistributionForResult(Ldata, indexes);
            SetNodeStateParent("result2");
            Net.SetNodeDefinition("result2", probability);
            Net.WriteFile(FileName + ".xdsl");
        }


        /*
        public List<BayesResult> GetResults()
        {
            List<BayesResult> result = new List<BayesResult>();
            Net.UpdateBeliefs();
            foreach (BayesStructureSchema child in Schema)
            {
                for (int i = 0; i < 4; i++)
                {
                    Net.SetEvidence(child.ChildId, "s" + i.ToString());
                    Net.UpdateBeliefs();
                    foreach (string parent in child.Parents)
                    {
                        for (int j = 0; j < 3; ++j)
                        {                           //var f = Net.GetNodeValue(parent);
                            double probability = (Net.GetNodeValue("result"))[j];
                            result.Add(new BayesResult(parent, "s" + j.ToString(), child.ChildId, "s" + i.ToString(), probability));
                        }
                    }
                    Net.ClearEvidence(child.ChildId);
                }

            }
            return result;
        }
        */
        public BayesResult2 GetResult()
        {
            BayesResult2 result = new BayesResult2();
            List<double> probabilities = new List<double>();

            Net.UpdateBeliefs();
            foreach (CancerData testData in this.Tdata)
            {
                Net.ClearAllEvidence();
                foreach (string attr in StructureAttributes)
                {
                    int idx = this.AttritubeToIndex(attr);
                    if (testData.attributes[idx] > -1 && attr.IndexOf('p') >= 0)
                    {
                        List<double> test = new List<double>();

                        test.AddRange(Net.GetNodeValue("result"));
                        Net.SetEvidence(attr, "s" + testData.attributes[idx]);
                        Net.UpdateBeliefs();

                        test.AddRange(Net.GetNodeValue("result"));
                         test.Clear();
                    }
                }
                probabilities.AddRange(Net.GetNodeValue("result"));
                Console.WriteLine("Result:" + probabilities.ElementAt(0) + " " + probabilities.ElementAt(1) + " " + probabilities.ElementAt(2));
                int cancerClass = this.ClassForMaxElement(probabilities);
                if (cancerClass == testData.cancerClass)
                {
                    result.Good++;
                }
                else
                {
                    result.Bad++;
                }

                probabilities.Clear();
            }

            return result;
        }

        public BayesResult2 GetResult(List<BayesNode> BayesStructure)
        {
            BayesResult2 result = new BayesResult2();
            List<double> probabilities = new List<double>();

            Net.UpdateBeliefs();
            foreach (CancerData testData in this.Tdata)
            {
                Net.ClearAllEvidence();
                foreach (BayesNode node in BayesStructure)
                {
                    foreach (int vertex in node.Layers[0])
                    {
                        int idx = vertex - 1;
                        if (testData.attributes[idx] > -1)
                        {
                            List<double> test = new List<double>();

                            test.AddRange(Net.GetNodeValue("result2"));
                            Net.SetEvidence("n" + vertex.ToString(), "s" + testData.attributes[idx]);
                            Net.UpdateBeliefs();

                            test.AddRange(Net.GetNodeValue("result2"));
                            test.Clear();
                        }
                    }
                }
                probabilities.AddRange(Net.GetNodeValue("result2"));
                Console.WriteLine("Result:" + probabilities.ElementAt(0) + " " + probabilities.ElementAt(1) + " " + probabilities.ElementAt(2));
                int cancerClass = this.ClassForMaxElement(probabilities);
                if (cancerClass == testData.cancerClass)
                {
                    result.Good++;
                }
                else
                {
                    result.Bad++;
                }

                probabilities.Clear();
            }

            return result;
        }

        private int ClassForMaxElement(List<double> probabilities)
        {
            if (probabilities == null || probabilities.Count < 1)
            {
                return -1;
            }

            int idx = 0;
            double maxValue = probabilities[0];
            for (int i = 1; i < probabilities.Count; ++i)
            {
                if (probabilities[i] > maxValue)
                {
                    maxValue = probabilities[i];
                    idx = i;
                }
            }

            return ((idx%3)+1);
        }

        private int AttritubeToIndex(string attr)
        {
            int result;
            Int32.TryParse(attr.Substring(1), out result);
            return (result - 1);
        }
        /*
        public void PrintResult(List<BayesResult> result)
        {
            foreach(BayesResult b in result)
            {
                Console.WriteLine(b.ToString());
            }
        }
        */
        public void PrintResult(BayesResult2 result)
        {
            Console.WriteLine("Good: {0}, Bad: {1}", result.Good, result.Bad);
        }


        private List<int> SetIndexes(int idx1, int idx2, int idx3)
        {
            return new List<int> { idx1, idx2, idx3 };
        }

    }

}
