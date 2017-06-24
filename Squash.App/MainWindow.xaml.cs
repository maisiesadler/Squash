using Newtonsoft.Json;
using Squash;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Squash.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _configFileLocation;

        public MainWindow()
        {
            _configFileLocation = ConfigurationManager.AppSettings["ConfigFileLocation"];
            
            InitializeComponent();
            TryReadConfig();
        }

        private void TryReadConfig()
        {
            try
            {
                SquashConfiguration config = new SquashConfiguration(_configFileLocation);
                if (string.IsNullOrWhiteSpace(config.ErrorMessage))
                {
                    TitleInput.Value = config.Title;
                    InputDirectoryInput.Value = config.InputDirectory;
                    OutputDirectoryInput.Value = config.OutputDirectory;
                }
            }
            catch
            {
                //do nothing
            }
        }

        private SquashConfiguration GetSquashConfiguration()
        {
            var squashConfiguration = new SquashConfiguration(title: TitleInput.Value,
                                                inputDirectory: InputDirectoryInput.Value,
                                                outputDirectory: OutputDirectoryInput.Value);

            return squashConfiguration;
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            var squasher = new Squasher();
            SquashConfiguration squashConfiguration = GetSquashConfiguration();

            if (!squashConfiguration.Validate())
            {
                MessageBox.Show("Invalid Config: " + squashConfiguration.ErrorMessage);
            }
            else
            {
                squasher.Squash(squashConfiguration);
                MessageBox.Show("Done!");
            //    Directory.Open(squashConfiguration.OutputDirectory, FileMode.Open);
            }
        }

        private void SaveConfig_Click(object sender, RoutedEventArgs e)
        {
            var squashConfiguration = GetSquashConfiguration();
            var json = JsonConvert.SerializeObject(squashConfiguration, Formatting.Indented);
            File.WriteAllText( _configFileLocation, json);

            MessageBox.Show("Done!");
        }
    }
}
