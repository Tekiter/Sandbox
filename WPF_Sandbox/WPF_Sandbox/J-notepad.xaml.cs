using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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



namespace WPF_Sandbox
{
    /// <summary>
    /// J_notepad.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class J_notepad : Window
    {
        public J_notepad()
        {
            InitializeComponent();
        }

        //전역변수부분
        bool change_check = true; //텍스트가 변경되었는지 확인하는 bool형식의 변수
        string save_file_path = "";

        #region MenuEvents



        private void menu_file_exit_click(object sender, RoutedEventArgs e)
        {
            Exit_check();
        }

        private void menu_file_new_click(object sender, RoutedEventArgs e)
        {
            txt_main.Text = "";

        }

        private void menu_file_open_click(object sender, RoutedEventArgs e)
        {
            ShowOpenFile();
        }


        private void menu_file_save_click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void menu_file_saveas_click(object sender, RoutedEventArgs e)
        {
            ShowSaveFileAs();
        }

        private void menu_form_autoline_click(object sender, RoutedEventArgs e)
        {
            if (menu_form_autoline.IsChecked)
            {
                MessageBox.Show("체크됨!");
                txt_main.TextWrapping = System.Windows.TextWrapping.Wrap;
            }
            else
            {
                MessageBox.Show("체크 안됨!");
                txt_main.TextWrapping = System.Windows.TextWrapping.NoWrap;
            }
        }

        #endregion

        #region FileActions

        bool ShowOpenFile()
        {
            // 파일 열기 창을 띄우는 로직
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "텍스트 파일|*.txt|모든 파일|*.*"; // 어떤 파일만 보이게 할지 결정
            dialog.Title = "열기";

            if (dialog.ShowDialog() == true)        // 유저가 확인을 눌렀을 시
            {
                string buf;
                string filename = dialog.FileName;


                if (Open(filename, out buf))        // 유저가 누른 파일을 읽어옴
                {
                    txt_main.Text = buf;        // 읽은 파일의 내용을 텍스트박스에 넣음
                    this.Title = filename;
                    return true;
                }
                else
                {
                    MessageBox.Show("파일을 읽는데 실패했습니다.");
                }
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
                if (SaveAs(path, txt_main.Text))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("파일을 저장하는 데 실패했습니다.");
                }
            }
            

            return false;
        }

        #endregion

        #region FileIO

        bool SaveAs(string path, string data) // 지정된 파일에 내용을 저장한다
        {
            try
            {
                StreamWriter writer = new StreamWriter(path); // File *pf = fopen(path) 와 유사

                writer.Write(data); // 연 파일에 데이터 쓰기

                writer.Close();
                writer.Dispose();
                save_file_path = path;
                change_check = true;
                return true; // 파일 쓰기 성공
            }
            catch (IOException) // 파일이 사용중이라던가 같은 이유로 예외가 날때
            {
                return false; // 파일 쓰기 실패
            }

        }


        bool Save()
        {
             try
            {
                Savecheck();
                return true;
            }
            catch
            {
                return false;
            }
        }

        void Savecheck()
        {
            if (save_file_path == "")
            {
                ShowSaveFileAs();
            }
            else
            {
                SaveAs(save_file_path, txt_main.Text);
            }
        }

        void Exit_check()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (change_check) this.Close();
            else
            {
                if (save_file_path == "")
                {
                    MessageBoxResult result = MessageBox.Show("변경내용을 제목없음에 저장하시겠습니까?", "", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        SaveAs(save_file_path, txt_main.Text);
                        this.Close();
                    }
                    else this.Close();
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("변경사항을 저장하시겠습니까?", "", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        SaveAs(save_file_path, txt_main.Text);
                        this.Close();
                    }
                    else this.Close();
                }
            }
        }

        bool Open(string path, out string data) // 지정된 파일의 내용을 읽어온다
        {
            data = "";
            try
            {
                StreamReader reader = new StreamReader(path); // path의 파일을 열고

                data = reader.ReadToEnd(); // 그 파일의 내용을 끝까지 읽는다

                reader.Close();
                reader.Dispose();
                return true; // 파일 읽기 성공
            }
            catch (IOException)
            {

                return false; // 파일 읽기 실패
            }

        }



        #endregion

        private void txt_main_TextChanged(object sender, TextChangedEventArgs e)
        {
            change_check = false;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("아직 구현이 되지 않은 기능입니다");
        }
    }
}
