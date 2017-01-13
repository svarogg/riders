using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Riders.BL;
using Riders.Common.Model;

namespace Riders.PL.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LoadRaces();
        }

        private void LoadRaces()
        {
            var races = Bookie.GetRaces();
            if (!races.Any())
            {
                GenerateAndShowRaces();
            }
            else
            {
                ShowRaces();
            }
        }

        private void ShowRaces()
        {
            var races = Bookie.GetRaces();
            RaceList.ItemsSource = races;

            RacesGrid.Visibility = Visibility.Visible;
            BettingGrid.Visibility = Visibility.Hidden;
        }

        private void GenerateAndShowRaces()
        {
            DataGenerator.GenerateData();
            ShowRaces();
        }

        private void StartRacesButton_Click(object sender, RoutedEventArgs e)
        {
            var results = Bookie.CommenceRaces();
            ShowResults(results);
        }

        private void ShowResults(RaceResults results)
        {
            var sb = new StringBuilder();

            sb.Clear();
            sb.AppendLine("The games are over!");
            sb.AppendLine("And here are the results!");
            sb.AppendLine();

            foreach (var gameResult in results.RaceWinners)
            {
                var race = gameResult.Key;
                var winner = gameResult.Value;
                sb.AppendLine($"Race between {race.Rider1.Name} and {race.Rider2.Name}. Winner: {winner.Name}");
            }
            sb.AppendLine();
            sb.AppendLine("Prizes go to:");

            foreach (var prize in results.Prizes)
            {
                var bidderName = prize.Key;
                var amount = prize.Value;
                sb.AppendLine($"{bidderName} won ${amount}!");
            }

            MessageBox.Show(sb.ToString());
        }

        private void ShowBets(Race race)
        {
            BettingGrid.DataContext = new BetsViewModel(race);
            BettingGrid.Visibility = Visibility.Visible;
            RacesGrid.Visibility = Visibility.Hidden;
        }

        private void Properties_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowBets((Race) e.Parameter);
        }

        private void BackToRacesButton_Click(object sender, RoutedEventArgs e)
        {
            ShowRaces();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private enum RiderBet { First, Second}

        private void PlaceBet1Button_Click(object sender, RoutedEventArgs e)
        {
            PlaceBet(RiderBet.First);
        }

        private void PlaceBet2Button_Click(object sender, RoutedEventArgs e)
        {
            PlaceBet(RiderBet.Second);
        }

        private void PlaceBet(RiderBet whichRider)
        {
            var viewModel = (BetsViewModel) BettingGrid.DataContext;
            var rider = whichRider == RiderBet.First
                ? viewModel.Race.Rider1
                : viewModel.Race.Rider2;
            Bookie.PlaceBet(viewModel.Race, rider, viewModel.BidderName, viewModel.BidAmount);

            MessageBox.Show("Bet placed successfully!");
        }
    }
}
