using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Color stopColor = Color.FromRgb(0,32,41);
        Color freeCellColor = Color.FromRgb(26, 60, 72);
        Color freeCellFontColor = Color.FromRgb(255,255,255);
        Color winningCellColor = Color.FromRgb(80, 0, 17);
        Color winningCellFontColor = Color.FromRgb(255, 195, 0); 
        Color stickyColor = Color.FromRgb(251, 86, 7);
        Color maskColor = Color.FromRgb(6, 214, 160);

        List<TabInfo> tabs;
        public ObservableCollection<DataTable> DataList { get; set; } = new ObservableCollection<DataTable>();
        public ObservableCollection<String> WinningList { get; set; } = new ObservableCollection<String>();

        public List<List<List<CellStyle>>> cellStyles = new List<List<List<CellStyle>>>();

        public List<Slot> Slots { get; set; } = new List<Slot>();

        private double scale = 1.0;

        public MainWindow()
        {
            DataContext = this;
            var args = Environment.GetCommandLineArgs();
            SlotStateParser ssp;
            if (args != null && args.Length > 1)
            {
                ssp = new SlotStateParser(args[1]);
            }
            else
            {
                ssp = new SlotStateParser("C:\\Users\\alessandro.benedetti\\Desktop\\XMLFile2.xml");
            }
            tabs = ssp.Parse();
            InitializeComponent();


            InitializeSlots();
            InitializeWinningList();
            InitializeDataList();
        }
        private void InitializeSlots()
        {
            foreach (TabInfo tab in tabs)
            {
                Grid extGrid = new Grid();
                RowDefinition stopRow = new RowDefinition();
                stopRow.Height = new GridLength(50, GridUnitType.Pixel);
                RowDefinition slotRow = new RowDefinition();
                slotRow.Height = new GridLength(1, GridUnitType.Star);
                extGrid.RowDefinitions.Add(stopRow);
                extGrid.RowDefinitions.Add(slotRow);

                UniformGrid sp = new UniformGrid();
                sp.Margin = new Thickness(7,7,7,7);
                sp.Columns = tab.Stops.Count;
                sp.Rows = 1;
                foreach (String stop in tab.Stops)
                {
                    Label l = new Label();
                    l.Content = stop;
                    l.MinWidth = 100;
                    l.HorizontalContentAlignment = HorizontalAlignment.Center;
                    l.VerticalContentAlignment = VerticalAlignment.Center;
                    l.Background = new SolidColorBrush(stopColor);
                    l.Foreground = new SolidColorBrush(freeCellFontColor);
                    sp.Children.Add(l);
                }
                sp.SetValue(Grid.ColumnProperty, 0);
                sp.SetValue(Grid.RowProperty, 0);
                extGrid.Children.Add(sp);

                Slots.Add(new Slot());
                int numCol = tab.Slot.Count;
                int numRow = 0;
                foreach (var col in tab.Slot)
                    numRow = Math.Max(numRow, col.Count);

                TabItem tp = new TabItem();

                tp.Name = "tab_" + tabs.IndexOf(tab).ToString();
                tp.Header = tab.Name;
                tp.Background = null;

                cellStyles.Add(new List<List<CellStyle>>());
                for (int c = 0; c < numCol; c++)
                {
                    cellStyles[tabs.IndexOf(tab)].Add(new List<CellStyle>());
                    Slots[tabs.IndexOf(tab)].cells.Add(new List<Label>());
                    for (int r = 0; r < tab.Slot[c].Count; r++)
                    {
                        cellStyles[tabs.IndexOf(tab)][c].Add(new CellStyle(freeCellFontColor, freeCellColor));
                        Label cell = new Label();
                        cell.Name = $"Cell{c}_{r}";
                        cell.Content = tab.Slot[c][r].ToString();
                        if (tab.WildMask[c][r] != "0" && tab.WildMask[c][r] != "")
                            cell.Content += " x" + tab.WildMask[c][r];

                        cell.HorizontalContentAlignment = HorizontalAlignment.Center;
                        cell.VerticalContentAlignment = VerticalAlignment.Center;
                        cell.FontFamily = new FontFamily("Arial");
                        cell.FontSize = 30;
                        cell.Margin = new Thickness(1, 1, 1, 1);

                        Binding foregroundBinding = new Binding("ForegroundColor");
                        foregroundBinding.Source = cellStyles[tabs.IndexOf(tab)][c][r];
                        cell.SetBinding(Label.ForegroundProperty, foregroundBinding);

                        Binding backgroundBinding = new Binding("BackgroundColor");
                        backgroundBinding.Source = cellStyles[tabs.IndexOf(tab)][c][r];
                        cell.SetBinding(Label.BackgroundProperty, backgroundBinding);

                        cell.SetValue(Grid.RowProperty, r);
                        cell.SetValue(Grid.ColumnProperty, c);
                        Slots[tabs.IndexOf(tab)].cells[c].Add(cell);

                        if (tab.Mask[c][r] != "0" && tab.Sticky[c][r] != "0")
                        {
                            LinearGradientBrush gradientBrush = new LinearGradientBrush();
                            gradientBrush.StartPoint = new Point(0, 0);
                            gradientBrush.EndPoint = new Point(1, 0);
                            gradientBrush.GradientStops.Add(new GradientStop(stickyColor, 0.5));
                            gradientBrush.GradientStops.Add(new GradientStop(maskColor, 0.5));
                            cell.BorderBrush = gradientBrush;
                            cell.BorderThickness = new Thickness(2,2,2,2);
                        }
                        else if (tab.Mask[c][r] != "0")
                        {
                            cell.BorderBrush = new SolidColorBrush(maskColor);
                            cell.BorderThickness = new Thickness(2, 2, 2, 2);
                        }
                        else if (tab.Sticky[c][r] != "0")
                        {
                            cell.BorderBrush = new SolidColorBrush(stickyColor);
                            cell.BorderThickness = new Thickness(2,2,2,2);
                        }
                        else
                        {
                            cell.BorderBrush = new SolidColorBrush(freeCellColor);
                            cell.BorderThickness = new Thickness(2,2,2,2);
                        }


                        if (cell.Content == "")
                            cell.Visibility = Visibility.Hidden;

                    }
                }

                UniformGrid grid = new UniformGrid();
                grid.Columns = numCol;
                grid.Rows = numRow;
                grid.Margin = new Thickness(10, 10, 10, 10);
                grid.SetValue(Grid.ColumnProperty, 0);
                grid.SetValue(Grid.RowProperty, 1);

                for (int r = 0; r < numRow; r++)
                    for (int c = 0; c < numCol; c++)
                        grid.Children.Add(Slots[tabs.IndexOf(tab)].cells[c][r]);

                extGrid.Children.Add(grid);
                tp.Content = extGrid;
                tabControl1.Items.Add(tp);
            }

        }

        private void InitializeWinningList()
        {
            if (tabs.Count == 0) return;

            tabControl1.SelectedIndex = 0;
        }
        private void InitializeDataList()
        {
            if (tabs.Count == 0 || tabs[0].Winnings.Count == 0) return;

            PaylineList.SelectedIndex = 0;
        }

        private void tabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WinningList.Clear();
            var tc = sender as TabControl;
            int selectedTab = tc.SelectedIndex;

            if (selectedTab < 0) return;

            foreach (var win in tabs[selectedTab].Winnings)
            {
                WinningList.Add($"Winning #{win.id + 1}: symbol {win.symbol}");
            }

            PaylineList.SelectedIndex = 0;
        }

        private void PaylineList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
      
            DataList.Clear();
            var pl = sender as ListBox;
            int selectedWinning = pl.SelectedIndex;

            if (selectedWinning < 0) return;

            var tabIndex = tabControl1.SelectedIndex;
            var tab = tabs[tabIndex];

            DataList.Add(new DataTable("reelset",tab.Reelset));
            foreach (var data in tab.Winnings[selectedWinning].additionalInfo)
            {
                DataList.Add(new DataTable(data.Item1, data.Item2));
            }

            for (int c = 0; c < tab.Slot.Count; c++)
            {
                for (int r = 0; r < tab.Slot[c].Count; r++)
                {

                    if (tab.Winnings[selectedWinning].winningPos.Contains(new Tuple<int, int>(r,c)))
                    {
                        cellStyles[tabs.IndexOf(tab)][c][r].ForegroundColor = new SolidColorBrush(winningCellFontColor);
                        cellStyles[tabs.IndexOf(tab)][c][r].BackgroundColor = new SolidColorBrush(winningCellColor);
                    }
                    else
                    {
                        cellStyles[tabs.IndexOf(tab)][c][r].ForegroundColor = new SolidColorBrush(freeCellFontColor);
                        cellStyles[tabs.IndexOf(tab)][c][r].BackgroundColor = new SolidColorBrush(freeCellColor);
                    }

                }
            }        
        }

        private void MainWindow_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                var elem = sender as Control;
                scale = elem.LayoutTransform.Value.M11;
                if (e.Delta > 0)
                {
                    scale *= 1.1;
                }
                else
                {
                    scale /= 1.1 ;
                }

                elem.LayoutTransform = new ScaleTransform(scale, scale);

                e.Handled = true;
            }
        }

        private void scalewindow(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (e.Delta > 0)
                {
                    Width *= 1.05;
                    Height *= 1.05;
                    scale = 1.05;
                }
                else
                {
                    Width /= 1.05;
                    Height /= 1.05;
                    scale = 1.0/1.05;
                }

                int toll = 0; 
                if (Width > MinWidth + toll && Height > MinHeight + toll)
                {
                    DataGrid1.LayoutTransform = new ScaleTransform(DataGrid1.LayoutTransform.Value.M11 * scale, DataGrid1.LayoutTransform.Value.M11 * scale);
                    PaylineList.LayoutTransform = new ScaleTransform(PaylineList.LayoutTransform.Value.M11 * scale, PaylineList.LayoutTransform.Value.M11 * scale);
                }
                e.Handled = true;
            }
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;
            if (e.Delta < 0)
            {
                scrollViewer.LineRight();
            }
            else
            {
                scrollViewer.LineLeft();
            }
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }
    }

    public class CellStyle : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private SolidColorBrush _ForegroundColor;
        public SolidColorBrush ForegroundColor
        {
            get { return _ForegroundColor; }
            set {
                if (_ForegroundColor != value && this.PropertyChanged != null)
                {
                    _ForegroundColor = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("ForegroundColor"));
                }
                else
                    _ForegroundColor = value;
            }
        }
        private SolidColorBrush _BackgroundColor;
        public SolidColorBrush BackgroundColor
        {
            get { return _BackgroundColor; }
            set
            {
                if (_BackgroundColor != value && this.PropertyChanged != null)
                {
                    _BackgroundColor = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("BackgroundColor"));
                }
                else
                    _BackgroundColor = value;
            }
        }

        public CellStyle(Color Fg, Color bg)
        {
            ForegroundColor = new SolidColorBrush(Fg);
            BackgroundColor = new SolidColorBrush(bg);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
