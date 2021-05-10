using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;


namespace BattleShip.Model
{
    public class Player : INotifyPropertyChanged
    {
        private bool _won;
        private bool _lose;
        private bool _isShooting;
        private bool _isPlayerReady;


        public Player(bool isShooting)
        {
            _won = false;
            _lose = false;
            _isShooting = isShooting;

            MyShips = Ship.GetShips();
            EnemyShips = Ship.GetShips();

            MyGrid = new Grid(GridType.My);
            EnemyGrid = new Grid(GridType.Enemy);
        }


        public bool Lose
        {
            get { return _lose; }
            private set
            {
                _lose = value;
                NotifyPropertyChanged();
            }
        }
        public bool Won
        {
            get { return _won; }
            private set
            {
                _won = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsShooting
        {
            get { return _isShooting; }
            private set
            {
                _isShooting = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsPlayerReady
        {
            get { return _isPlayerReady; }
            set
            {
                if (_isPlayerReady != value)
                {
                    _isPlayerReady = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public List<Ship> MyShips { get; set; }
        public List<Ship> EnemyShips { get; set; }

        public Grid MyGrid { get; private set; }
        public Grid EnemyGrid { get; private set; }


        public void PlaceShips()
        {
            int shipIndex = 0;
            Random random = new Random();

            foreach (Ship ship in MyShips)
            {
                bool shipPlaced = false;

                while (!shipPlaced)
                {
                    double randomValue = random.NextDouble();

                    int row = random.Next(0, Grid.SIZE);
                    int column = random.Next(0, Grid.SIZE);

                    if (randomValue < 0.5 && MyGrid.ShipCanBePlacedHorizontally(row, column, ship.Length))
                    {
                        MyGrid.PlaceShipHorizontal(row, column, shipIndex, ship.Length);
                    }
                    else if (randomValue >= 0.5 && MyGrid.ShipCanBePlacedVertically(row, column, ship.Length))
                    {
                        MyGrid.PlaceShipVertical(row, column, shipIndex, ship.Length);
                    }
                    else
                    {
                        continue;
                    }

                    shipPlaced = true;
                    IsPlayerReady = true;

                    shipIndex++;
                }
            }
        }

        public void Fire(int row, int column, Player enemy)
        {
            int shipIndex;

            SquareType firedSquareType = enemy.FiredAt(row, column, out shipIndex);
            EnemyGrid.Squares[row][column].ShipIndex = shipIndex;
            EnemyGrid.Squares[row][column].Type = firedSquareType;

            //hit
            if (shipIndex > -1)
            {
                //sunk
                if (EnemyShips[shipIndex].Hit())
                {
                    SetSquaresToSunk(shipIndex, EnemyGrid);

                    //no remaining ships
                    if (EnemyShips.Where(x => !x.IsSunk).Count() == 0)
                    {
                        Won = true;
                        enemy.Lose = true;

                        RevealShipsToEnemy(enemy);
                    }
                }
            }
            else
            {
                //switch player
                IsShooting = false;
                enemy.IsShooting = true;
            }
        }

        public SquareType FiredAt(int row, int column, out int shipIndex)
        {
            Square firedSquare = MyGrid.Squares[row][column];
            shipIndex = -1;

            if (firedSquare.Type == SquareType.Unknown)
            {
                firedSquare.Type = SquareType.Water;
            }

            if (firedSquare.Type == SquareType.Undamaged)
            {
                firedSquare.Type = SquareType.Damaged;
                shipIndex = firedSquare.ShipIndex;
                Ship firedShip = MyShips[firedSquare.ShipIndex];

                if (firedShip.Hit())
                {
                    firedSquare.Type = SquareType.Sunk;
                    SetSquaresToSunk(firedSquare.ShipIndex, MyGrid);
                }
            }

            return firedSquare.Type;
        }

        private void SetSquaresToSunk(int shipIndex, Grid grid)
        {
            IEnumerable<Square> squares = grid.Squares.SelectMany(x => x).Where(x => x.ShipIndex == shipIndex);

            for (int i = Math.Max(0, squares.First().Row - 1); i <= Math.Min(9, squares.Last().Row + 1); i++)
            {
                for (int j = Math.Max(0, squares.First().Column - 1); j <= Math.Min(9, squares.Last().Column + 1); j++)
                {
                    if (grid.Squares[i][j].ShipIndex >= 0)
                    {
                        grid.Squares[i][j].Type = SquareType.Sunk;
                    }
                    else
                    {
                        grid.Squares[i][j].Type = SquareType.Water;
                    }
                }
            }
        }

        private void RevealShipsToEnemy(Player enemy)
        {
            IEnumerable<Square> myShips = MyGrid.Squares.SelectMany(x => x).Where(x => x.ShipIndex > -1);

            foreach (var square in myShips)
            {
                enemy.EnemyGrid.Squares[square.Row][square.Column].Type = square.Type;
            }

            NotifyPropertyChanged("EnemyGrid");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
