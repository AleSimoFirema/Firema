using System;
using System.Collections.Generic;
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

namespace provaFirema
{
    /// <summary>
    /// Logica di interazione per SetWindow.xaml
    /// </summary>
    public partial class SetWindow : Window
    {
        public SetWindow()
        {
            InitializeComponent();
        }

        public double val;
        public bool closed;

        public double getval()
        {
            return this.val;
        }

        public void setVal(double v)
        {
            this.val = v;
        }

        public bool getClosed()
        {
            return this.closed;
        }

        public void setClosed(bool b)
        {
            this.closed = b;
        }

        private void setBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string va = setBox.Text;
            double v = Convert.ToDouble(va);
            setVal(v);
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            string va = setBox.Text;
            double v = Convert.ToDouble(va);
            setVal(v);
            this.setClosed(true);
            this.Close();
        }

        public string ContenutoTextBox
        {
            get { return setBox.Text; }
        }
    }
}
