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
using System.Windows.Shapes;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
  
    public partial class EngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.EngineerExperience EngineerExperience { get; set; } = BO.EngineerExperience.Novice;


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
        public BO.Engineer Engineer
        {
            get { return (BO.Engineer)GetValue(EngineerProperty); }
            set { SetValue(EngineerProperty, value); }
        }

        public static readonly DependencyProperty EngineerProperty =
            DependencyProperty.Register("Engineer", typeof(IEnumerable<BO.Engineer>), typeof(EngineerWindow), new PropertyMetadata(null));

    }
}
