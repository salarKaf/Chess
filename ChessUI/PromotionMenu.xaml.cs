using ChessLogic;
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

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for PromotionMenu.xaml
    /// </summary>
    public partial class PromotionMenu : UserControl
    {
        public event Action<TypePieces> PieceSelected;
        public PromotionMenu(Player player)
        {
            InitializeComponent();

            QueenImg.Source = Images.GetImage(player, TypePieces.Queen);
            BishopImg.Source = Images.GetImage(player, TypePieces.BiShop);
            RookImg.Source = Images.GetImage(player, TypePieces.Rook);
            KnightImg.Source = Images.GetImage(player, TypePieces.knight);
        }
        private void QueenImg_MouseDown(object sender,MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(TypePieces.Queen);
        }
        private void BishopImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(TypePieces.BiShop);
        }
        private void RookImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(TypePieces.Rook);
        }
        private void knightImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PieceSelected?.Invoke(TypePieces.knight);
        }
    }
}
