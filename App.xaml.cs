using BattleShip.Model;
using System;
using System.Windows;

namespace BattleShip
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            bool isFirst = false;
            Random random = new Random();
            double randomValue = random.NextDouble();
            if (randomValue < 0.5)
            {
                isFirst = true;
            }

            Player player1 = new Player(isFirst);
            Player player2 = new Player(!isFirst);

            MainWindow Player1Window = new MainWindow(player1, player2);
            Player1Window.Title = "Player 1";
            Player1Window.Show();

            MainWindow Player2Window = new MainWindow(player2, player1);
            Player2Window.Title = "Player 2";
            Player2Window.Show();
        }
    }
}
