using System;
using System.Diagnostics.Eventing.Reader;
using System.Xml;

namespace VisualDebugger
{
	public class SlotStateParser
	{
		private String xmlPath;

        public SlotStateParser(String xmlPath)
		{
			this.xmlPath = xmlPath;
		}

		public List<TabInfo> Parse()
		{
			List<TabInfo> tabInfos = new List<TabInfo>();

			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(xmlPath);
			XmlNodeList states = xmlDocument.SelectNodes("states/state");
			if (states == null )
			{
				throw new Exception("No state found!");
			}

			foreach (XmlNode state in states)
			{
				TabInfo newTabInfo = new TabInfo();

				newTabInfo.Name = state.SelectSingleNode("name").InnerText;

				XmlNode screen = state.SelectSingleNode("screen");
				int row = 0;
				foreach (XmlNode child in screen.ChildNodes)
				{
					newTabInfo.Slot.Add(new List<String>());
					String[] col = child.InnerText.Split(',');
					for (int i = 0; i < col.Length; i++)
					{
                        newTabInfo.Slot[row].Add(col[i]);
					}
					row++;
				}
				
				XmlNode sticky = state.SelectSingleNode("sticky");
				row = 0;
				foreach (XmlNode child in sticky.ChildNodes)
				{
					newTabInfo.Sticky.Add(new List<String>());
					String[] col = child.InnerText.Split(',');
					for (int i = 0; i < col.Length; i++)
					{
                        newTabInfo.Sticky[row].Add(col[i]);
					}
					row++;
				}
				
				XmlNode mask = state.SelectSingleNode("mask");
				row = 0;
				foreach (XmlNode child in mask.ChildNodes)
				{
					newTabInfo.Mask.Add(new List<String>());
					String[] col = child.InnerText.Split(',');
					for (int i = 0; i < col.Length; i++)
					{
                        newTabInfo.Mask[row].Add(col[i]);
					}
					row++;
				}
				
				XmlNode wild = state.SelectSingleNode("wild-mask");
				row = 0;
				foreach (XmlNode child in wild.ChildNodes)
				{
					newTabInfo.WildMask.Add(new List<String>());
					String[] col = child.InnerText.Split(',');
					for (int i = 0; i < col.Length; i++)
					{
                        newTabInfo.WildMask[row].Add(col[i]);
					}
					row++;
				}

				XmlNode XmlPaylines = state.SelectSingleNode("wins");
				uint id = 0;
				String[] mandatoryInfo = { "type", "symbol", "pos" };
				foreach (XmlNode payline in XmlPaylines.ChildNodes)
				{
					String symbol = payline["symbol"].InnerText;
					String type = payline["type"].InnerText;

					List<Tuple<String, String>> additionalInfo = new List<Tuple<String, String>>();
					foreach (XmlNode child in payline.ChildNodes)
					{
						if (!Array.Exists(mandatoryInfo, elem => elem == child.Name))
							additionalInfo.Add(new Tuple<String, String>(child.Name, child.InnerText));
					}

					List<Tuple<int, int>> positions = new List<Tuple<int, int>>();
					XmlNodeList rows = payline.SelectNodes("pos/ps");
					for (int i = 0; i < rows.Count; i++)
					{
						String[] pos_index = rows[i].InnerText.Split(',');
						positions.Add(new Tuple<int, int>(Int32.Parse(pos_index[0]), Int32.Parse(pos_index[1])));
					}

					Winning p = new Winning(id, type, symbol, additionalInfo, positions);
					newTabInfo.Winnings.Add(p);
					id++;
				}

				tabInfos.Add(newTabInfo);
			}

			return tabInfos;
        }
	}

}
