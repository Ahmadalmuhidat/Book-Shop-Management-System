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
using static Book_Shop_Management_System.Pages.SalesDatabase;

namespace Book_Shop_Management_System.Pages
{
    public class PurchasesDataItem
    {
        public string PurchaseID { get; set; }
        public string PurchaseBookID { get; set; }
        public string PurchaseSupplierID { get; set; }
        public string PurchaseQuantity { get; set; }
        public string PurchaseDate { get; set; }
        public string PurchaseETA { get; set; }
        public string PurchaseReceived { get; set; }
        public string PurchaseInvoice { get; set; }
    }

    public partial class PurchasesDatabase : Page
    {
        public PurchasesDatabase()
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
                        cmd.CommandText = "select * from purchases";
                        // cmd.Parameters.AddWithValue("@ID", "100");
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Purchases.Items.Add(new PurchasesDataItem
                                {
                                    PurchaseID = reader["PurchaseID"].ToString(),
                                    PurchaseBookID = reader["PurchaseBookID"].ToString(),
                                    PurchaseSupplierID = reader["PurchaseSupplierID"].ToString(),
                                    PurchaseQuantity = reader["PurchaseQuantity"].ToString(),
                                    PurchaseDate = reader["PurchaseDate"].ToString(),
                                    PurchaseETA = reader["PurchaseETA"].ToString(),
                                    PurchaseReceived = reader["PurchaseReceived"].ToString(),
                                    PurchaseInvoice = reader["PurchaseInvoice"].ToString(),
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
