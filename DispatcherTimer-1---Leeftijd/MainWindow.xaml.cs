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
using System.Windows.Threading;

namespace DispatcherTimer_1___Leeftijd
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Titel van MainWindow zetten op huidige datum bij inladen van MainWindow
            Title = $"Leeftijd in jaren, maanden en dagen voor {DateTime.Now.ToLongDateString()}";
            // Eerst al de huidige tijd direct tonen, want anders komt eerste update pas na 1 seconde
            Timer_Tick(null, null);

            // Timer aanmaken en starten
            // Om de seconde de Timer_Tick event procedure uitvoeren
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        // De huidige tijd aanpassen
        private void Timer_Tick(object sender, EventArgs e)
        {
            dateLabel.Content = $"{DateTime.Now.ToLongDateString()}  {DateTime.Now.ToLongTimeString()}";
        }


        // Logica voor de berekening
        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            // Test correcte datum invoer.
            bool isCorrectDate = DateTime.TryParse(birthDateTextBox.Text, out DateTime birthDate);
            if (!isCorrectDate)
            {
                // Als de datum niet correct is
                MessageBox.Show("Geef correcte datum: \r\nFormaat: " + "(dd-mm-yyyy) of (dd/mm/yyyy) of (dd.mm.yyyy)",
                    "Geboortedatum", MessageBoxButton.OK, MessageBoxImage.Error);
                birthDateTextBox.SelectAll(); // selecteer ingevulde datum tekst
                birthDateTextBox.Focus(); // zorg dat je in deze textbox tekst kan ingeven door hierop focus te leggen
            }
            else
            {
                // Als de datum wel correct is
                DateTime currentDate = DateTime.Today;
                int numberOfDays;
                int numberOfMonths;
                int numberOfYears;

                // ---jaren en maanden ---
                numberOfYears = currentDate.Year - birthDate.Year;

                if ((currentDate.Month < birthDate.Month) ||
                   (currentDate.Month == birthDate.Month && currentDate.Day < birthDate.Day))
                {
                    numberOfYears--;
                    numberOfMonths = (numberOfYears * 12) + currentDate.Month + 1;
                }
                else
                {
                    numberOfMonths = (numberOfYears * 12) - birthDate.Month + currentDate.Month;
                }
                // ---dagen ---
                // Substract kan sec, minuten, uren en dagen halen uit DateTime
                numberOfDays = currentDate.Subtract(birthDate).Days;

                // Toon resultaat:
                yearsTextBox.Text = numberOfYears.ToString();
                monthsTextBox.Text = numberOfMonths.ToString();
                daysTextBox.Text = numberOfDays.ToString();
            }
        }
    }
}
