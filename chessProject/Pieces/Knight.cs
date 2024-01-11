using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Knight: Piece
    {

        public override TypePieces Type => TypePieces.knight;

        public override Player Color { get; }
        public Knight(Player color)
        {
            Color = color;
        }

        public override Piece copy()
        {
            Knight knightCopy = new Knight(Color);
            knightCopy.IsPlaying = IsPlaying;
            return knightCopy;

        }

        private static IEnumerable<Position> PotentialToPositions(Position from)
        { 
            foreach (Direction vDir in new Direction[]{Direction.North, Direction.South })
            {
                foreach (Direction hDir in new Direction[] { Direction.West, Direction.East })
                {
                    yield return from  + vDir * 2 + hDir  ;
                    yield return  from + hDir * 2 + vDir ;
                }

            }
        }

        private IEnumerable<Position> MovePositions(Position from, Board board)
        {
            return PotentialToPositions(from).Where(pos => Board.IsInSide(pos)
                && (board.IsEmpty(pos) || board[pos].Color != Color));
        }

        public override IEnumerable<Move> GetMoves(Position from , Board board)
        {
            return MovePositions(from , board).Select(to => new NormalMoves(from, to)); 

        }


    }
}
