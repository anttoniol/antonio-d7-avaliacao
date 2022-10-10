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
using AuxiliaryClasses;

namespace antonio_d7_avaliacao
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AccessControl accessControl;
        public MainWindow()
        {
            accessControl = new AccessControl();
            InitializeComponent();
        }

        private void btnAccess_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password.ToString();
            Popup popup = new Popup();
            if (accessControl.checkData(email, password) == true)
                popup.lblMessage.Content = "Usuário autenticado!";
            else
                popup.lblMessage.Content = "Credenciais inválidas!";
            this.Opacity = 0.5;
            popup.Show();
        }
    }
}
