using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Osk1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var dictname = @"q:\temp\dictionary1.xaml";
            if (File.Exists(dictname))
            {
                using (var fs = new FileStream(dictname, FileMode.Open))
                {
                    ResourceDictionary dic =
                       (ResourceDictionary)XamlReader.Load(fs);
                    Resources.MergedDictionaries.Clear();
                    Resources.MergedDictionaries.Add(dic);
                }
            }
            var window = new MainWindow();
            window.Show();
        }
    }
}
