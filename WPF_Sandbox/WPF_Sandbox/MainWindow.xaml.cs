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

namespace WPF_Sandbox
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void uptrend_click(object sender, RoutedEventArgs e)
        {
            uptrend win = new uptrend();
            win.Show();
            
        }

        private void merong_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("메롱~~!");
        }

        private void hitrend_click(object sender, RoutedEventArgs e)
        {
            hitrend win = new hitrend();
            win.Show();
        }

        private void solartrend_click(object sender, RoutedEventArgs e)
        {
            Solartrend_sword search_word = new Solartrend_sword();
            search_word.Show();
        }

        private void simplenotepad_click(object sender, RoutedEventArgs e)
        {
            SimpleNotepad win = new SimpleNotepad();
            win.Show();
        }
    }
}
