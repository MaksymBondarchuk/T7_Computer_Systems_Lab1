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
        List<List<int>> matrix;
        int unit;

        public MainWindow()
        {
            InitializeComponent();
        }
        

        private void bGenerateMatrix_Click(object sender, RoutedEventArgs e)
        {
            
            
        }

        void print_Matrix(List<List<int>> matrix, TextBox tb)
        {
            tb.Text = "\n";

            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Count; j++)
                    tb.Text += string.Format("{0,4}", matrix[i][j]) + "\t";
                tb.Text += "\n";
            }
        }

        private void bTransp_Click(object sender, RoutedEventArgs e)
        {
            //tbMxRes.Visibility = Visibility.Visible;
            tbMx2.Text = tbMx1.Text;
            var res = dv.Transpose(matrix, unit);
            print_Matrix(res, tbMxRes);
        }

        private void lColl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string s = ((Convert.ToInt32(lCollMx1.Content.ToString()) + 1) % 10).ToString();
            lCollMx1.Content = (s == "0")?  "1" : s;
        }

        private void lColl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            string s = (Convert.ToInt32(lCollMx1.Content.ToString()) - 1).ToString();
            lCollMx1.Content = (s == "0") ? "9" : s;
        }

        private void lRow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string s = ((Convert.ToInt32(lRowMx1.Content.ToString()) + 1) % 10).ToString();
            lRowMx1.Content = (s == "0") ? "1" : s;
        }

        private void lRow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            string s = (Convert.ToInt32(lRowMx1.Content.ToString()) - 1).ToString();
            lRowMx1.Content = (s == "0") ? "9" : s;
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

        private void tbMxGenerate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //tbMx1.Visibility = Visibility.Visible;
            //bTsp.Visibility = Visibility.Visible;

            int coll = Convert.ToInt32(lCollMx1.Content);
            int row = Convert.ToInt32(lRowMx1.Content);
            unit = Convert.ToInt32(lNumberOfProc.Content);

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
            matrix = rmg.Generate(row, coll);
            dv = new Divided();


            print_Matrix(matrix, tbMx1);
        }
    }
}
