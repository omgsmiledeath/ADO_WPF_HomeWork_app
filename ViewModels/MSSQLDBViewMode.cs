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
        public event PropertyChangedEventHandler? PropertyChanged;
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
            if (!IsConnectedToSql)
            {
                
                MSSQLCon.ConnectionString = conStr;
                try
                {
                   MSSQLAdapter =  await Task.Run(SqlDataAdapter () =>
                    {
                        MSSQLCon.Open();
                        isConnectedToSql = true;
                        var Adapter = new SqlDataAdapter(@"SELECT * FROM Custumers;", MSSQLCon);
                        return Adapter;
                    });
                    SetCommands(MSSQLCon);
                    MSSQLAdapter.Fill(CustumersDt);

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "error");
                }
            }
        }

        private void SetCommands(SqlConnection con)
        {

            //SELECT
            var sql ="SELECT * FROM Custumers";
            MSSQLAdapter.SelectCommand = new SqlCommand(sql, con);
            //INSERT
            sql = @"INSERT INTO Custumers (lastName,firstName,middleName,phone,email) VALUES (@lastname,@firstName,@middleName,@phone,@email);";
            MSSQLAdapter.InsertCommand = new SqlCommand(sql, con);
            MSSQLAdapter.InsertCommand.Parameters.Add("@id",SqlDbType.Int,4,"id").Direction=ParameterDirection.Output;
            MSSQLAdapter.InsertCommand.Parameters.Add("@lastName", SqlDbType.NVarChar, 20, "lastName");
            MSSQLAdapter.InsertCommand.Parameters.Add("@firstName", SqlDbType.NVarChar, 20, "firstName");
            MSSQLAdapter.InsertCommand.Parameters.Add("@middleName", SqlDbType.NVarChar, 20, "middleName");
            MSSQLAdapter.InsertCommand.Parameters.Add("@phone", SqlDbType.NVarChar, 20, "phone");
            MSSQLAdapter.InsertCommand.Parameters.Add("@email", SqlDbType.NVarChar, 20, "email");
            //UPDATE
            sql = @"UPDATE Custumers SET lastName = @lastName,firstName=@firstName,middleName=@middleName,phone=@phone,email=@email WHERE id=@id";
            MSSQLAdapter.UpdateCommand = new SqlCommand(sql, con);
            MSSQLAdapter.UpdateCommand.Parameters.Add("@id", SqlDbType.Int, 4, "id").SourceVersion=DataRowVersion.Original;
            MSSQLAdapter.UpdateCommand.Parameters.Add("@lastName", SqlDbType.NVarChar, 20, "lastName");
            MSSQLAdapter.UpdateCommand.Parameters.Add("@firstName", SqlDbType.NVarChar, 20, "firstName");
            MSSQLAdapter.UpdateCommand.Parameters.Add("@middleName", SqlDbType.NVarChar, 20, "middleName");
            MSSQLAdapter.UpdateCommand.Parameters.Add("@phone", SqlDbType.NVarChar, 20, "phone");
            MSSQLAdapter.UpdateCommand.Parameters.Add("@email", SqlDbType.NVarChar, 20, "email");
            //DELETE
            sql = "DELETE FROM Custumers WHERE id = @id";
            MSSQLAdapter.DeleteCommand = new SqlCommand(sql, con);
            MSSQLAdapter.DeleteCommand.Parameters.Add("@id", SqlDbType.Int, 4, "id");
        }
        public void Update()
        {
            MSSQLAdapter.Update(CustumersDt);
        }
    }
}
