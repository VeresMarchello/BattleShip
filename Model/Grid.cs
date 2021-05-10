using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BattleShip.Model
{
    public enum GridType
    {
        My,
        Enemy
    }

    public class Grid : INotifyPropertyChanged
    {
        public const int SIZE = 10;
        private GridType _type;
        private List<List<Square>> _squares;


        public Grid(GridType type)
        {
            _type = type;

            CreateSquares();
            //ClearSquares();
        }


        public List<List<Square>> Squares
        { 
            get { return _squares; } 
            set 
            { 
                _squares = value; 
                NotifyPropertyChanged(); 
            } 
        }


        private void CreateSquares()
        {
            _squares = new List<List<Square>>();

            for (int i = 0; i < SIZE; i++)
            {
                _squares.Add(new List<Square>());

                for (int j = 0; j < SIZE; j++)
                {
                    _squares[i].Add(new Square(i, j));
                }
            }
        }
        public void ClearSquares()
        {
            foreach (var row in Squares)
            {
                foreach (var square in row)
                {
                    square.Reset(SquareType.Unknown);
                }
            }
        }

        public void PlaceShipHorizontal(int row, int column, int shipIndex, int shipLength)
        {
            for (int i = column; i < column + shipLength; i++)
            {
                Squares[row][i].ShipIndex = shipIndex;
                Squares[row][i].Type = SquareType.Undamaged;
            }
        }
        public void PlaceShipVertical(int row, int column, int shipIndex, int shipLength)
        {
            for (int i = row; i < row + shipLength; i++)
            {
                Squares[i][column].ShipIndex = shipIndex;
                Squares[i][column].Type = SquareType.Undamaged;
            }
        }

        public bool ShipCanBePlacedHorizontally(int row, int column, int shipLength)
        {
            if (column + shipLength >= SIZE)
            {
                return false;
            }

            for (int i = Math.Max(0, column - 1); i < Math.Min(column + shipLength + 1, SIZE - 1); i++)
            {
                int upperRow = Math.Max(0, row - 1);
                int lowerRow = Math.Min(SIZE - 1, row + 1);

                if (!Squares[row][i].IsEmpty || !Squares[upperRow][i].IsEmpty || !Squares[lowerRow][i].IsEmpty)
                {
                    return false;
                }
            }

            return true;
        }
        public bool ShipCanBePlacedVertically(int row, int column, int shipLength)
        {
            if (row + shipLength >= SIZE)
            {
                return false;
            }

            for (int i = Math.Max(0, row - 1); i < Math.Min(row + shipLength + 1, SIZE - 1); i++)
            {
                int leftColumn = Math.Max(0, column - 1);
                int rightColumn = Math.Min(SIZE - 1, column + 1);

                if (!Squares[i][column].IsEmpty || !Squares[i][leftColumn].IsEmpty || !Squares[i][rightColumn].IsEmpty)
                {
                    return false;
                }
            }

            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
