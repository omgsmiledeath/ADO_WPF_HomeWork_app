using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
    /// Логика взаимодействия для AddRecord.xaml
    /// </summary>
    public partial class AddRecord : Window
    {
        DataRow OleDbDR =null;
        DataRow MSSQLDR = null;
        bool isMSSQL, isOleDB = false;
        public AddRecord()
        {
            InitializeComponent();
        }
        public AddRecord(DataRow dr):this()
        {
            switch (dr.Table.Rows.Count)
            {
                case 4:
                    this.OleDbDR = dr;
                    OrdersPanel.Visibility =  Visibility.Visible;
                    CustumerPanel.Visibility = Visibility.Collapsed;
                    isOleDB = true;
                    break;
                case 6:
                    this.MSSQLDR = dr;
                    OrdersPanel.Visibility = Visibility.Collapsed;
                    CustumerPanel.Visibility = Visibility.Visible;
                    isMSSQL = true;
                    break;
            }


        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (CheckOleDBboxes())
            {
                OleDbDR["id"] = "150";
                OleDbDR["email"] = emailTxt.Text;
                OleDbDR["productId"] = productIdTxt.Text;
                OleDbDR["productDescription"] = productDescTxt.Text;
                this.DialogResult = true;
            }
            if (CheckMSSQLDBboxes())
            {
                MSSQLDR["id"] = "150";
                MSSQLDR["lastName"] = lastNameTxt.Text;
                MSSQLDR["firstName"] = firstNameTxt.Text;
                MSSQLDR["middleName"] = midleNameTxt.Text;
                MSSQLDR["phone"] = phoneTxt.Text;
                MSSQLDR["email"] = emailTxt2.Text;
                this.DialogResult = true;
            }
            else this.DialogResult= false;
        }
            
        private bool CheckOleDBboxes()
        {
            if (!isOleDB) return false;
            if (!String.IsNullOrWhiteSpace(emailTxt.Text) && !String.IsNullOrWhiteSpace(productIdTxt.Text) && !String.IsNullOrWhiteSpace(productDescTxt.Text))
            {
                foreach (char c in productIdTxt.Text)
                {
                    if (!Char.IsDigit(c))
                    {
                        MessageBox.Show("Product ID only numbers");
                        break;
                    }
                }
                return true;
            }
                else
                {
                    MessageBox.Show("Enter all fields");
                    return false;
                }
        }
        private bool CheckMSSQLDBboxes()
        {
            if (!isOleDB) return false;
            if (!String.IsNullOrWhiteSpace(firstNameTxt.Text) && 
                !String.IsNullOrWhiteSpace(lastNameTxt.Text) && 
                !String.IsNullOrWhiteSpace(midleNameTxt.Text)&&
                !String.IsNullOrWhiteSpace(emailTxt2.Text))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Enter all fields");
                return false;
            }
        }
    }
}


        
