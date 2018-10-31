using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace WPF_Sandbox
{
    /// <summary>
    /// hitrend.xaml에 대한 상호 작용 논리
    /// </summary>

    public partial class hitrend : Window
    {

        public hitrend()
        {
            InitializeComponent(); //cs에서 사용했던 것 사용할 때, 한 창에는 하나의 프로그램만. 다른 프로그램을 만들 때는 다른 창을 띄워서 해야함
        }

        private void hitrend_click(object sender, RoutedEventArgs e)
        {
            List<string> test = Getlist();
            lst.Items.Clear();
            for (int i = 0; i < test.Count; i++)
            {
                lst.Items.Add((i + 1).ToString() + "위: " + test[i]);
            }

        }

        List<string> Getlist()
        {
            string txt = Web("http://www.daum.net");
            List<string> list = new List<string>();

            txt = StringSplit(StringSplit(txt, "<h4 class=\"tit_hotissue\">실시간 이슈 검색어</h4>")[1], "<h4 class=\"screen_out\">오늘의 정보</h4>")[0];
            string[] slices = StringSplit(txt, "<span class=\"num_pctop rank");

            for (int i=1; i<=20; i+=2)
            {
                list.Add(StringSplit(StringSplit(slices[i], "class=\"link_issue\">")[1],"</a>")[0]);
            }

            return list;

            
   
        }

        string Web(string url) //일부값만 반환 합니다 ?
        {
            WebClient web = new WebClient();
            web.Encoding = Encoding.UTF8;
            string str = web.DownloadString("http://daum.net");
            web.Dispose(); //dispose = free

            return str;

        }

        string[] StringSplit(string str, string sep)
        {
            return str.Split(new string[] { sep }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
