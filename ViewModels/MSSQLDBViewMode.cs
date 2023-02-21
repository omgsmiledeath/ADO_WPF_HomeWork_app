using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ADO_WPF_HomeWork_app.ViewModels
{
    internal class MSSQLDBViewMode:INotifyPropertyChanged
    {
        bool isConnectedToSql;
        public bool IsConnectedToSql => isConnectedToSql;

        private DataTable custumersDt = new DataTable();
        public DataTable CustumersDt { 
            get { return custumersDt; } 
            private set 
            { 
                custumersDt = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CustumersDt"));
            } 
        }
        private SqlDataAdapter MSSQLAdapter = new SqlDataAdapter();
        private SqlConnection MSSQLCon = new SqlConnection();

        public async Task ConnectToSQL (string conStr)
        {
            MSSQLCon.ConnectionString=conStr;
            try
            {
                await MSSQLCon.OpenAsync();
                isConnectedToSql= true;
                MSSQLAdapter = new SqlDataAdapter(@"SELECT * FROM Custumers;", MSSQLCon);
                MSSQLAdapter.Fill(CustumersDt);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "error");
            }

        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
