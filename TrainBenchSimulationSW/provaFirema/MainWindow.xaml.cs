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
        ObservableCollection<Persona> persone = new ObservableCollection<Persona>();
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
                        persone.Add(new Persona { n = i, nome = xlRange.Cells[xlRow, 1].Text, cognome = xlRange.Cells[xlRow, 2].Text, valore=Convert.ToDouble(xlRange.Cells[xlRow,3].Text) });
                    }
                }
                dataGrid1.ItemsSource = persone;
                xlBook.Close();
                xlApp.Quit();
            }
        }
        class Persona
        {
            public int n { get; set; }
            public string nome { get; set; }
            public string cognome { get; set; }
            public double valore { get; set; }
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //show the results of the search with the apllied filter
            var _itemSourceList = new CollectionViewSource() { Source = persone };
            ICollectionView Itemlist = _itemSourceList.View;
            // Filter
            string search = searchBox.Text;
            var yourCostumFilter = new Predicate<object>(item => ((Persona)item).nome.Contains(search));
            Itemlist.Filter = yourCostumFilter;
            dataGrid2.ItemsSource = Itemlist;
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dataGrid1.SelectedItem != null)
                {
                    if (dataGrid1.SelectedItem is Persona)
                    {
                        var row = (Persona)dataGrid1.SelectedItem;

                        if (row != null)
                        {
                            selectedTxt.Text = "#  "+row.n+"  Name:  "+row.nome+"  Surname:  "+row.cognome+"  Value:  "+row.valore;
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
                    if (dataGrid2.SelectedItem is Persona)
                    {
                        var row = (Persona)dataGrid2.SelectedItem;

                        if (row != null)
                        {
                            selected2Txt.Text = "#  " + row.n + "  Name:  " + row.nome + "  Surname:  " + row.cognome+ "  Value:  " + row.valore;
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
            Persona p = persone[riga];
            p.valore = v;
            persone[riga] = p;
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
                        for (int j = 0; j < persone.Count; j++)
                        {
                            if (persone[j].nome == nomeScript)
                                persone[j].valore = script[i].value;
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
    }
}