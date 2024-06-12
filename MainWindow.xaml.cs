using newTestLibrary.dataBase;
using newTestLibrary.Model;
using newTestLibrary.View;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace newTestLibrary
{
    
    public partial class MainWindow : Window
    {
        LibraryRepository _libraryRepository;
        public MainWindow()
        {
            InitializeComponent();
            _libraryRepository = new LibraryRepository();
            
            MainFrame.Navigate(new MainPage());
        }

       
    }
}