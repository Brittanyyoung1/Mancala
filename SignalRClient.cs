using SS.Mancala.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS.Mancala.MAUI
{
    public class SignalRClient : SignalRConnection
    {
        public SignalRClient(string hubAddress) : base(hubAddress)
        {

        }

        public SignalRClient(string hubAddress, string owner) :
            base(hubAddress, owner)
        {

        }

    }
}
