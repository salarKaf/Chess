using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class StateString
    {
        private readonly StringBuilder sb= new StringBuilder();
        public StateString(Player currentPlayer , Board board) 
        {
            AddPiecePlacement(board);
            sb.Append(' ');
            AddCurrentPlayer(currentPlayer);
            sb.Append(' ');
            AddCastlingRights(board);
            sb.Append(' ');
            AddEnPassant(board, currentPlayer);
        }

        public override string ToString()
        {
            return sb.ToString();
        }
        private static char PieceChar(Piece piece)
        {
            char c = piece.Type switch
            {
                TypePieces.Pawn => 'p',
                TypePieces.knight => 'n',
                TypePieces.Rook => 'r',
                TypePieces.BiShop => 'b',
                TypePieces.Queen => 'q',
                TypePieces.king => 'k',
                _ =>' '
            };
            if (piece.Color == Player.White)
            {
                return char.ToUpper(c);
            }
            return c;
        }
        private void AddRowData(Board board, int row)
        {
            int empty = 0;
            for (int c =0;c<8;c++)
            {
                if (board[row,c] == null)
                {
                    empty++;
                    continue;
                }
                if (empty>0)
                {
                    sb.Append(empty);
                    empty = 0;
                }
                sb.Append(PieceChar(board[row, c]));
            }
            if (empty>0)
            {
                sb.Append(empty);
            }

        }
        public void AddPiecePlacement(Board board)
        {
            for (int r=0; r < 8; r++)
            {
                if (r!=0)
                {
                    sb.Append('/');
                }
                AddRowData(board,r);
            }
        }

        public void AddCurrentPlayer(Player curretPlayer)
        {
            if (curretPlayer == Player.White)
            {
                sb.Append('w');
            }
            else
            {
                sb.Append('b');
            }
        }

        public void AddCastlingRights(Board board)
        {
            bool castleWKS = board.CatleRightKS(Player.White);
            bool castleWQS = board.CastleRightQS(Player.White);
            bool castleBKS = board.CatleRightKS(Player.Black);
            bool castleBQS = board.CastleRightQS(Player.Black);

            if (!(castleWKS || castleWQS || castleBKS || castleBQS))
            {
                sb.Append('-');
                return;
            }
            if (castleWKS)
            {
                sb.Append('K');
            }
            if (castleWQS)
            {
                sb.Append('Q');
            }
            if (castleBKS)
            {
                sb.Append('k');
            }
            if (castleBQS)
            {
                sb.Append('q');
            }
        }

        private void AddEnPassant(Board board,Player currentPlayer)
        {
            if (! board.CanCaptureEnPassant(currentPlayer))
            {
                sb.Append('-');
                return;
            }
            Position pos = board.GetPawnSkipPosition(currentPlayer.Opponent());
            char file = (char)('a' + pos.Column);
            int rank = 8 - pos.Row;
            sb.Append(file);
            sb.Append(rank);
        }

    }
}
