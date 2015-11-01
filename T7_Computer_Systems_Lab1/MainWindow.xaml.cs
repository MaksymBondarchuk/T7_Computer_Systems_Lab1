using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace T7_Computer_Systems_Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public Divided Dv { get; private set; }
        List<List<int>> _matrixA, _matrixB;
        public int Unit { get; private set; }

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

        private void lColl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = ((Convert.ToInt32(lCollMx1.Content.ToString()) + 1) % 10).ToString();
            lCollMx1.Content = (s == "0")?  "1" : s;
            if ((string) lType.Content == "Addition")
                lCollMx2.Content = lCollMx1.Content;
        }

        private void lColl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = (Convert.ToInt32(lCollMx1.Content.ToString()) - 1).ToString();
            lCollMx1.Content = (s == "0") ? "9" : s;
            if ((string) lType.Content == "Addition")
                lCollMx2.Content = lCollMx1.Content;
        }

        private void lRow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = ((Convert.ToInt32(lRowMx1.Content.ToString()) + 1) % 10).ToString();
            lRowMx1.Content = (s == "0") ? "1" : s;
            if ((string) lType.Content != "Addition")
                lCollMx2.Content = lRowMx1.Content;
            else
                lRowMx2.Content = lRowMx1.Content;
        }

        private void lRow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = (Convert.ToInt32(lRowMx1.Content.ToString()) - 1).ToString();
            lRowMx1.Content = (s == "0") ? "9" : s;
            if ((string) lType.Content != "Addition")
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
            var coll = Convert.ToInt32(lCollMx1.Content);
            var row = Convert.ToInt32(lRowMx1.Content);
            var rmg = new RandomMatrixGenerator();

            _matrixA = rmg.Generate(row, coll);
            print_Matrix(_matrixA, tbMx1);
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
            var coll = Convert.ToInt32(lCollMx1.Content);
            var row = Convert.ToInt32(lRowMx1.Content);
            var rmg = new RandomMatrixGenerator();

            _matrixB = rmg.Generate(row, coll);
            print_Matrix(_matrixB, tbMx2);
        }

        private void bCount_Click(object sender, RoutedEventArgs e)
        {
            Dv = new Divided();
            Unit = Convert.ToInt32(lNumberOfProc.Content);

            if ((string) lType.Content == "Transposition")
                print_Matrix(Dv.Transpose(_matrixA, Unit), tbMxRes);

        }
    }
}
