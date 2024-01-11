using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public abstract class Piece
    {
        public abstract TypePieces Type { get; }
        public abstract Player Color { get; }
        public bool IsPlaying { get;set ; } = false;
        public abstract Piece copy();
        public abstract IEnumerable<Move> GetMoves(Position from , Board board);

        
        protected IEnumerable<Position> MovePositionsInDir(Position from, Board board, Direction[] dirs)
        {
            foreach(Direction dir in dirs)
            {
                for(Position pos= from+ dir ; Board.IsInSide(pos); pos =  pos+dir )
                {
                    if(board.IsEmpty(pos))
                    {
                        yield return pos;
                        continue;
                    }
                    Piece piece = board[pos];
                    if (piece.Color!=Color)
                    {
                        yield return pos;

                    }
                }

            }

        }
        public  virtual bool CanCapturOpponentKing(Position from , Board board)
        {
            return GetMoves(from , board).Any(move =>
            {
                Piece piece= board[move.ToPosition];
                return piece !=null && piece.Type==TypePieces.king;

            });
        }



    }
}
