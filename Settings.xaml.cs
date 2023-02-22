using ADO_WPF_HomeWork_app.Models;
using ADO_WPF_HomeWork_app.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace ADO_WPF_HomeWork_app
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        MSSQLDBViewMode mssqlDBVM;
        OleDBViewModel oleDBVM;
        SecurityBot sb;
        public Settings()
        {
            InitializeComponent();
        }
        public Settings(MSSQLDBViewMode mssqlDBVM,OleDBViewModel oleDBVM) :this()
        {
            this.mssqlDBVM= mssqlDBVM;
            this.oleDBVM= oleDBVM;
            sb = new SecurityBot();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(loginTxt.Text) && !string.IsNullOrWhiteSpace(passTxt.Text))
            {
                if (sb.TryToLogin(loginTxt.Text, passTxt.Text))
                {
                    MessageBox.Show("Successfully");
                    this.DialogResult = true;
                    AuthenticationPanel.Visibility = Visibility.Collapsed;
                    MSSQLPanel.Visibility = Visibility.Visible;
                    OleDBPanel.Visibility = Visibility.Visible;
                }
                else MessageBox.Show("Wrong Lorin or Password");
            }
            else MessageBox.Show("Incorrect values in boxes");
        }

        private async void msqlConButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(dataSourceTxt.Text) && !string.IsNullOrWhiteSpace(initialCatTxt.Text))
            {
                var conStr = new SqlConnectionStringBuilder()
                {
                    //DataSource = @"(localdb)\MSSQLLocalDB",
                    DataSource = dataSourceTxt.Text,
                    InitialCatalog = initialCatTxt.Text,
                    //InitialCatalog = "ADO_WPF_HomeWork_base",
                    IntegratedSecurity = true
                };
                await Dispatcher.InvokeAsync(() => mssqlDBVM.ConnectToSQL(conStr.ConnectionString));
                if (mssqlDBVM.IsConnectedToSql)
                {
                    MssqlEllipse.Fill = new SolidColorBrush() { Color = Colors.Green };
                    MssqlConStateBlock.Text = "Connection Open!";
                }
                else
                {
                    MessageBox.Show("Connection not opened '\n'Check Data Source");
                }
            }
            else MessageBox.Show("Enter Data Source and Initial Catalog");
        }

        private async void oleDBButton_Click(object sender, RoutedEventArgs e)
        {
            var shecduler = TaskScheduler.FromCurrentSynchronizationContext();
            if (!string.IsNullOrWhiteSpace(accessPathBox.Text))
            {
                var conStr = @$"Provider=Microsoft.ACE.OLEDB.12.0; Data Source ={accessPathBox.Text}";
                MessageBox.Show(conStr);
                    var t = Dispatcher.InvokeAsync(() => oleDBVM.ConnectToAccess(conStr)).Result;

                    if (oleDBVM.IsConnectedToAccess)
                    {
                        OleDbEllipse.Fill = new SolidColorBrush() { Color = Colors.Green };
                        OleDBConStateBlock.Text = $"{t.Result}";
                    }
                    else
                    {
                        MessageBox.Show($"{t.Result}");
                    }                  
            }
            else MessageBox.Show("Enter data path");
        }
    }
}
