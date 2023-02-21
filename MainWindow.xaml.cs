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
using ADO_WPF_HomeWork_app.ViewModels;

namespace ADO_WPF_HomeWork_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Переменные
        DataRowView dr;
        OleDBViewModel oleDBVM;
        MSSQLDBViewMode mssqlDBVM;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            oleDBVM = new OleDBViewModel();
            OrdersGrid.DataContext = oleDBVM.OrdersDt;
            mssqlDBVM = new MSSQLDBViewMode();
            CustumersGrid.DataContext = mssqlDBVM.CustumersDt;
        }

       

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await oleDBVM.ConnectToAccess("");
        }

        private void btnUpdateOleDB_Click(object sender, RoutedEventArgs e)
        {
            var dr = oleDBVM.OrdersDt.NewRow();
            AddRecord ar = new AddRecord(dr);
            ar.ShowDialog();
            if (ar.DialogResult == true)
            {
                oleDBVM.OrdersDt.Rows.Add(dr);
            }
            oleDBVM.Update();
        }

        

        private void DeleteMenu_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersGrid.SelectedItem != null)
            {
                (OrdersGrid.SelectedItem as DataRowView).Row.Delete();
                oleDBVM.Update();
            }
            else MessageBox.Show("Select row for delete");
        }

        private void OrdersGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dr != null)
            {
                dr.EndEdit();
                oleDBVM.Update();
            }
        }

        private void OrdersGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            

            if (OrdersGrid.SelectedItem != null)
            {
                dr = (DataRowView)OrdersGrid.SelectedItem;
                dr.BeginEdit();
            }
        }

        private void CustumersGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dr != null)
            {
                dr.EndEdit();
               // UPDATE MSSQLDATAADAPTER
            }
        }

        private void CustumersGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (CustumersGrid.SelectedItem != null)
            {
                dr = (DataRowView)CustumersGrid.SelectedItem;
                dr.BeginEdit();
            }
        }

        private void CustumersDeleteMenu_Click(object sender, RoutedEventArgs e)
        {
            if (CustumersGrid.SelectedItem != null)
            {
                (CustumersGrid.SelectedItem as DataRowView).Row.Delete();
                // UPDATE MSSQLDATAADAPTER
            }
            else MessageBox.Show("Select row for delete");
        }

        private async void mssqlConButton_Click(object sender, RoutedEventArgs e)
        {
            var conStr = new SqlConnectionStringBuilder()
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "ADO_WPF_HomeWork_base",
                IntegratedSecurity= true
            };
            await mssqlDBVM.ConnectToSQL(conStr.ConnectionString);
        }
    }
}
