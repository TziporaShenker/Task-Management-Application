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
            if (Id == 0)
            {
                Task = new BO.Task();
                Task.Engineer = new BO.EngineerInTask();
            }
            else
            {
                try
                {            

                    Task = s_bl.Task.Read(Id)!;
                    if (Task.Engineer == null) { 
                    Task.Engineer = new BO.EngineerInTask();}

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
                //if (Task.Engineer.Id==0)
                //{ Task.Engineer== null; }
                    s_bl.Task.Update(Task);
            }
            this.Close();

        }
    }
}
