using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Application
{
    public class StockURLS
    {
        public List<Tuple<string, int>> URLPORTS = new List<Tuple<string, int>>();

        public StockURLS()
        {
            Tuple<string, int> RussiAOrB = new Tuple<string, int>("ec2-35-164-218-97.us-west-2.compute.amazonaws.com", 1234);
            Tuple<string, int> Vatikan = new Tuple<string, int>("ec2-54-93-94-230.eu-central-1.compute.amazonaws.com", 80);
            Tuple<string, int> Niemandsland = new Tuple<string, int>("boerse.pwnhofer.at", 443);
            Tuple<string, int> Neuseeland = new Tuple<string, int>("boerse.windhound.at", 80);
            Tuple<string, int> Mexiko = new Tuple<string, int>("ec2-35-156-47-142.eu-central-1.compute.amazonaws.com", 8080);
            Tuple<string, int> BestKorea = new Tuple<string, int>("ec2-35-156-40-8.eu-central-1.compute.amazonaws.com", 80);
            Tuple<string, int> Bhutan = new Tuple<string, int>("ec2-35-164-36-173.us-west-2.compute.amazonaws.com", 80);
            URLPORTS.Add(RussiAOrB);
            URLPORTS.Add(Vatikan);
            URLPORTS.Add(Niemandsland);
            URLPORTS.Add(Neuseeland);
            URLPORTS.Add(Mexiko);
            URLPORTS.Add(BestKorea);
            URLPORTS.Add(Bhutan);
        }


        
    }
}
