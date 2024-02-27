using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualDebugger
{

    public class Winning
    {
        public uint id;
        public String type;
        public String symbol;
        public List<Tuple<String, String>> additionalInfo;
        public List<Tuple<int, int>> winningPos;

        public Winning(uint id, String type, String symbol, List<Tuple<String, String>> additionalInfo, List<Tuple<int, int>> winningPos)
        {
            this.id = id;
            this.type = type;
            this.symbol = symbol;
            this.additionalInfo = additionalInfo;
            this.winningPos = winningPos;
        }


    }

    public class TabInfo
    {
        public String Name;
        public List<List<String>> Slot = new List<List<String>>();
        public List<List<String>> Sticky = new List<List<String>>();
        public List<List<String>> Mask = new List<List<String>>();
        public List<List<String>> WildMask = new List<List<String>>();
        public List<Winning> Winnings = new List<Winning>();
    }

}
