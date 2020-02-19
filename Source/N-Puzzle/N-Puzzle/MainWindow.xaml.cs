using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace N_Puzzle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ChooseImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {

            int SendN = 4;
            if (N3.IsChecked == true)
            {
                SendN = 3;
            }

            if (N5.IsChecked == true)
            {
                SendN = 5;
            }

            if (rbNumber.IsChecked == true)
            {
                PlayWindow screen = new PlayWindow(SendN);
                screen.ShowDialog();
            }
            else
            {
                PlayImageWindow screen = new PlayImageWindow(SendN);
                screen.ShowDialog();
            }
            

        }
    }
}