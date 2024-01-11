using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class BiShop:Piece
    {
        public override TypePieces Type => TypePieces.BiShop;

        public override Player Color { get; }
        private static readonly Direction[] dirs = new Direction[]
        {
            Direction.NorthWest,
            Direction.NorthEast,
            Direction.SouthEeast,
            Direction.SouthWest
        };


        public BiShop(Player color)
        {
            Color = color;
        }

        public override Piece copy()
        {
            BiShop BiShopCopy = new BiShop(Color);
            BiShopCopy.IsPlaying = IsPlaying;
            return BiShopCopy;

        }

        public override IEnumerable<Move> GetMoves(Position from , Board board)
        {
            return MovePositionsInDir(from, board , dirs).Select(to => new NormalMoves(from , to));
        }
    }
}
