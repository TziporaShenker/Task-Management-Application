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
            Button clickedButton = (Button)sender;
            object contentValue = clickedButton.Content;
            if (contentValue == "Add") {
                s_bl.Engineer.Create(Engineer);
            }
            else
            {
                s_bl.Engineer.Update(Engineer);
            }
            this.Close();

        }
    }
}
