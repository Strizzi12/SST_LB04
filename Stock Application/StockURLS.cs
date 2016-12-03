using System;
using System.Collections.Generic;

namespace Stock_Application
{
    /// <summary>
    /// Class for storing the static strings of the URls
    /// </summary>
    public class StockURLS
    {
        /// <summary>
        /// Creates list of available URLs
        /// </summary>
        public static List<string> getStockURLS()
        {
            List<string> tmpList = new List<string>();

            string RussiAOrB = "http://ec2-35-164-218-97.us-west-2.compute.amazonaws.com:8080/";
            string Vatikan = "http://ec2-54-93-94-230.eu-central-1.compute.amazonaws.com:80/ ";
            string Niemandsland = "https://boerse.pwnhofer.at:443/";
            string Neuseeland = "http://boerse.windhound.at:80/";
            string Mexiko = "http://ec2-35-156-47-142.eu-central-1.compute.amazonaws.com:8080/awsServer/";
            string BestKorea = "http://ec2-35-156-40-8.eu-central-1.compute.amazonaws.com:80/";
            string Bhutan = "http://ec2-35-164-36-173.us-west-2.compute.amazonaws.com:80/";
            string Oregon = "http://ec2-35-165-43-113.us-west-2.compute.amazonaws.com:80/";

            tmpList.Add(RussiAOrB);
            tmpList.Add(Vatikan);
            tmpList.Add(Niemandsland);
            tmpList.Add(Neuseeland);
            tmpList.Add(Mexiko);
            tmpList.Add(BestKorea);
            tmpList.Add(Bhutan);
            tmpList.Add(Oregon);

            return tmpList;
        }
    }
}
