using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ADO_WPF_HomeWork_app.ViewModels
{
    public class OleDBViewModel : INotifyPropertyChanged
    {
        private OleDbDataAdapter OleDbAdapter = new OleDbDataAdapter();
        private OleDbConnection oleDbCon = new OleDbConnection();
        DataTable ordersDt = new DataTable();
        public DataTable OrdersDt { get => ordersDt;
            private set { ordersDt = value;
                 PropertyChanged(this, new PropertyChangedEventArgs("OrdersDt"));
            }
        }
        bool isConnectedToAccess;
        public bool IsConnectedToAccess => isConnectedToAccess;

        public event PropertyChangedEventHandler? PropertyChanged;

        public async Task<string> ConnectToAccess(string conStr)
        {
            var OleDBStringBuilder = new OleDbConnectionStringBuilder();
            if(oleDbCon.State ==ConnectionState.Connecting) { return "Connecting..."; }
            oleDbCon.ConnectionString = conStr;
            try
            {
                await oleDbCon.OpenAsync();
                SetCommands(oleDbCon);
                OleDbAdapter.Fill(OrdersDt);
                return Task.Run( string () => {
                   
                    if (oleDbCon.State == ConnectionState.Open)
                    { 
                        isConnectedToAccess = true;
                        
                        return "Connection to Access base is ok";
                    }
                    else
                    {
                        isConnectedToAccess = false;
                        return "Connection to Access base its not ok";
                    }
                }).Result;
                
            }
            catch (Exception ex)
            {
                isConnectedToAccess = false;
                return ex.Message;
            }
            
        }
        void SetCommands(OleDbConnection oleDbCon)
        {
            //SELECT
            string sql = "SELECT * FROM Orders";
            var com = new OleDbCommand(sql, oleDbCon);
            
            OleDbAdapter.SelectCommand = com;
            //INSERT
            sql = @"INSERT INTO Orders (id,email,productId,productDescription)
                                VALUES (@id,@email,@productId,@productDescription);";

            
            OleDbAdapter.InsertCommand = new OleDbCommand(sql, oleDbCon);
            OleDbAdapter.InsertCommand.Parameters.Add("@id", OleDbType.Integer, 4, "id").SourceVersion = DataRowVersion.Current;
            OleDbAdapter.InsertCommand.Parameters.Add("@email", OleDbType.BSTR, 15, "email");
            OleDbAdapter.InsertCommand.Parameters.Add("@productId", OleDbType.Integer, 4, "productId");
            OleDbAdapter.InsertCommand.Parameters.Add("@productDescription", OleDbType.BSTR, 20, "productDescription");
            //UPDATE
            sql = @"UPDATE Orders SET 
            email =@email,
            productId = @productId,
            productDescription = @productDescription
            WHERE id = @id;";
            OleDbAdapter.UpdateCommand = new OleDbCommand(sql,oleDbCon);

            OleDbAdapter.UpdateCommand.Parameters.Add("@email", OleDbType.BSTR, 15, "email");
            OleDbAdapter.UpdateCommand.Parameters.Add("@productId", OleDbType.Integer, 4, "productId");
            OleDbAdapter.UpdateCommand.Parameters.Add("@productDescription", OleDbType.BSTR, 20, "productDescription");
            OleDbAdapter.UpdateCommand.Parameters.Add("@id", OleDbType.Integer, 4, "id");
            //DELETE
            sql = @"DELETE FROM Orders WHERE id=@id;";
            OleDbAdapter.DeleteCommand = new OleDbCommand(sql,oleDbCon);
            OleDbAdapter.DeleteCommand.Parameters.Add("@id", OleDbType.Integer, 4, "id");

            
        }
        public void Update()
        {
            //var dr = OrdersDt.NewRow();
            //dr["id"] = 99;
            //OrdersDt.Rows.Add(dr);
            // var changes = OrdersDt.GetChanges();

            //OrdersDt.AcceptChanges();
            OleDbAdapter.Update(OrdersDt);
            

        }
       
    }
}
