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

namespace provaFirema
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        ObservableCollection<Persona> persone = new ObservableCollection<Persona>();
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
            SetWindow sw = new SetWindow();
            sw.Show();
            double v = sw.getval();
            int riga = dataGrid1.SelectedIndex;
            Persona p = persone[riga];
            p.valore = v;
            persone[riga] = p;
            dataGrid1.Items.Refresh();
        }
    }
}