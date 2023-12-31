﻿using Book_Shop_Management_System.DB;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;


namespace Book_Shop_Management_System.UserControls
{
    public class PurchaseComboBoxItem
    {
        public string DisplayText { get; set; }
        public string Value { get; set; }
    }

    public partial class PurchaseDataEntry : UserControl
    {
        private MySQLConnector DB = new MySQLConnector();

        public PurchaseDataEntry()
        {
            InitializeComponent();
            getBooks();
            getSuppliers();
        }

        public void clearInputs()
        {
            PurchaseQuantity.Clear();
            PurchaseETA.Clear();
            PurchaseReceived.Clear();
            PurchaseDate.SelectedDate = null;
            PurchaseBookID.SelectedValue = null;
            PurchaseSupplierID.SelectedValue = null;
        }

        public void getBooks()
        {
            try
            {
                String query = "select BookID, BookName from books";
                using (var reader = DB.FetchData(query))
                {
                    foreach (DataRow row in reader.Rows)
                    {
                        PurchaseBookID.Items.Add(new PurchaseComboBoxItem
                        {
                            DisplayText = row["BookName"].ToString(),
                            Value = row["BookID"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void getSuppliers()
        {
            try
            {
                String query = "select SupplierID, SupplierFullName from suppliers";
                using (var reader = DB.FetchData(query))
                {
                    foreach (DataRow row in reader.Rows)
                    {
                        PurchaseSupplierID.Items.Add(new PurchaseComboBoxItem
                        {
                            DisplayText = row["SupplierFullName"].ToString(),
                            Value = row["SupplierID"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex?.ToString());
            }
        }

        public bool areInputsNotEmpty()
        {
            if (string.IsNullOrWhiteSpace(PurchaseBookID.SelectedValue.ToString()) ||
                string.IsNullOrWhiteSpace(PurchaseSupplierID.SelectedValue.ToString()) ||
                string.IsNullOrWhiteSpace(PurchaseQuantity.Text) ||
                string.IsNullOrWhiteSpace(PurchaseETA.Text) ||
                string.IsNullOrWhiteSpace(PurchaseReceived.Text) ||
                PurchaseDate.SelectedDate == null
                )
            {
                MessageBox.Show("Please fill in all required fields.");
                return false;
            }
            return true;
        }

        public void submit(object sender, RoutedEventArgs e)
        {
            try
            {
                if(areInputsNotEmpty())
                {
                    Random random = new Random();
                    String PurchaseID = random.Next(1, 1000).ToString();
                    String query = "INSERT INTO purchases (PurchaseID, PurchaseBookID, PurchaseSupplierID, PurchaseQuantity, PurchaseDate, PurchaseETA, PurchaseReceived)";
                    String[] values = {
                        PurchaseID,
                        PurchaseBookID.SelectedValue.ToString(),
                        PurchaseSupplierID.SelectedValue.ToString(),
                        PurchaseQuantity.Text,
                        PurchaseDate.SelectedDate.Value.ToString("yyyy-MM-dd"),
                        PurchaseETA.Text,
                        PurchaseReceived.Text,
                    };

                    if (DB.InsertData(query, values))
                    {
                        query = "UPDATE books SET BookQuantity=BookQuantity + " + PurchaseQuantity.Text + " WHERE BookID=" + PurchaseBookID.SelectedValue.ToString();
                        if (DB.UpdateData(query))
                        {
                            MessageBox.Show("Data inserted successfully!");
                            clearInputs();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No rows were inserted. Check your data or database.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}