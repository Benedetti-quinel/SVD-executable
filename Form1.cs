using Microsoft.VisualBasic.ApplicationServices;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection.Metadata;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Xml;
using System.Runtime.InteropServices;

namespace VisualDebugger
{
    public partial class Form1 : Form
    {
        Color baseColor = Color.FromArgb(34, 36, 43);
        Color freeCellColor = Color.FromArgb(26, 60, 72);
        Color freeCellFontColor = Color.White;
        Color winningCellColor = Color.FromArgb(80, 0, 17);
        Color winningCellFontColor = Color.FromArgb(255,195,0);

        List<TabInfo> tabs;

        public Form1(String xmlPath = "C:\\Users\\alessandro.benedetti\\Desktop\\XMLFile2.xml")
        {
            InitializeComponent();

            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.DrawItem += new DrawItemEventHandler(this.tabControl1_DrawItem);

            SlotStateParser ssp = new SlotStateParser(xmlPath);
            tabs = ssp.Parse();

            InitializeSlots();
            InitializeDataGrid();
            InitializeList();
        }

        private void InitializeSlots()
        {
            foreach (TabInfo tab in tabs)
            {
                int gap = 40;
                int numCol = tab.Slot.Count;
                int numRow = 0;
                foreach (var col in tab.Slot)
                    numRow = Math.Max(numRow, col.Count);

                int freeWidth = tabControl1.Width;
                int freeHeight = tabControl1.Height;
                int boxSize = Math.Min(freeWidth / numCol, freeHeight / numRow) * 9 / 10;

                TabPage tp = new TabPage(tab.Name);
                tp.BackColor = baseColor;
                for (int j = 0; j < numCol; j++)
                {
                    for (int i = 0; i < tab.Slot[j].Count; i++)
                    {
                        CustomLabel cell = new CustomLabel(tab.Sticky[j][i] == "1", tab.Mask[j][i] == "1");
                        cell.Location = new Point(gap + boxSize * j, gap + boxSize * i);
                        cell.Name = $"Tab{tabControl1.TabCount}-Cell{i}_{j}";
                        cell.Size = new Size(boxSize, boxSize);
                        cell.Text = tab.Slot[j][i].ToString();
                        if (tab.WildMask[j][i] != "0")
                            cell.Text += " x" + tab.WildMask[j][i];
                        cell.TextAlign = ContentAlignment.MiddleCenter;
                        cell.Font = new Font("Arial", 30);
                        cell.BackColor = freeCellColor;
                        cell.BorderStyle = BorderStyle.FixedSingle;

                        tp.Controls.Add(cell);
                    }
                }

                tabControl1.Controls.Add(tp);
            }

        }

        private void InitializeList()
        {
            foreach (var winning in tabs[0].Winnings)
            {
                paylineList.Items.Add($"Winning #{winning.id + 1}: symbol {winning.symbol}");
            }
            if (paylineList.Items.Count > 0)
                paylineList.SelectedIndex = 0;
        }

        private void InitializeDataGrid()
        {
            additionalDataView.ColumnCount = 2;
            additionalDataView.Columns[0].Name = "Name";
            additionalDataView.Columns[1].Name = "Value";

            additionalDataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        private void paylineList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (paylineList.SelectedItem == null) return;

            int currentTabIndex = tabControl1.TabPages.IndexOf(tabControl1.SelectedTab);
            TabInfo currentTab = tabs[currentTabIndex];
            string curItem = paylineList.SelectedItem.ToString();
            int index = paylineList.FindString(curItem);

            if (index == -1)
            {
                MessageBox.Show("Item not found!");
                return;
            }

            int numCol = currentTab.Slot.Count;
            int numRow = 0;
            foreach (var row in currentTab.Slot)
                numRow = Math.Max(numRow, row.Count);

            for (int j = 0; j < numCol; j++)
            {
                for (int i = 0; i < currentTab.Slot[j].Count; i++)
                {
                    CustomLabel cell = this.Controls.Find($"Tab{currentTabIndex}-Cell{i}_{j}", true).FirstOrDefault() as CustomLabel;
                    cell.Font = new Font("Arial", 30);
                    cell.ForeColor = freeCellFontColor;
                    cell.BackColor = freeCellColor;
                }
            }

            Winning selectedWinning = currentTab.Winnings[index];
            foreach (var pos in selectedWinning.winningPos)
            {
                CustomLabel cell = this.Controls.Find($"Tab{currentTabIndex}-Cell{pos.Item1}_{pos.Item2}", true).FirstOrDefault() as CustomLabel;
                cell.BackColor = winningCellColor;
                cell.ForeColor = winningCellFontColor;
                cell.Font = new Font("Arial", 40, FontStyle.Bold);
            }

            additionalDataView.Rows.Clear();
            foreach (var info in selectedWinning.additionalInfo)
            {
                String[] new_info = { info.Item1, info.Item2 };
                additionalDataView.Rows.Add(new_info);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int currentTabIndex = tabControl1.TabPages.IndexOf(tabControl1.SelectedTab);
            TabInfo currentTab = tabs[currentTabIndex];
            paylineList.Items.Clear();

            foreach (var winning in currentTab.Winnings)
            {
                paylineList.Items.Add($"Winning #{winning.id + 1}: symbol {winning.symbol}");
            }

            additionalDataView.Rows.Clear();
            if (paylineList.Items.Count > 0)
                paylineList.SelectedIndex = 0;
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {

            Rectangle rec = tabControl1.ClientRectangle;

            StringFormat StrFormat = new StringFormat();
            StrFormat.LineAlignment = StringAlignment.Center;
            StrFormat.Alignment = StringAlignment.Center;

            SolidBrush backColor = new SolidBrush(baseColor);
            SolidBrush fontColor;

            e.Graphics.FillRectangle(backColor, rec);

            Font fntTab = e.Font;
            Brush bshBack = new SolidBrush(freeCellColor);

            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                bool bSelected = (tabControl1.SelectedIndex == i);
                Rectangle recBounds = tabControl1.GetTabRect(i);
                RectangleF tabTextArea = (RectangleF)tabControl1.GetTabRect(i);
                if (bSelected)
                {
                    e.Graphics.FillRectangle(bshBack, recBounds);
                    fontColor = new SolidBrush(Color.White);
                    e.Graphics.DrawString(tabControl1.TabPages[i].Text, fntTab, fontColor, tabTextArea, StrFormat);
                }
                else
                {
                    fontColor = new SolidBrush(Color.White);
                    e.Graphics.DrawString(tabControl1.TabPages[i].Text, fntTab, fontColor, tabTextArea, StrFormat);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor = Color.Red;
            button1.ForeColor = Color.White;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = baseColor;
            button1.ForeColor = Color.Gray;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }


    public class CustomLabel : System.Windows.Forms.Label
    {
        Color stickyColor = Color.FromArgb(114, 9, 183);
        Color maskColor = Color.FromArgb(6, 214, 160);
        bool isSticky;
        bool isMask;

        public CustomLabel(bool isSticky, bool isMask)
        {
            this.isSticky = isSticky;
            this.isMask = isMask;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);


            Pen stickyPen = new Pen(stickyColor, 5);
            Pen maskPen = new Pen(maskColor, 5);

            if (isMask && isSticky)
            {
                float[] dashValues = { 3, 3 };
                maskPen.DashPattern = dashValues;
                e.Graphics.DrawRectangle(stickyPen, ClientRectangle);
                e.Graphics.DrawRectangle(maskPen, ClientRectangle);

            }
            else if (isMask)
            {
                e.Graphics.DrawRectangle(maskPen, ClientRectangle);
            }
            else if (isSticky)
            {
                e.Graphics.DrawRectangle(stickyPen, ClientRectangle);
            }

        }

    }
}