using Microsoft.Win32;
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

namespace Squash.App
{
    /// <summary>
    /// Interaction logic for TextInput.xaml
    /// </summary>
    public partial class TextInput : UserControl
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                TitleLabel.Content = value;
            }
        }

        public bool IsFileDialog
        {
            set
            {
                if (value)
                {
                    Grid.SetColumnSpan(ValueTextBox, 1);
                    OpenFileDialogButton.Visibility = Visibility.Visible;
                }
            }
        }

        public TextInput()
        {
            InitializeComponent();
            //    TitleLabel.Content = Title;
        }

        public string Value
        {
            get { return ValueTextBox.Text; }
            set
            {
                ValueTextBox.Text = value;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                fbd.SelectedPath = Value;
                fbd.ShowDialog();
                Value = fbd.SelectedPath;
            }
        }
    }
}
