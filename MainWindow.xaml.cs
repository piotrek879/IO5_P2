using Botex.database;
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

namespace Botex
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Okno defaultowe przerzuca do View (nie pisać tu nic)
            CreateSqliteDb createSqliteDb = new CreateSqliteDb();
            

            Botex.View.MainBotexView window = new Botex.View.MainBotexView();
            window.Show();
            //Botex.View.IgView window1 = new Botex.View.IgView();
            //window1.Show();
        }
        
        

    }
}
