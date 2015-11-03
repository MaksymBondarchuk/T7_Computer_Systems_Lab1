using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private Divided Dv { get; set; }
        private Common Cm { get; set; }
        private List<List<int>> _matrixA;
        private List<List<int>> _matrixB;

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
            LCollMx1.Content = (s == "0") ? "1" : s;
            if ((string)LType.Content == "Addition")
                LCollMx2.Content = LCollMx1.Content;
            set_matrix1();
            set_matrix2();
        }

        private void lColl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = (Convert.ToInt32(LCollMx1.Content.ToString()) - 1).ToString();
            LCollMx1.Content = (s == "0") ? "9" : s;
            if ((string)LType.Content == "Addition")
                LCollMx2.Content = LCollMx1.Content;
            set_matrix1();
            set_matrix2();
        }

        private void lRow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = ((Convert.ToInt32(LRowMx1.Content.ToString()) + 1) % 10).ToString();
            LRowMx1.Content = (s == "0") ? "1" : s;
            if ((string)LType.Content != "Addition")
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
            if ((string)LType.Content != "Addition")
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
            LTime.Content = "<NONE>";
            TbMxRes.Text = "";
            BCount.IsEnabled = true;
            TbMx1.Visibility = Visibility.Visible;
            TbMx2.Visibility = Visibility.Hidden;
            LAlpha.Visibility = Visibility.Hidden;
            LTextAlpha.Visibility = Visibility.Hidden;

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
            LTime.Content = "<NONE>";
            TbMxRes.Text = "";
            BCount.IsEnabled = true;
            TbMx1.Visibility = Visibility.Visible;
            TbMx2.Visibility = Visibility.Visible;
            LAlpha.Visibility = Visibility.Hidden;
            LTextAlpha.Visibility = Visibility.Hidden;

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
            LTime.Content = "<NONE>";
            TbMxRes.Text = "";
            BCount.IsEnabled = true;
            TbMx1.Visibility = Visibility.Visible;
            TbMx2.Visibility = Visibility.Visible;
            LAlpha.Visibility = Visibility.Visible;
            LTextAlpha.Visibility = Visibility.Visible;

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

        private async void DoPbDv()
        {
            int currentProgress;
            do
            {
                lock (Dv.FreeRows)
                {
                    currentProgress = Dv.Progress;
                }
                PbWork.Value = currentProgress;

                await Task.Delay(100);
            } while (currentProgress < 100);
        }

        private async void DoPbCm()
        {
            int currentProgress;
            do
            {
                lock (Cm.FreeRows)
                {
                    currentProgress = Cm.Progress;
                }
                PbWork.Value = currentProgress;

                await Task.Delay(100);
            } while (currentProgress < 100);
        }

        private async void bCount_Click(object sender, RoutedEventArgs e)
        {
            Dv = new Divided();
            Cm = new Common();
            PbWork.Value = 0;
            var unit = Convert.ToInt32(LNumberOfProc.Content);
            var alpha = Convert.ToInt32(LAlpha.Content);
            PbWork.Visibility = Visibility.Visible;

            if (RbDv.IsChecked != null && (bool)RbDv.IsChecked)
                switch ((string)LType.Content)
                {
                    case "Transposition":
                        DoPbDv();
                        print_Matrix(await Dv.Transpose(_matrixA, unit), TbMxRes);
                        break;
                    case "Addition":
                        DoPbDv();
                        print_Matrix(await Dv.Add(_matrixA, _matrixB, unit), TbMxRes);
                        break;
                    case "Multiplication":
                        DoPbDv();
                        print_Matrix(await Dv.Multiplicate(_matrixA, _matrixB, unit, alpha), TbMxRes);
                        break;
                }
            else
                switch ((string)LType.Content)
                {
                    case "Transposition":
                        DoPbCm();
                        print_Matrix(await Cm.Transpose(_matrixA, unit), TbMxRes);
                        break;
                    case "Addition":
                        DoPbCm();
                        print_Matrix(await Cm.Add(_matrixA, _matrixB, unit), TbMxRes);
                        break;
                    case "Multiplication":
                        DoPbCm();
                        print_Matrix(await Cm.Multiplicate(_matrixA, _matrixB, unit, alpha), TbMxRes);
                        break;
                }
            LTime.Content = (Dv.Time > Cm.Time) ? Dv.Time.ToString() : Cm.Time.ToString();
        }

        private void set_matrix1()
        {
            var row = Convert.ToInt32(LCollMx1.Content);
            var coll = Convert.ToInt32(LRowMx1.Content);
            var rmg = new RandomMatrixGenerator();

            _matrixA = rmg.Generate(row, coll);
            print_Matrix(_matrixA, TbMx1);
        }

        private void LAlpha_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = ((Convert.ToInt32(LAlpha.Content.ToString()) + 1) % 10).ToString();
            LAlpha.Content = (s == "0") ? "1" : s;
        }

        private void LAlpha_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = (Convert.ToInt32(LAlpha.Content.ToString()) - 1).ToString();
            LAlpha.Content = (s == "0") ? "9" : s;
        }

        private void set_matrix2()
        {
            var row = Convert.ToInt32(LCollMx2.Content);
            var coll = Convert.ToInt32(LRowMx2.Content);
            var rmg = new RandomMatrixGenerator();

            _matrixB = rmg.Generate(row, coll);
            print_Matrix(_matrixB, TbMx2);
        }
    }
}
