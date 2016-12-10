using Smile;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungCancerBayesNetwork
{
    public class LibraryTest
    {
        public void JustTest()
        {
            Network net = new Network();
            net.AddNode(Network.NodeType.Cpt, "Success");
            net.SetOutcomeId("Success", 0, "Success");
            net.SetOutcomeId("Success", 1, "Failure");
            net.AddNode(Network.NodeType.Cpt, "Forecast");
            net.AddOutcome("Forecast", "Good");
            net.AddOutcome("Forecast", "Moderate");
            net.AddOutcome("Forecast", "Poor");
            net.DeleteOutcome("Forecast", 0);
            net.DeleteOutcome("Forecast", 0);
            net.AddArc("Success", "Forecast");
            double[] aSuccessDef = { 0.2, 0.8 };
            net.SetNodeDefinition("Success", aSuccessDef);
            double[] aForecastDef = { 0.4, 0.4, 0.2, 0.1, 0.3, 0.6 };
            net.SetNodeDefinition("Forecast", aForecastDef);

            // Changing the nodes' spacial and visual attributes:
            net.SetNodePosition("Success", 20, 20, 80, 30);
            net.SetNodeBgColor("Success", Color.Tomato);
            net.SetNodeTextColor("Success", Color.White);
            net.SetNodeBorderColor("Success", Color.Black);
            net.SetNodeBorderWidth("Success", 2);
            net.SetNodePosition("Forecast", 30, 100, 60, 30);

            net.WriteFile("tutorial_a.xdsl");
        }
    }
}
