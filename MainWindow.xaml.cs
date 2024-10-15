using System.Windows;
using System.Windows.Controls;

namespace ExtraLogikaOefening
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private double _lidgeldBasis = 100d;
        private int _ouderdom = 0;
        private int _kortingOuderdomBoven50 = 0;
        private int _kortingOuderdomOnder18 = 0;
        private int _kortingStempel = 0;
        private int _kortingsBon = 0;
        private int _kortingGeslacht = 0;

        private void btnVulDeWaarden_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            txtOuderdom.Text = random.Next(10, 100).ToString();

            if (random.Next(2) == 1)
            {
                radJa.IsChecked = true;
            }
            else
            {
                radNee.IsChecked = true;
            }

            switch (random.Next(4))
            {
                case 0:
                    radGeenKorting.IsChecked = true;
                    break;

                case 1:
                    radKorting10.IsChecked = true;
                    break;

                case 2:
                    radKorting20.IsChecked = true;
                    break;

                case 3:
                    radKorting25.IsChecked = true;
                    break;
            }

            if (random.Next(2) == 1)
            {
                radVrouw.IsChecked = true;
            }
            else
            {
                radMan.IsChecked = true;
            }

        }

        private void btnBereken1_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckGegevens())
            {
                return;
            }

            SetStandaardKortingen();
            CalculateLidgeld();            
        }

        

        private void btnBereken2_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckGegevens())
            {
                return;
            }

            SetStandaardKortingen();
            
            if (_kortingOuderdomBoven50 > 0)
            {
                int somSpeciaalKorting = 0;
                for (int i = 1; i <= _kortingOuderdomBoven50; i++)
                {
                    somSpeciaalKorting += i;
                }
                _kortingOuderdomBoven50 = somSpeciaalKorting;
            }
            CalculateLidgeld();
        }

        private void CalculateLidgeld()
        {
            int somVanProcentueleKortingen = _kortingOuderdomOnder18 + _kortingsBon;
            double lidgeld = (_lidgeldBasis - _kortingStempel - _kortingGeslacht - _kortingOuderdomBoven50) * ((100d - somVanProcentueleKortingen) / 100);
            if (lidgeld < 0)
            {
                lidgeld = 0;
            }

            ResetVariables();
            MessageBox.Show("Lidgeld is " + Math.Round(lidgeld, 2).ToString("F2") + " €.");
        }

        private void SetStandaardKortingen()
        {
            if (_ouderdom < 18)
            {
                _kortingOuderdomOnder18 = 50;
            }
            else if (_ouderdom > 50)
            {
                _kortingOuderdomBoven50 = _ouderdom - 50;
            }

            RadioButton selectedRadioButton1 = SelectedRadioButton(spStempel);
            RadioButton selectedRadioButton2 = SelectedRadioButton(spKortingsBon);
            RadioButton selectedRadioButton3 = SelectedRadioButton(spGeslacht);

            switch (selectedRadioButton1.Content.ToString())
            {
                case "Ja":
                    _kortingStempel = 25;
                    break;

                case "Nee":
                    break;

                default:
                    break;
            }

            switch (selectedRadioButton2.Content.ToString())
            {
                case "10%":
                    _kortingsBon = 10;
                    break;

                case "20%":
                    _kortingsBon = 20;
                    break;

                case "25%":
                    _kortingsBon = 25;
                    break;

                default:
                    break;
            }

            switch (selectedRadioButton3.Content.ToString())
            {
                case "Vrouw":
                    _kortingGeslacht = 15;
                    break;

                default:
                    break;
            }
        }

        private void ResetVariables()
        {
            _ouderdom = 0;
            _kortingOuderdomBoven50 = 0;
            _kortingOuderdomOnder18 = 0;
            _kortingStempel = 0;
            _kortingsBon = 0;
            _kortingGeslacht = 0;
        }

        private bool CheckGegevens()
        {
            if (string.IsNullOrEmpty(txtOuderdom.Text))
            {
                MessageBox.Show("Vul je _ouderdom in!");
                return false;
            }
            else if (!Int32.TryParse(txtOuderdom.Text, out _ouderdom) || _ouderdom > 120)
            {
                MessageBox.Show("Ouderdom moet een geheel getal zijn tussen 5 en 120");
                return false;
            };

            return true;
        }

        private RadioButton SelectedRadioButton(StackPanel spStempel)
        {
            foreach (RadioButton rb in spStempel.Children.OfType<RadioButton>())
            {
                if (rb.IsChecked == true)
                {
                    return rb;
                }
            }

            RadioButton noRadioButtonSelected = new RadioButton();
            noRadioButtonSelected.Content = "No radiobutton selected";
            return noRadioButtonSelected;
        }        
    }
}