using BattleShip.Commands;
using BattleShip.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace BattleShip.ViewModel
{
    class PlayerGridVM : INotifyPropertyChanged
    {
        private bool _gameStarted;

        public Player Me { get; }
        public Player Enemy { get; }


        public PlayerGridVM(Player me, Player enemy)
        {
            Me = me;
            Enemy = enemy;

            GenerateShipsCommand = new RelayCommand(o => RegenerateShips());
            StartGameCommand = new RelayCommand(o => StartGame());
            RestartGameCommand = new RelayCommand(o => RestartGame());
        }

        public bool GameStarted
        {
            get { return _gameStarted; }
            set
            {
                _gameStarted = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand GenerateShipsCommand { get; set; }
        public ICommand StartGameCommand { get; set; }
        public ICommand RestartGameCommand { get; set; }



        public void Fire(Square square)
        {
            if (square.Type == SquareType.Unknown)
            {
                Me.Fire(square.Row, square.Column, Enemy);
            }
        }

        private void RegenerateShips()
        {
            Me.MyGrid.ClearSquares();
            Me.PlaceShips();
        }

        private void StartGame()
        {
            GameStarted = true;
        }
        private void RestartGame()
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
