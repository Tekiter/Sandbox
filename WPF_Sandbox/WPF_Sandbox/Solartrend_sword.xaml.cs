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
using System.Windows.Shapes;
using System.Net; //이 것을 사용하여야 Webclint 내장 클래스를 사용가능하다

namespace WPF_Sandbox
{
    /// <summary>
    /// Solartrend_sword.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Solartrend_sword : Window
    {
        public Solartrend_sword()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int i;
            List<string> search_w = Getstring();

            new_search_word.Items.Clear();

            for (i = 0; i < search_w.Count; i++)
            {
                new_search_word.Items.Add((i + 1).ToString() + " " + search_w[i]);
            }
        }

        List<string> Getstring()
        {
            List<string> save = new List<string>();
            int i;
            string gethtml = Gethtml("https://www.daum.net/");

            //문자열 자르기
            gethtml = Split_html(Split_html(gethtml, "<div class=\"rank_cont @1\">")[1], "<h4 class = \"screen_out\">")[0];
            string[] splitlast = Split_html(gethtml, "<a href");

            for (i = 1; i <= 10; i++)
            {
                save.Add(Split_html(Split_html(splitlast[i], "class=\"link_issue\">")[1], "</a>")[0]);
            }
            return save;
        }

        string Gethtml(string url)
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string str = client.DownloadString("https://www.daum.net/");
            client.Dispose();
            return str;
        }

        string[] Split_html(string str, string what)
        {
            return str.Split(new string[] { what }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
