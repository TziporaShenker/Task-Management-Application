﻿
using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.EngineerExperience EngineerExperience { get; set; } = BO.EngineerExperience.None;
        public TaskListWindow()
        {
            InitializeComponent();
            var temp  = s_bl?.Task.ReadAll()!;
            TaskList= temp;
        }
        // עדכון רשימת המשימות לאחר סגירת חלון משימה
        private void UpdateListAfterTaskWindowClosed()
        {
            var temp = s_bl?.Task.ReadAll();
            TaskList = temp;
        }
        public IEnumerable<BO.Task> TaskList
        {
            get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.Task>), typeof(TaskListWindow), new PropertyMetadata(null));

        //private void CBEngineerExperience_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    TaskList = (EngineerExperience == BO.EngineerExperience.None) ?
        //        s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => item.Level == EngineerExperience)!;
        //}

        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               var taskWindow=new TaskWindow();
                taskWindow.Closed += (sender, e) => UpdateListAfterTaskWindowClosed();
                taskWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
            }

        }

        private void UpdateTask_click(object sender, RoutedEventArgs e)
        {
            BO.Task? task = (sender as ListView)?.SelectedItem as BO.Task;
            var taskWindow = new TaskWindow(task.Id);
            taskWindow.Closed += (s, args) => UpdateListAfterTaskWindowClosed();
            taskWindow.ShowDialog();
        }
        private void CBCopmlexity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskList = (EngineerExperience == BO.EngineerExperience.None) ?
                s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => item.Copmlexity == EngineerExperience)!;
        }
    }
}