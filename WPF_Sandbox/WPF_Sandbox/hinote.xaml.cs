using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
    /// hinote.xaml에 대한 상호 작용 논리
    /// </summary>

   
    public partial class hinote : Window
    {
        string currentFileNameValue;
        string currentFileName
        {
            get
            {
                return currentFileNameValue;
            }
            set
            {
                currentFileNameValue = value;
                if (string.IsNullOrEmpty(value))
                {
                    this.Title = "제목 없음 - hinote";
                }
                else
                {
                    this.Title = System.IO.Path.GetFileName(value) + " - hinote";
                }
            }
        }

        bool isSaved;

        public hinote() //생성자는 클래스 안에
        {
            InitializeComponent();
            currentFileName = "";
            isSaved = true;
        }

        //메뉴들 ----------
        private void menu_file_exit_click(object sender, RoutedEventArgs e)
        {
            if (!isSaved)
            {
                if (!SaveFileAsk())
                {
                    return;
                }
            }
            
              this.Close();
            
        
        }

        private void menu_file_new_click(object sender, RoutedEventArgs e)
        {
            if(!isSaved)
            {
                if(!SaveFileAsk())
                {
                    return;
                }
            }

            txt_main.Text = "";
            currentFileName = "";
            isSaved = true;
        }

        private void menu_file_open_click(object sender, RoutedEventArgs e)
        {
            if(!isSaved)
            {
                if (!SaveFileAsk())
                {
                    return;
                }
                
            }
            ShowOpenFile();
        }

        private void menu_file_save_click(object sender, RoutedEventArgs e)
        {
            SaveFile();
        }

        private void menu_file_saveas_click(object sender, RoutedEventArgs e)
        {
            ShowSaveFileAs();   
        }

     

        private void menu_form_autoline_click(object sender, RoutedEventArgs e)
        {
            if(menu_form_autoline.IsChecked) //자동줄바꿈 선택했을 때
            {
                txt_main.TextWrapping = TextWrapping.WrapWithOverflow; //WrapWithOverflow : 자동으로 줄바꿈
            }
            else
            {
                txt_main.TextWrapping = TextWrapping.NoWrap; //NoWrap : 줄바꿈이 수행되지 않는다.
            }
        }

        private void txt_main_changed(object sender, TextChangedEventArgs e)
        {
           
            isSaved = false;
        }

        //메뉴창 끝 ---------------------------

        // 기능 시작 ------------------------

        private void window_closing (object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isSaved)
            {
                if(!SaveFileAsk())
                {
                    e.Cancel = true;
                }
            }
        }

        bool ShowOpenFile() //파일 열기 창
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "텍스트 파일|*.txt|모든 파일|*.*"; //어떤 파일 불러 올건지
            dialog.Title = "열기";

            if (dialog.ShowDialog() == true)
            {
                string buf;
                string filename = dialog.FileName;

                if(ShowOpenFile(filename,out buf))
                {
                    txt_main.Text = buf;
                    this.Title = filename;
                    isSaved = true;
                    currentFileName = filename;
                    return true;
                }
                else
                {
                    MessageBox.Show("파일을 읽는데 실패했습니다.");
                }
            }
            return false;
        }

        bool SaveFile()
        {
            if (!string.IsNullOrEmpty(currentFileName))
            {
                if(SaveAs(currentFileName, txt_main.Text))
                {
                    isSaved = true;
                    return true;
                }
                else
                {
                    MessageBox.Show("파일을 저장하는 데 실패했습니다.");
                }
            }
            else
            {
                return ShowSaveFileAs();
            }
            return false;
        }

        bool ShowSaveFileAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "텍스트 파일|*.txt|모든 파일|*.*";
            dialog.Title = "다른이름으로 저장";

            if (dialog.ShowDialog() == true)
            {
                string path = dialog.FileName;
                if(SaveAs(path, txt_main.Text))
                {
                    currentFileName = path;
                    isSaved = true;
                    return true;
                }
                else
                {
                    MessageBox.Show("파일을 저장하는 데 실패했습니다.");
                }
            }
            return false;
        }


        bool SaveFileAsk()
        {
            MessageBoxResult result;

            if(string.IsNullOrEmpty(currentFileName))
            {
                result = MessageBox.Show("변경 내용을 제목 없음에 저장하시겠습니까?", "Hinote", MessageBoxButton.YesNoCancel);
            }
            else
            {
                result = MessageBox.Show("변경 내용을 " + System.IO.Path.GetFileName(currentFileName) + "에 저장하시겠습니까?", "Hinote", MessageBoxButton.YesNoCancel);
            }

            if (result == MessageBoxResult.Yes)
            {
                return SaveFile();
            }
            else if(result == MessageBoxResult.No)
            {
                return true;
            }

            return false;
        }

        bool SaveAs (string path, string data) //파일 지정해서 저장
        {
            try
            {   //StreamWriter -> 파일에 쓰는 메서드
                StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8); //File *pf = fopen(path)와 유사

                writer.Write(data); //연 파일에 데이터 쓰기
                writer.Close();
                writer.Dispose();

                return true; //파일쓰기 성공
            }

            catch (IOException) //예외가 날 때
            {
                return false; //파일쓰기 실패
            }
        }

        bool ShowOpenFile(string path, out string data)
        {
            data = "";
            try
            {
                StreamReader reader = new StreamReader(path, Encoding.UTF8);
                data = reader.ReadToEnd(); //그 파일의 내용을 끝까지 읽는다.

                reader.Close();
                reader.Dispose();
                return true; //파일 읽기 성공
            }
            catch (IOException)
            {
                return false; //파일 읽기 실패
            }   
        }

        private void x_click(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
   
    
}
