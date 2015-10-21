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

namespace T7_Computer_Systems_Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Divided dv;

        public MainWindow()
        {
            InitializeComponent();
        }
        

        private void bGenerateMatrix_Click(object sender, RoutedEventArgs e)
        {
            tbMxGenerate.Visibility = Visibility.Visible;
            bTransp.Visibility = Visibility.Visible;

            int coll = Convert.ToInt32(lColl.Content);
            int row = Convert.ToInt32(lRow.Content);
            int unit = Convert.ToInt32(lNumberOfProc.Content);

            //Random rand = new Random();
            //List<List<int>> matrix = new List<List<int>>();

            //for (int i = 0; i < coll; i++)
            //{
            //    matrix.Add(new List<int>(row));
            //    for (int j = 0; j < row; j++)
            //        matrix[i].Add(rand.Next(-9, 10));
            //}

            //tbMxGenerate.Text = "";

            //for (int i = 0; i < coll; i++)
            //{
            //    for (int j = 0; j < row; j++)
            //        tbMxGenerate.Text += matrix[i][j].ToString() + " ";
            //    tbMxGenerate.Text += "\n";
            //}
            //print_Matrix(matrix, tbMxGenerate);

            //MessageBox.Show("Generate");

            //while (tbMxTranspon.Visibility == Visibility.Hidden) ;
            //System.Threading.Thread.Sleep(10);

            RandomMatrixGenerator rmg = new RandomMatrixGenerator();
            var matrix = rmg.Generate(row, coll);
            dv = new Divided(matrix, unit);

            
            print_Matrix(matrix, tbMxGenerate);
            
        }

        void print_Matrix(List<List<int>> matrix, TextBox tb)
        {
            tb.Text = "";

            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Count; j++)
                    tb.Text += string.Format("{0,4}", matrix[i][j]);
                tb.Text += "\n";
            }
        }

        private void bTransp_Click(object sender, RoutedEventArgs e)
        {
            tbMxTranspon.Visibility = Visibility.Visible;
            var res = dv.Transposition();
            print_Matrix(res, tbMxTranspon);
        }

        private void lColl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string s = ((Convert.ToInt32(lColl.Content.ToString()) + 1) % 10).ToString();
            lColl.Content = (s == "0")?  "1" : s;
        }

        private void lColl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            string s = (Convert.ToInt32(lColl.Content.ToString()) - 1).ToString();
            lColl.Content = (s == "0") ? "9" : s;
        }

        private void lRow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string s = ((Convert.ToInt32(lRow.Content.ToString()) + 1) % 10).ToString();
            lRow.Content = (s == "0") ? "1" : s;
        }

        private void lRow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            string s = (Convert.ToInt32(lRow.Content.ToString()) - 1).ToString();
            lRow.Content = (s == "0") ? "9" : s;
        }

        private void lNumberOfProc_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string s = ((Convert.ToInt32(lNumberOfProc.Content.ToString()) + 1) % 10).ToString();
            lNumberOfProc.Content = (s == "0") ? "1" : s;
        }

        private void lNumberOfProc_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            string s = (Convert.ToInt32(lNumberOfProc.Content.ToString()) - 1).ToString();
            lNumberOfProc.Content = (s == "0") ? "9" : s;
        }
    }
}
