using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using CheckBox = System.Windows.Controls.CheckBox;

namespace TrainBenchSimulationSW
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        ObservableCollection<Dati> dati = new ObservableCollection<Dati>();
        ObservableCollection<Dati> datiBackUp = new ObservableCollection<Dati>();
        ObservableCollection<DatiSc> script = new ObservableCollection<DatiSc>();
        ObservableCollection<String> results = new ObservableCollection<String>();
        
        public MainWindow()
        {
            InitializeComponent();
            dataGrid2.IsReadOnly = true;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlBook;
            Microsoft.Office.Interop.Excel.Worksheet xlSheet;
            Microsoft.Office.Interop.Excel.Range xlRange;
            
            int xlRow;
            string strFileName;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Excel Office| *.xls; *.xlsx";
            openFileDialog1.ShowDialog();
            strFileName = openFileDialog1.FileName;

            if (strFileName != string.Empty)
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlBook = xlApp.Workbooks.Open(strFileName);
                xlSheet = xlBook.Worksheets["Foglio1"];
                xlRange = xlSheet.UsedRange;
                int i = 0;

                for (xlRow = 2; xlRow < xlRange.Count; xlRow++)
                {
                    if (xlRange.Cells[xlRow, 1].Text != "")
                    {
                        i++;
                        dati.Add(new Dati { n = i, name = xlRange.Cells[xlRow, 1].Text, type = xlRange.Cells[xlRow, 2].Text, value=Convert.ToDouble(xlRange.Cells[xlRow,3].Text) });
                        datiBackUp.Add(new Dati { n = i, name = xlRange.Cells[xlRow, 1].Text, type = xlRange.Cells[xlRow, 2].Text, value = Convert.ToDouble(xlRange.Cells[xlRow, 3].Text) });
                    }
                }
                dataGrid1.ItemsSource = dati;
                xlBook.Close();
                xlApp.Quit();

                filterTypeLbl.IsEnabled = true;
                combo.IsEnabled = true;
            }
        }
        class Dati
        {
            public int n { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public double value { get; set; }
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //show the results of the search with the apllied filter
            var _itemSourceList = new CollectionViewSource() { Source = dati };
            ICollectionView Itemlist = _itemSourceList.View;
            // Filter
            string search = searchBox.Text;
            var yourCostumFilter = new Predicate<object>(item => ((Dati)item).name.Contains(search));
            Itemlist.Filter = yourCostumFilter;
            dataGrid2.ItemsSource = Itemlist;
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dataGrid1.SelectedItem != null)
                {
                    if (dataGrid1.SelectedItem is Dati)
                    {
                        var row = (Dati)dataGrid1.SelectedItem;

                        if (row != null)
                        {
                            selectedTxt.Text = "#  "+row.n+"  Name:  "+row.name+"  Type:  "+row.type+"  Value:  "+row.value;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void dataGrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dataGrid2.SelectedItem != null)
                {
                    if (dataGrid2.SelectedItem is Dati)
                    {
                        var row = (Dati)dataGrid2.SelectedItem;

                        if (row != null)
                        {
                            selected2Txt.Text = "#  " + row.n + "  Name:  " + row.name + "  Type:  " + row.type+ "  Value:  " + row.value;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            okbtn.IsEnabled = true;
            newValtxt.IsEnabled = true;
            cancBtn.IsEnabled = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string val = newValtxt.Text;
            double v = Convert.ToDouble(val);
            int riga = dataGrid1.SelectedIndex;
            Dati p = dati[riga];
            p.value = v;
            dati[riga] = p;
            dataGrid1.Items.Refresh();
            okbtn.IsEnabled = false;
            newValtxt.Clear();
            newValtxt.IsEnabled = false;
            cancBtn.IsEnabled = false;
        }

        private void OpenSc_Click(object sender, RoutedEventArgs e)
        { 
            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlBook;
            Microsoft.Office.Interop.Excel.Worksheet xlSheet;
            Microsoft.Office.Interop.Excel.Range xlRange;

            int xlRow;
            string strFileName;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Excel Office| *.xls; *.xlsx;" ;
            openFileDialog1.ShowDialog();
            strFileName = openFileDialog1.FileName;

                if (strFileName != string.Empty)
                {
                    xlApp = new Microsoft.Office.Interop.Excel.Application();
                    xlBook = xlApp.Workbooks.Open(strFileName);
                    xlSheet = xlBook.Worksheets["Sequence"];
                    xlRange = xlSheet.UsedRange;
                    int i = 0;

                    for (xlRow = 2; xlRow < xlRange.Count; xlRow++)
                    {
                        if (xlRange.Cells[xlRow, 1].Text != "")
                        {
                            i++;
                            script.Add(new DatiSc { operation = xlRange.Cells[xlRow, 1].Text, name = xlRange.Cells[xlRow, 2].Text, value = Convert.ToDouble(xlRange.Cells[xlRow, 3].Text) });
                        }
                    }
                    dataGridSc.ItemsSource = script;
                    resGrid.IsEnabled = true;
                    startBtn.IsEnabled = true;
                    xlBook.Close();
                    xlApp.Quit();
            }
        }
        class DatiSc
        {
            public string operation { get; set; }
            public string name { get; set; }
            public double value { get; set; }
        }

        private void cancBtn_Click(object sender, RoutedEventArgs e)
        {
            newValtxt.Clear();
            newValtxt.IsEnabled = false;
            okbtn.IsEnabled = false;
            cancBtn.IsEnabled = false;
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.Items.Count == 0)
            {
                MessageBox.Show("Please, Load the Data File in the Main Window first!");
            }
            else
            {
                for (int i = 0; i < script.Count; i++)
                {
                    DatiSc d = script[i];
                    if (d.operation == "Write")
                    {
                        string nomeScript = script[i].name;
                        for (int j = 0; j < dati.Count; j++)
                        {
                            if (dati[j].name == nomeScript)
                                dati[j].value = script[i].value;
                        }
                        results.Add("PASSED");
                    }
                    else results.Add("-");
                }
                dataGrid1.Items.Refresh();
                resGrid.ItemsSource = results;
                startBtn.IsEnabled = false;
            }
        }

        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<Dati> datiD = new ObservableCollection<Dati>();
            ObservableCollection<Dati> datiA = new ObservableCollection<Dati>();
            //Digital selection
            if (combo.SelectedIndex == 0)
            {
                dati.Clear();
                for(int i=0;i<datiBackUp.Count;i++)
                dati.Add(datiBackUp[i]);

                for(int i = 0; i< dati.Count; i++)
                {
                    Dati d = dati[i];
                    if (d.type=="DO"||d.type=="DI")
                        datiD.Add(d);
                }
                dati.Clear();
                for (int i = 0; i < datiD.Count; i++)
                    dati.Add(datiD[i]);
                dataGrid1.Items.Refresh();
            }
            //Analog Selection
            if (combo.SelectedIndex == 1)
            {
                dati.Clear();
                for (int i = 0; i < datiBackUp.Count; i++)
                    dati.Add(datiBackUp[i]);

                for (int i = 0; i < dati.Count; i++)
                {
                    Dati d = dati[i];
                    if (d.type == "AO" || d.type == "AI")
                        datiA.Add(d);
                }
                dati.Clear();
                for (int i = 0; i < datiA.Count; i++)
                    dati.Add(datiA[i]);
                dataGrid1.Items.Refresh();
            }
            //All selection
            if (combo.SelectedIndex == 2)
            {
                dati.Clear();
                for (int i = 0; i < datiBackUp.Count; i++)
                    dati.Add(datiBackUp[i]);
                dataGrid1.Items.Refresh();
            }
        }
    }
}