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
            var s = ((Convert.ToInt32(LCollMx1.Content.ToString()) + 1) % 10).ToString();
            LCollMx1.Content = (s == "0")?  "1" : s;
            if ((string) LType.Content == "Addition")
                LCollMx2.Content = LCollMx1.Content;
            set_matrix1();
            set_matrix2();
        }

        private void lColl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = (Convert.ToInt32(LCollMx1.Content.ToString()) - 1).ToString();
            LCollMx1.Content = (s == "0") ? "9" : s;
            if ((string) LType.Content == "Addition")
                LCollMx2.Content = LCollMx1.Content;
            set_matrix1();
            set_matrix2();
        }

        private void lRow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = ((Convert.ToInt32(LRowMx1.Content.ToString()) + 1) % 10).ToString();
            LRowMx1.Content = (s == "0") ? "1" : s;
            if ((string) LType.Content != "Addition")
                LCollMx2.Content = LRowMx1.Content;
            else
                LRowMx2.Content = LRowMx1.Content;
            set_matrix1();
            set_matrix2();
        }

        private void lRow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = (Convert.ToInt32(LRowMx1.Content.ToString()) - 1).ToString();
            LRowMx1.Content = (s == "0") ? "9" : s;
            if ((string) LType.Content != "Addition")
                LCollMx2.Content = LRowMx1.Content;
            else
                LRowMx2.Content = LRowMx1.Content;
            set_matrix1();
            set_matrix2();
        }

        private void lNumberOfProc_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = ((Convert.ToInt32(LNumberOfProc.Content.ToString()) + 1) % 10).ToString();
            LNumberOfProc.Content = (s == "0") ? "1" : s;
        }

        private void lNumberOfProc_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = (Convert.ToInt32(LNumberOfProc.Content.ToString()) - 1).ToString();
            LNumberOfProc.Content = (s == "0") ? "9" : s;
        }

        private void tbMxGenerate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            set_matrix1();
        }

        private void bTsp_Click(object sender, RoutedEventArgs e)
        {
            BCount.IsEnabled = true;
            TbMx2.Visibility = Visibility.Hidden;

            GbMx2.Visibility = Visibility.Hidden;
            LCollMx2.Visibility = Visibility.Hidden;
            LSignOfMultMx2.Visibility = Visibility.Hidden;
            LRowMx2.Visibility = Visibility.Hidden;

            LType.Content = "Transposition";
            set_matrix1();
            set_matrix2();

        }

        private void bAdd_Click(object sender, RoutedEventArgs e)
        {
            BCount.IsEnabled = true;
            TbMx2.Visibility = Visibility.Visible;

            GbMx2.Visibility = Visibility.Hidden;
            LCollMx2.Visibility = Visibility.Hidden;
            LSignOfMultMx2.Visibility = Visibility.Hidden;
            LRowMx2.Visibility = Visibility.Hidden;

            LType.Content = "Addition";

            LCollMx2.Content = LCollMx1.Content;
            LRowMx2.Content = LRowMx1.Content;

            set_matrix1();
            set_matrix2();
        }

        private void bMlt_Click(object sender, RoutedEventArgs e)
        {
            BCount.IsEnabled = true;
            TbMx2.Visibility = Visibility.Visible;

            GbMx2.Visibility = Visibility.Visible;
            LCollMx2.Visibility = Visibility.Visible;
            LSignOfMultMx2.Visibility = Visibility.Visible;
            LRowMx2.Visibility = Visibility.Visible;

            LType.Content = "Multiplication";

            LCollMx2.Content = LRowMx1.Content;

            set_matrix1();
            set_matrix2();
        }

        private void lRowMx2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = ((Convert.ToInt32(LRowMx2.Content.ToString()) + 1) % 10).ToString();
            LRowMx2.Content = (s == "0") ? "1" : s;
            set_matrix2();
        }

        private void lRowMx2_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = (Convert.ToInt32(LRowMx2.Content.ToString()) - 1).ToString();
            LRowMx2.Content = (s == "0") ? "9" : s;
            set_matrix2();
        }

        private void tbMx2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            set_matrix2();
        }

        private void bCount_Click(object sender, RoutedEventArgs e)
        {
            Dv = new Divided();
            Unit = Convert.ToInt32(LNumberOfProc.Content);

            switch ((string) LType.Content)
            {
                case "Transposition":
                    print_Matrix(Dv.Transpose(_matrixA, Unit), TbMxRes);
                    break;
                case "Addition":
                    print_Matrix(Dv.Add(_matrixA, _matrixB, Unit), TbMxRes);
                    break;
                case "Multiplication":
                    print_Matrix(Dv.Multiplicate(_matrixA, _matrixB, Unit), TbMxRes);
                    break;
            }
        }

        void set_matrix1()
        {
            var row = Convert.ToInt32(LCollMx1.Content);
            var coll = Convert.ToInt32(LRowMx1.Content);
            var rmg = new RandomMatrixGenerator();

            _matrixA = rmg.Generate(row, coll);
            print_Matrix(_matrixA, TbMx1);
        }

        void set_matrix2()
        {
            var row = Convert.ToInt32(LCollMx2.Content);
            var coll = Convert.ToInt32(LRowMx2.Content);
            var rmg = new RandomMatrixGenerator();

            _matrixB = rmg.Generate(row, coll);
            print_Matrix(_matrixB, TbMx2);
        }
    }
}
