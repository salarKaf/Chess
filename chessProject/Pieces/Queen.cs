using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Queen : Piece
    {
        public override TypePieces Type => TypePieces.Queen;

        public override Player Color { get; }

        public static readonly Direction[] dirs = new Direction[]
        {
            Direction.North,
            Direction.South,
            Direction.East,
            Direction.West,
            Direction.NorthWest,
            Direction.NorthEast,
            Direction.SouthWest,
            Direction.SouthEeast,
        };



        public Queen(Player color)
        {
            Color = color;
        }

        public override Piece copy()
        {
            Queen QueenCopy = new Queen(Color);
            QueenCopy.IsPlaying = IsPlaying;
            return QueenCopy;

        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionsInDir(from, board, dirs).Select(to => new NormalMoves(from, to));
        }
    }
}
