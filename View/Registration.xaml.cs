using Mysqlx.Expect;
using newTestLibrary.dataBase;
using newTestLibrary.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace newTestLibrary.View
{
    public partial class Registration : Page
    {
        User user;
        LibraryRepository _libraryRepository;
        public Registration()
        {
            InitializeComponent();

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSaveUser_Click(sender, e);
            }
        }

        private void btnSaveUser_Click(object sender, RoutedEventArgs e)
        {
            if(tbNameUser.Text==string.Empty)
            {
                lbNameUser.Content = "Заполните поле имя";
                return; 
            }
            if(pbPasswordUser.Password!=string.Empty && pbRepPasswordUser.Password!=string.Empty)
            { 
                if(pbPasswordUser.Password!=pbRepPasswordUser.Password)
                {
                    lbPassword.Content = "Пароли не совпадают";
                    return ;
                }
            }
            user = new User();
            user.Name = tbNameUser.Text;
            user.Password=Crypto.CryptPassword(pbPasswordUser.Password);
            user.Phone=tbPhone.Text;
            _libraryRepository = new LibraryRepository();
            _libraryRepository.AddUser(user);
            NavigationService.Navigate(new MainPage());
        }
    }
}
