using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BattleShip.Model
{
    public enum SquareType
    {
        Unknown,
        Water,
        Damaged,
        Undamaged,
        Sunk
    }
    public class Square : INotifyPropertyChanged
    {

        private int _shipIndex;
        private SquareType _type;

        public int Row { get; private set; }
        public int Column { get; private set; }

        public int ShipIndex
        {
            get { return _shipIndex; }
            set
            {
                _shipIndex = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsEmpty => ShipIndex == -1 ? true : false;

        //public SquareType Type
        //{
        //    get { return (SquareType)GetValue(TypeProperty); }
        //    set { SetValue(TypeProperty, value); }
        //}
        //public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(SquareType), typeof(Square), null);

        public SquareType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                NotifyPropertyChanged();
            }
        }
        
        public Square(int row, int column)
        {
            Row = row;
            Column = column;
            _type = SquareType.Unknown;
            _shipIndex = -1;
        }

        public void Reset(SquareType type)
        {
            Type = type;
            ShipIndex = -1;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
