using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Application
{
    /// <summary>
    /// Represents a check object which gets json serialized for polling responds to a buy or sell request
    /// </summary>
    class Check
    {
        /// <summary>
        /// ID of the order for which a responds gets polled
        /// </summary>
        public string orderID;

        /// <summary>
        /// Used for additional excercise
        /// </summary>
        public string hash = string.Empty;

        /// <summary>
        /// Constructor for filling object with guid
        /// Hash will stay string.empty for now
        /// </summary>
        /// <param name="tmpGuid"></param>
        public Check(string tmpGuid)
        {
            orderID = tmpGuid;
        }
    }
}
