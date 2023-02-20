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
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Runtime.CompilerServices;


namespace ADO_WPF_HomeWork_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Переменные
        bool isConnectedToAccess;
        bool isConnectedToSql;
        private SqlDataAdapter sqlAdapter = new SqlDataAdapter();
        private SqlConnection sqlCon=new SqlConnection();
        private OleDbDataAdapter OleDbAdapter = new OleDbDataAdapter();
        private OleDbConnection oleDbCon = new OleDbConnection();
        private DataTable custumersDt = new DataTable();
        private DataTable ordersDt = new DataTable();
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            
        }

        public bool ConnectToAccess (string path)
        {
            var OleDBStringBuilder = new OleDbConnectionStringBuilder();
            OleDBStringBuilder.DataSource = @"D:\OrdersBase.accdb;DataBase Password = '123'";
            OleDBStringBuilder.Provider = "Microsoft.ACE.OLEDB.12.0";
           


            oleDbCon.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source = D:\\OrdersBase.accdb; Jet OLEDB:DataBase Password = '123'";
            try
            {
                oleDbCon.Open();
                if(oleDbCon.State==ConnectionState.Open) 
                {
                    MessageBox.Show("Connection to Access base is ok");
                    isConnectedToAccess= true;
                   
                    
                    var scom = new OleDbCommand("SELECT * FROM Orders");
                    OleDbAdapter = new OleDbDataAdapter("SELECT * FROM Orders", oleDbCon);
                    OleDbAdapter.Fill(ordersDt);
                    OrdersGrid.ItemsSource = ordersDt.DefaultView;
                    //var com = new OleDbCommand(@"INSERT INTO Orders (email,productId,productDescription)
                    //                             VALUES('1@1.ru',2,'чтото');", oleDbCon);
                    //com.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Connection to Access base its not ok");
                    isConnectedToAccess = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "ERROR");
                isConnectedToAccess = false;
            }
                        return isConnectedToAccess;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConnectToAccess("");
        }
    }
}
