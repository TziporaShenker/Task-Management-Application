using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.EngineerExperience EngineerExperience { get; set; } = BO.EngineerExperience.None;
        public EngineerListWindow()
        {
            InitializeComponent();
            var temp = s_bl?.Engineer.ReadAll();
            EngineerList = temp;
        }

        // עדכון רשימת המשימות לאחר סגירת חלון מהנדס
        private void UpdateListAfterEnginnerWindowClosed()
        {
            var temp = s_bl?.Engineer.ReadAll();
            EngineerList = temp;
        }
        public IEnumerable<BO.Engineer> EngineerList
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));

        private void CBEngineerExperience_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EngineerList = (EngineerExperience == BO.EngineerExperience.None)?
                s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => item.Level == EngineerExperience)!;
        }

        // פעולת התגובה ללחיצה על כפתור "הוספה"
        private void BtnAddEngineer_Click(object sender, RoutedEventArgs e)
        {
            //new EngineerWindow().ShowDialog();
            try
            {
                var engineerWindow = new EngineerWindow();
                engineerWindow.Closed += (sender, e) => UpdateListAfterEnginnerWindowClosed();
                engineerWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
            }

        }
        // פעולת התגובה ללחיצה כפולה על אובייקט ברשימה לעדכון פרטיו
        private void UpdateEngineer_click(object sender, RoutedEventArgs e)
        {
            BO.Engineer? engineer = (sender as ListView)?.SelectedItem as BO.Engineer;
            var engineerWindow = new EngineerWindow(engineer.Id);
            engineerWindow.Closed += (s, args) => UpdateListAfterEnginnerWindowClosed();
            engineerWindow.ShowDialog();
        }
    }
}
