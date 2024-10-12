using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void btnVulDeWaarde_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            txtOuderdom.Text = random.Next(10, 100).ToString();
        }

        private void btnBereken1_Click(object sender, RoutedEventArgs e)
        {
            double lidgeldBasis = 100d;
            int ouderdom;

            if (string.IsNullOrEmpty(txtOuderdom.Text))
            {
                MessageBox.Show("Vul je ouderdom in!");
                return;
            } 
            else if (!Int32.TryParse(txtOuderdom.Text, out ouderdom) || ouderdom > 120)
            {
                MessageBox.Show("Ouderdom moet een geheel getal zijn tussen 5 en 120");
                return;
            }

            int kortingOuderdomBoven50 = 0;
            int kortingOuderdomOnder18 = 0;
            int kortingStempel = 0;
            int kortingsBon = 0;
            int kortingGeslacht = 0;

            if (ouderdom < 18)
            {
                kortingOuderdomOnder18 = 50;
            }
            else if (ouderdom > 50)
            {
                kortingOuderdomBoven50 = ouderdom - 50;
            }

            RadioButton selectedRadioButton1 = SelectedRadioButton(spStempel);
            RadioButton selectedRadioButton2 = SelectedRadioButton(spKortingsBon);
            RadioButton selectedRadioButton3 = SelectedRadioButton(spGeslacht);

            switch (selectedRadioButton1.Content.ToString())
            {
                case "Ja":
                    kortingStempel = 25;
                    break;

                case "Nee":
                    break;

                default:
                    break;
            }

            switch (selectedRadioButton2.Content.ToString())
            {
                case "10%":
                    kortingsBon = 10;
                    break;

                case "20%":
                    kortingsBon = 20;
                    break;

                case "25%":
                    kortingsBon = 25;
                    break;

                default:
                    break;
            }

            switch (selectedRadioButton3.Content.ToString())
            {
                case "Vrouw":
                    kortingGeslacht = 15;
                    break;                

                default:
                    break;
            }

            int somVanProcentueleKortingen = kortingOuderdomOnder18 + kortingsBon;
            double lidgeld = (lidgeldBasis - kortingStempel - kortingGeslacht - kortingOuderdomBoven50) * ((100d - somVanProcentueleKortingen) / 100);
            if (lidgeld < 0)
            {
                lidgeld = 0;
            }
            MessageBox.Show("Lidgeld is " + lidgeld);
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