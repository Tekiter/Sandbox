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
using System.Net;
using System.Collections.Specialized;

namespace WPF_Sandbox
{
    /// <summary>
    /// uptrend.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class uptrend : Window
    {


        public uptrend()
        {
            InitializeComponent();

        }

        private void reload_click(object sender, RoutedEventArgs e)
        {
            List<string> result = GetList();
            lst.Items.Clear();
            for (int i = 0; i < result.Count; i++)
            {
                lst.Items.Add((i + 1).ToString() + "위: " + result[i]);
            }
        }

        List<string> GetList()
        {

            string str = GetHtmlFromWeb("http://www.naver.com");
            List<string> lstt = new List<string>();


            str = StringSplit(StringSplit(str, "<ul class=\"ah_l\" data-list=\"1to10\"")[1], "<p class=\"ah_time\"")[0];

            string[] slices = StringSplit(str, "<li class=\"ah_item");

            for (int i = 1; i <= 20; i++)
            {
                lstt.Add(StringSplit(StringSplit(slices[i], "<span class=\"ah_k\">")[1], "</span>")[0]);
            }
            return lstt;
        }

        string GetHtmlFromWeb(string url)
        {
            WebClass wc = new WebClass();
            return wc.GET(url);
        }

        string[] StringSplit(string str, string sep)
        {
            return str.Split(new string[] { sep }, StringSplitOptions.RemoveEmptyEntries);
        }
    }

    public class WebClass
    {
        private WebClient wc;

        public WebClass()
        {
            wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
        }

        public string GET(string url)
        {
            return wc.DownloadString(url);
        }

        public string POST(string url, NameValueCollection param)
        {
            byte[] response = wc.UploadValues(url, param);
            return Encoding.UTF8.GetString(response);
        }


    }
}
