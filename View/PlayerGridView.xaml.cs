using BattleShip.Model;
using BattleShip.ViewModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BattleShip.View
{
    /// <summary>
    /// Interaction logic for PlayerGridView.xaml
    /// </summary>
    public partial class PlayerGridView : UserControl
    {
        public PlayerGridView()
        {
            InitializeComponent();
        }

        private void Square_Clicked(object sender, MouseButtonEventArgs e)
        {
            PlayerGridVM viewModel = this.DataContext as PlayerGridVM;
            ListViewItem cell = sender as ListViewItem;
            Square square = cell.Content as Square;

            if (square == null)
                return;

            viewModel.Fire(square);
        }
    }
}
