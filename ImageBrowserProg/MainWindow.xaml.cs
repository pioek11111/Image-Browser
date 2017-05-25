using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Globalization;

namespace ImageBrowserProg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //List<string> paths;
        ObservableCollection<string> paths;
        int heigh;
        public MainWindow()
        {
            InitializeComponent();
            paths = new ObservableCollection<string>();
            heigh = 100;
            paths.Add("C:\\Users\\Piotrek\\Desktop\\WCveg.jpg");
            paths.Add("C:\\Users\\Piotrek\\Desktop\\WCveg.jpg");
            listV.DataContext = paths;
            slider.DataContext = heigh;
            //listView.DataContext = paths;
            //border.Background = new SolidColorBrush(Colors.Transparent);
        }
        
        public int Heigh
        {
            get { return heigh; }
            set
            {
                if (Equals(value, heigh)) return;
                heigh = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> Paths
        {
            get { return paths; }
            set
            {
                if (Equals(value, paths)) return;
                paths = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            Window1 w = new Window1(ofd.FileName);
            w.ShowDialog();
        }

        private void add(object sender, RoutedEventArgs e)
        {
            paths.Add("C:\\Users\\Piotrek\\Desktop\\WCveg.jpg");
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void openFolder(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog f = new System.Windows.Forms.FolderBrowserDialog();
            f.ShowDialog();
            var dirInfo = new DirectoryInfo(f.SelectedPath);
            FileInfo[] info = dirInfo.GetFiles("*.jpg*");
            foreach (var item in info)
            {
                paths.Add(item.FullName);
            }
        }

        private void clickOnImage(object sender, MouseButtonEventArgs e)
        {
            var v = sender as Border;
            Window1 w = new Window1(v.DataContext.ToString());
            w.Show();
            e.Handled = true;
        }
    }

    [ValueConversion(typeof(string), typeof(string))]
    class ToFileNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.IO.Path.GetFileName(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
