using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace XsiO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int[,] matrice = new int[3, 3];
        private BitmapImage xImage;
        private BitmapImage oImage;
        private bool isXTurn = true;

        public MainWindow()
        {
            InitializeComponent();

            InitializeazaMatrice();
            InitializeazaTablaJoc();

            xImage = new BitmapImage(new Uri("Imagini/x.png", UriKind.Relative));
            oImage = new BitmapImage(new Uri("Imagini/o.png", UriKind.Relative));
        }

        private void OnCellClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.Content is Image img)
                {
                    if (img.Visibility == Visibility.Visible)
                        return;

                    img.Source = isXTurn ? xImage : oImage;
                    img.Visibility = Visibility.Visible;

                    isXTurn = !isXTurn;

                    var border = VisualTreeHelper.GetParent(button) as Border;
                    if (border == null)
                        return;

                    int row = Grid.GetRow(border);
                    int col = Grid.GetColumn(border);
                    matrice[row, col] = isXTurn ? 0 : 1; // 0 for O, 1 for X

                    VerificaJocCastigat();
                }
            }
        }

        private void VerificaJocCastigat()
        {
            if (VerificaLinie() || VerificaColoana() || VerificaDiagonalaPrimara() || VerificaDiagonalaSecundara())
            {
                MessageBox.Show("Ai câștigat!", "Felicitări", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool VerificaLinie()
        {
            for (int linie = 0; linie < 3; linie++)
            {
                int primulElementLinie = matrice[linie, 0];
                bool jocCastigat = true;

                for (int col = 1; col < 3; col++)
                {
                    if (matrice[linie, col] != primulElementLinie)
                    {
                        jocCastigat = false;
                        break;
                    }
                }

                if (jocCastigat == true)
                    return true;
            }

            return false;
        }

        private bool VerificaColoana()
        {
            for (int col = 0; col < 3; col++)
            {
                int primulElementColoana = matrice[0, col];
                bool jocCastigat = true;

                for (int linie = 1; linie < 3; linie++)
                {
                    if (matrice[linie, col] != primulElementColoana)
                    {
                        jocCastigat = false;
                        break;
                    }
                }

                if (jocCastigat == true)
                    return true;
            }

            return false;
        }

        private bool VerificaDiagonalaPrimara()
        {
            if (matrice[0, 0] == matrice[1, 1] && matrice[1, 1] == matrice[2, 2])
            {
                return true;
            }

            return false;
        }

        private bool VerificaDiagonalaSecundara()
        {
            if (matrice[0, 2] == matrice[1, 1] && matrice[1, 1] == matrice[2, 0])
            {
                return true;
            }

            return false;
        }

        private void JoacaDinNou_Click(object sender, RoutedEventArgs e)
        {
            InitializeazaMatrice();
            InitializeazaTablaJoc();
        }

        private void InitializeazaMatrice()
        {
            matrice = new int[3, 3]
            {
                { -1, -2, -3 },
                { -4, -5, -6 },
                { -7, -8, -9 }
            };
        }

        private void InitializeazaTablaJoc()
        {
            foreach (UIElement element in TablaJoc.Children)
            {
                if (element is Border border && border.Child is Button button)
                {
                    if (button.Content is Image image)
                    {
                        image.Visibility = Visibility.Collapsed;
                        image.Source = null;
                    }
                }
            }

            isXTurn = true;
        }

    }
}