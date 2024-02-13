using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>

    public partial class EngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.EngineerExperience EngineerExperience { get; set; } = BO.EngineerExperience.Novice;

        public BO.Engineer Engineer
        {
            get { return (BO.Engineer)GetValue(EngineerProperty); }
            set { SetValue(EngineerProperty, value); }
        }

        public EngineerWindow(int Id = 0)
        {
            InitializeComponent();
            if (Id == 0)
            {
                Engineer = new BO.Engineer();

            }
            else
            {
                try
                {
                    Engineer = s_bl.Engineer.Read(Id)!;

                }
                catch (Exception)
                {

                    throw;
                }
            }

        }


        public static readonly DependencyProperty EngineerProperty =
            DependencyProperty.Register("Engineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));

        private void BtnSaveEngineer_Click(object sender, RoutedEventArgs e)
        {
            if (Engineer.Id == 0 || string.IsNullOrWhiteSpace(Engineer.Name) || string.IsNullOrWhiteSpace(Engineer.Email))
            {
                MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!IsValidEmail(Engineer.Email))
            {
                MessageBox.Show("Please enter a valid email address.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Engineer.Level == 0)
            {
                MessageBox.Show("Please select a task.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Button clickedButton = (Button)sender;
            object contentValue = clickedButton.Content;
            if (contentValue == "Add")
            {
                s_bl.Engineer.Create(Engineer);
            }
            else
            {
                s_bl.Engineer.Update(Engineer);
            }
            this.Close();

        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}