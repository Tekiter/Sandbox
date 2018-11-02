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
    /// SimpleNotepad.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SimpleNotepad : Window
    {


        public SimpleNotepad()
        {
            InitializeComponent();
        }

        #region MenuEvents



        private void menu_file_exit_click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
            }
            else
            {
                MessageBox.Show("체크 안됨!");
            }
        }

        #endregion

        #region FileActions

        bool ShowOpenFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "텍스트 파일|*.txt|모든 파일|*.*";
            dialog.Title = "열기";

            if (dialog.ShowDialog() == true)
            {
                string buf;
                string filename = dialog.FileName;
                if (Open(filename, out buf))
                {
                    txt_main.Text = buf;
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

        bool SaveAs(string path, string data)
        {
            try
            {
                StreamWriter writer = new StreamWriter(path);

                writer.Write(data);

                writer.Close();
                writer.Dispose();

                return true;
            }
            catch (IOException)
            {
                return false;
            }

        }


        //bool Save(string data)
        //{
        //    
        //}

        bool Open(string path, out string data)
        {
            data = "";
            try
            {
                StreamReader reader = new StreamReader(path);

                data = reader.ReadToEnd();

                reader.Close();
                reader.Dispose();
                return true;
            }
            catch (IOException)
            {

                return false;
            }

        }


        #endregion


    }
}
