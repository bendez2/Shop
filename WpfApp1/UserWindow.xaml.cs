using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        DbService db;

        public UserWindow()
        {
            InitializeComponent();

            db = new DbService();
            db.Users.Load();
            usersTable.ItemsSource = db.Users.Local.ToBindingList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }
    }
}
