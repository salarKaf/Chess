using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class King: Piece
    {
        public override TypePieces Type => TypePieces.king;

        public override Player Color { get; }
        public static readonly Direction[] dirs = new Direction[]
        {
            Direction.North,
            Direction.South,
            Direction.West,
            Direction.East,
            Direction.NorthWest,
            Direction.NorthEast,
            Direction.SouthEeast,
            Direction.SouthWest,

        };
        public King(Player color)
        {
            Color = color;
        }

        public override Piece copy()
        {
            King KingCopy = new King(Color);
            KingCopy.IsPlaying = IsPlaying;
            return KingCopy;

        }

        private IEnumerable<Position> MovePositions(Position from, Board board)
        {
            foreach (Direction dir in dirs)
            {
                Position to = from + dir;

                if (!Board.IsInSide(to))
                {
                    continue;
                }

                if (board.IsEmpty(to) || board[to].Color != Color)
                {
                    yield return to;
                }
            }
        }

        public override IEnumerable<Move> GetMoves(Position from , Board board)
        {
            foreach (Position to in MovePositions(from , board) )
            {
                yield return new NormalMoves(from, to);
            }

        }

        public override bool CanCapturOpponentKing(Position from, Board board)
        {
            return MovePositions(from, board).Any( to=>
            {
                Piece piece = board[to];
                return piece != null && piece.Type == TypePieces.king;
            });
        }






    }
}
