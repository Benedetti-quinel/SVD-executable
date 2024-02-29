using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    internal class Winning
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

    internal class TabInfo
    {
        public String Name;
        public String Reelset;
        public List<String> Stops = new List<string>();
        public List<List<String>> Slot = new List<List<String>>();
        public List<List<String>> Sticky = new List<List<String>>();
        public List<List<String>> Mask = new List<List<String>>();
        public List<List<String>> WildMask = new List<List<String>>();
        public List<Winning> Winnings = new List<Winning>();

    }

    public class Slot
    {
        public List<List<Label>> cells = new List<List<Label>>();
    }

    public class DataTable
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public DataTable(string Name, string Value) {
            this.Name = Name;
            this.Value = Value;
        }
    }
}
