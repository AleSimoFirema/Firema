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
    /// Logica di interazione per Toggle.xaml
    /// </summary>
    public partial class Toggle : UserControl
    {
        Thickness LeftSide = new Thickness(-64, 0, 0, 0);
        Thickness RightSide = new Thickness(0, 0, -64, 0);
        SolidColorBrush off = new SolidColorBrush(Color.FromRgb(160,160,160));
        SolidColorBrush on = new SolidColorBrush(Color.FromRgb(130, 190, 125));
        private bool Toggled = false;

        public Toggle()
        {
            InitializeComponent();
            Back.Fill = off;
            Toggled = false;
            Dot.Margin = LeftSide;
        }

        public bool Toggled1 { get => Toggled; set => Toggled = value; }

        private void Dot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Toggled)
            {
                Back.Fill = on;
                Toggled = true;
                Dot.Margin = RightSide;
            }
            else
            {
                Back.Fill = off;
                Toggled = false;
                Dot.Margin = LeftSide;
            }
        }

        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Toggled)
            {
                Back.Fill = on;
                Toggled = true;
                Dot.Margin = RightSide;
            }
            else
            {
                Back.Fill = off;
                Toggled = false;
                Dot.Margin = LeftSide;
            }
        }
    }
}
