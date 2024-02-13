using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.Status Status { get; set; } = BO.Status.Unscheduled;

        public BO.Task Task
        {
            get { return (BO.Task)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }
        public TaskWindow(int Id = 0)
        {
            InitializeComponent();
            EngineerList = s_bl.Engineer.ReadAll()!.Select(engineer => engineer.Id).ToList();
            if (Id == 0)
            {
                Task = new BO.Task();

            }
            else
            {
                try
                {            

                    Task = s_bl.Task.Read(Id)!;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public static readonly DependencyProperty TaskProperty =
            DependencyProperty.Register("Task", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));

        private void BtnSaveTask_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            object contentValue = clickedButton.Content;
            if (contentValue == "Add")
            {
                s_bl.Task.Create(Task);
            }
            else
            {
                s_bl.Task.Update(Task);
            }
            this.Close();

        }

        public List<int> EngineerList
        {
            get { return (List<int>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }
        public static readonly DependencyProperty EngineerListProperty =
          DependencyProperty.Register("EngineerList", typeof(List<int>), typeof(EngineerListWindow), new PropertyMetadata(null));

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
