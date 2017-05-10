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
using WpfTask.Views;

namespace WpfTask
{
    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Task1Btn_Click(object sender, RoutedEventArgs e)
        {
           var task1 = new Task1();
            task1.ShowDialog();
        }

        private void Task2Btn_Click(object sender, RoutedEventArgs e)
        {
            var task2 = new Task2();
            task2.ShowDialog();
        }

        private void Task5tn_Click(object sender, RoutedEventArgs e)
        {
            var task5 = new Task5();
            task5.ShowDialog();
        }
    }
}
