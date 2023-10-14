﻿using Book_Shop_Management_System.DB;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Book_Shop_Management_System.UserControls
{
    public partial class MemberDataEntry : UserControl
    {
        private String selectedImagePath;
        private MySQLConnector DB = new MySQLConnector();

        public MemberDataEntry()
        {
            InitializeComponent();
        }

        private void SelectImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.png, *.bmp)|*.jpg;*.png;*.bmp|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                selectedImagePath = openFileDialog.FileName;
                MemberImage.Text = selectedImagePath;
            }
        }

        private void clearInputs()
        {
            MemberID.Clear();
            MemberFullName.Clear();
            MemberAddressLine1.Clear();
            MemberAddressLine2.Clear();
            MemberAddressCity.Clear();
            MemberAddressState.Clear();
            MemberPhoneNumber.Clear();
            MemberValid.Clear();
            MemberBeginDate.SelectedDate = null;
            MemberEndDate.SelectedDate = null;
            MemberImage.Clear();
        }

        public bool areInputsNotEmpty()
        {
            if (string.IsNullOrWhiteSpace(MemberImage.Text) ||
                string.IsNullOrWhiteSpace(MemberID.Text) ||
                string.IsNullOrWhiteSpace(MemberFullName.Text) ||
                string.IsNullOrWhiteSpace(MemberAddressLine1.Text) ||
                string.IsNullOrWhiteSpace(MemberAddressLine2.Text) ||
                string.IsNullOrWhiteSpace(MemberAddressCity.Text) ||
                string.IsNullOrWhiteSpace(MemberAddressState.Text) ||
                string.IsNullOrWhiteSpace(MemberPhoneNumber.Text) ||
                string.IsNullOrWhiteSpace(MemberValid.Text) ||
                MemberBeginDate.SelectedDate == null ||
                MemberEndDate.SelectedDate == null

                )
            {
                MessageBox.Show("Please fill in all required fields.");
                return false;
            }
            return true;
        }

        private void submit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (areInputsNotEmpty())
                {
                    String RootPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
                    String DistinationPath = RootPath + "/Assets/Members Images/" + MemberID.Text + ".png";

                    String query = "INSERT INTO members (MemberID, MemberFullName, MemberAddressLine1, MemberAddressLine2, MemberAddressCity, MemberAddressState, MemberPhoneNumber, MemberBeginDate, MemberEndDate, MemberValid, MemberImagePath)";
                    String[] values = {
                        MemberID.Text,
                        MemberFullName.Text,
                        MemberAddressLine1.Text,
                        MemberAddressLine2.Text,
                        MemberAddressCity.Text,
                        MemberAddressState.Text,
                        MemberPhoneNumber.Text,
                        MemberBeginDate.SelectedDate.Value.ToString("yyyy-MM-dd"),
                        MemberEndDate.SelectedDate.Value.ToString("yyyy-MM-dd"),
                        MemberValid.Text,
                        DistinationPath
                    };

                    if (DB.InsertData(query, values))
                    {
                        System.IO.File.Copy(selectedImagePath, DistinationPath, true);
                        selectedImagePath = "";
                        MessageBox.Show("Data inserted successfully!");
                        clearInputs();
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