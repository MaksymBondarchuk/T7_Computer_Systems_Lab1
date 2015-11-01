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
        Divided _dv;
        List<List<int>> _matrix;
        int _unit;

        public MainWindow()
        {
            InitializeComponent();
        }


        static void print_Matrix(IEnumerable<List<int>> matrix, TextBox tb)
        {
            tb.Text = "\n";

            foreach (var t in matrix)
            {
                foreach (var t1 in t)
                    tb.Text += $"{t1,4}" + "\t";
                tb.Text += "\n";
            }
        }

        private void bTransp_Click(object sender, RoutedEventArgs e)
        {
            //tbMxRes.Visibility = Visibility.Visible;
            tbMx2.Text = tbMx1.Text;
            var res = _dv.Transpose(_matrix, _unit);
            print_Matrix(res, tbMxRes);
        }

        private void lColl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = ((Convert.ToInt32(lCollMx1.Content.ToString()) + 1) % 10).ToString();
            lCollMx1.Content = (s == "0")?  "1" : s;
            if (lType.Content == "Addition")
                lCollMx2.Content = lCollMx1.Content;
        }

        private void lColl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = (Convert.ToInt32(lCollMx1.Content.ToString()) - 1).ToString();
            lCollMx1.Content = (s == "0") ? "9" : s;
            if (lType.Content == "Addition")
                lCollMx2.Content = lCollMx1.Content;
        }

        private void lRow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = ((Convert.ToInt32(lRowMx1.Content.ToString()) + 1) % 10).ToString();
            lRowMx1.Content = (s == "0") ? "1" : s;
            if (lType.Content != "Addition")
                lCollMx2.Content = lRowMx1.Content;
            else
                lRowMx2.Content = lRowMx1.Content;
        }

        private void lRow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = (Convert.ToInt32(lRowMx1.Content.ToString()) - 1).ToString();
            lRowMx1.Content = (s == "0") ? "9" : s;
            if (lType.Content != "Addition")
                lCollMx2.Content = lRowMx1.Content;
            else
                lRowMx2.Content = lRowMx1.Content;
        }

        private void lNumberOfProc_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = ((Convert.ToInt32(lNumberOfProc.Content.ToString()) + 1) % 10).ToString();
            lNumberOfProc.Content = (s == "0") ? "1" : s;
        }

        private void lNumberOfProc_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = (Convert.ToInt32(lNumberOfProc.Content.ToString()) - 1).ToString();
            lNumberOfProc.Content = (s == "0") ? "9" : s;
        }

        private void tbMxGenerate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //tbMx1.Visibility = Visibility.Visible;
            //bTsp.Visibility = Visibility.Visible;

            var coll = Convert.ToInt32(lCollMx1.Content);
            var row = Convert.ToInt32(lRowMx1.Content);
            _unit = Convert.ToInt32(lNumberOfProc.Content);

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

            var rmg = new RandomMatrixGenerator();
            _matrix = rmg.Generate(row, coll);
            _dv = new Divided();


            print_Matrix(_matrix, tbMx1);
        }

        private void bTsp_Click(object sender, RoutedEventArgs e)
        {
            tbMx2.Visibility = Visibility.Hidden;

            gbMx2.Visibility = Visibility.Hidden;
            lCollMx2.Visibility = Visibility.Hidden;
            lSignOfMultMx2.Visibility = Visibility.Hidden;
            lRowMx2.Visibility = Visibility.Hidden;

            lType.Content = "Transposition";
            
        }

        private void bAdd_Click(object sender, RoutedEventArgs e)
        {
            tbMx2.Visibility = Visibility.Visible;

            gbMx2.Visibility = Visibility.Hidden;
            lCollMx2.Visibility = Visibility.Hidden;
            lSignOfMultMx2.Visibility = Visibility.Hidden;
            lRowMx2.Visibility = Visibility.Hidden;

            lType.Content = "Addition";

            lCollMx2.Content = lCollMx1.Content;
            lRowMx2.Content = lRowMx1.Content;
        }

        private void bMlt_Click(object sender, RoutedEventArgs e)
        {

            tbMx2.Visibility = Visibility.Visible;

            gbMx2.Visibility = Visibility.Visible;
            lCollMx2.Visibility = Visibility.Visible;
            lSignOfMultMx2.Visibility = Visibility.Visible;
            lRowMx2.Visibility = Visibility.Visible;

            lType.Content = "Multiplication";

            lCollMx2.Content = lRowMx1.Content;
        }

        private void lRowMx2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = ((Convert.ToInt32(lRowMx2.Content.ToString()) + 1) % 10).ToString();
            lRowMx2.Content = (s == "0") ? "1" : s;
        }

        private void lRowMx2_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = (Convert.ToInt32(lRowMx2.Content.ToString()) - 1).ToString();
            lRowMx2.Content = (s == "0") ? "9" : s;
        }

        private void tbMx2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var coll = Convert.ToInt32(lCollMx2.Content);
            var row = Convert.ToInt32(lRowMx2.Content);
            _unit = Convert.ToInt32(lNumberOfProc.Content);

            var rmg = new RandomMatrixGenerator();
            _matrix = rmg.Generate(row, coll);

            print_Matrix(_matrix, tbMx2);
        }
    }
}
