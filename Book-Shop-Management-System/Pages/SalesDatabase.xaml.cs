﻿using MySql.Data.MySqlClient;
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

namespace Book_Shop_Management_System.Pages
{
    public partial class SalesDatabase : Page
    {
        public class SalesDataItem
        {
            public string SaleID { get; set; }
            public string SaleInvoiceID { get; set; }
            public string SaleMemberID { get; set; }
            public string SaleBookID { get; set; }
            public string SaleQuantity { get; set; }
            public string SaleAmount { get; set; }
            public string SaleDate { get; set; }
            public string SaleEmployee { get; set; }
        }

        public SalesDatabase()
        {
            InitializeComponent();
            mysql();
        }

        private void mysql()
        {
            try
            {
                var connstr = "Server=localhost;Uid=root;Pwd=root;database=book_system";
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "select * from sales";
                        // cmd.Parameters.AddWithValue("@ID", "100");
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Sales.Items.Add(new SalesDataItem
                                {
                                    SaleID = reader["SaleID"].ToString(),
                                    SaleInvoiceID = reader["SaleInvoiceID"].ToString(),
                                    SaleMemberID = reader["SaleMemberID"].ToString(),
                                    SaleBookID = reader["SaleBookID"].ToString(),
                                    SaleQuantity = reader["SaleQuantity"].ToString(),
                                    SaleAmount = reader["SaleAmount"].ToString(),
                                    SaleDate = reader["SaleDate"].ToString(),
                                    SaleEmployee = reader["SaleEmployeeID"].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
