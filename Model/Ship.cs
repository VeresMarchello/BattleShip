using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Model
{
    public enum ShipType
    {
        PatrolBoat,
        Submarine,
        Destroyer,
        Battleship,
        Carrier
    }

    public class Ship : INotifyPropertyChanged
    {
        private int _health;
        private bool _isSunk;
        private static readonly Dictionary<ShipType, int> shipLengths = new Dictionary<ShipType, int>()
        {
            { ShipType.PatrolBoat, 2 },
            { ShipType.Submarine, 3 },
            { ShipType.Destroyer, 3 },
            { ShipType.Battleship, 4 },
            { ShipType.Carrier, 5 },
        };

        public static List<Ship> GetShips()
        {
            List<Ship> Ships = new List<Ship>();

            foreach (ShipType type in Enum.GetValues(typeof(ShipType)))
            {
                Ships.Add(new Ship(type));
            }

            return Ships;
        }

        private Ship(ShipType type)
        {
            _health = shipLengths[type];
            _isSunk = false;
            Type = type;
        }

        public bool IsSunk
        {
            get { return _isSunk; }
            private set
            {
                _isSunk = value;
                NotifyPropertyChanged();
            }
        }
        public int Length => shipLengths[Type];
        public ShipType Type { get; }

        public bool Hit()
        {
            if (--_health == 0)
            {
                IsSunk = true;
            }

            return IsSunk;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
