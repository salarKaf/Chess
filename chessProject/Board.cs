using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ChessLogic;

namespace ChessLogic
{
    public class Board
    {
        private readonly Piece[,] pieces = new Piece[8, 8];
        private readonly Dictionary<Player, Position> pawnSkipPostitions = new Dictionary<Player, Position>
        {
            { Player.White , null },
            { Player.Black , null }
        };


        public Piece this [int row, int col]
        {
            get { return pieces[row, col]; }
            set { pieces[row, col] = value; }
        }

        public Piece this[Position pos]
        {
            get { return this[pos.Row,pos.Column]; }
            set { this[pos.Row , pos.Column] = value; }
        }
        public Position GetPawnSkipPosition(Player player)
        {
            return pawnSkipPostitions[player];
        }
        public void SetPawnSkipPosition(Player player,Position pos)
        {
            pawnSkipPostitions[player] = pos;
        }
        public static Board intial()
        {
            Board board = new Board();
            board.AddStartPieces();
            return board;

        }

        private void AddStartPieces()
        {

            this[0, 0] = new Rook(Player.Black);
            this[0, 1] = new Knight(Player.Black);
            this[0, 2] = new BiShop(Player.Black);
            this[0, 3] = new Queen(Player.Black);
            this[0, 4] = new King(Player.Black);
            this[0, 5] = new BiShop(Player.Black);
            this[0, 6] = new Knight(Player.Black);
            this[0, 7] = new Rook(Player.Black);


            this[7, 0] = new Rook(Player.White);
            this[7, 1] = new Knight(Player.White);
            this[7, 2] = new BiShop(Player.White);
            this[7, 3] = new Queen(Player.White);
            this[7, 4] = new King(Player.White);
            this[7, 5] = new BiShop(Player.White);
            this[7, 6] = new Knight(Player.White);
            this[7, 7] = new Rook(Player.White);

            for (int i = 0; i < 8; i++)
            {
                this[1, i] = new Pawn(Player.Black);
                this[6, i] = new Pawn(Player.White);
            }
        }

            public static bool IsInSide(Position pos)
            {

            return pos.Row >= 0 && pos.Row < 8 && pos.Column < 8 && pos.Column >= 0;

            }

        public bool IsEmpty(Position pos)
        {
            return this[pos] == null; 
        }

        public IEnumerable<Position> PiecePositions()
        {
            for(int r = 0; r<8; r++)
                for(int c=0; c<8; c++)
                {
                    Position pos = new Position(r, c);

                    if (!IsEmpty(pos))
                    {
                        yield return pos;
                    }
                }
        }

        public IEnumerable<Position> PiecePositionFor(Player player)
        {
            return PiecePositions().Where(pos => this[pos].Color == player);
        }

        public bool IsInCheck(Player player)
        {
            return PiecePositionFor(player.Opponent()).Any(pos =>
            {
                Piece piece = this[pos];
                return piece.CanCapturOpponentKing(pos, this);
            });
        }

        public Board Copy()
        {
            Board copy = new Board();

            foreach(Position pos in PiecePositions())
            {
                copy[pos] = this[pos].copy();
            }
            return copy;
        }

        public Counting CountPieces()
        {
            Counting counting = new Counting();
            foreach(Position pos in PiecePositions())
            {
                Piece piece = this[pos];
                counting.Increment(piece.Color, piece.Type);
            }
            return counting;
        }
        public bool InsufficientMaterial()
        {
            Counting counting = CountPieces();
            return IsKingVKing(counting) || IsKingBishopVKing(counting) ||
                IsKingKnightVKing(counting) || IsKingBishopVKingBishop(counting);
        }
        private static bool IsKingVKing(Counting counting)
        {
            return counting.TotalCount == 2;
        }
        private static bool IsKingBishopVKing(Counting counting)
        {
            return counting.TotalCount == 3 && (counting.White(TypePieces.BiShop)==1 || counting.Black(TypePieces.BiShop)==1);
        }
        private static bool IsKingKnightVKing(Counting counting)
        {
            return counting.TotalCount == 3 && (counting.White(TypePieces.knight) == 1 || counting.Black(TypePieces.knight) == 1);
        }
        private bool IsKingBishopVKingBishop(Counting counting)
        {
            if (counting.TotalCount !=4 )
            {
                return false;
            }
            if (counting.White(TypePieces.BiShop) !=1 || counting.Black(TypePieces.BiShop) !=1 )
            {
                return false;
            }

            Position wBishopPos = FindPiece(Player.White, TypePieces.BiShop);
            Position bBishopPos = FindPiece(Player.Black, TypePieces.BiShop);

            return wBishopPos.SquareColor() == bBishopPos.SquareColor();
        }
        private Position FindPiece(Player color , TypePieces type)
        {
            return PiecePositionFor(color).First(pos => this[pos].Type == type);
        }

        private bool IsUmovedKingAndRook(Position kingPos , Position rookPos)
        {
            if (IsEmpty(kingPos) || IsEmpty(rookPos))
            {
                return false;
            }

            Piece King = this[kingPos];
            Piece rook = this[rookPos];

            return King.Type == TypePieces.king && rook.Type == TypePieces.Rook &&
                !King.IsPlaying && !rook.IsPlaying;
        }

        public bool CatleRightKS(Player player)
        {
            return player switch
            {
                Player.White => IsUmovedKingAndRook(new Position(7, 4), new Position(7, 7)),
                Player.Black => IsUmovedKingAndRook(new Position(0, 4), new Position(0, 7)),
                _ => false
            };
        }
        public bool CastleRightQS(Player player) 
        {
            return player switch
            {
                Player.White => IsUmovedKingAndRook(new Position(7, 4), new Position(7, 0)),
                Player.Black => IsUmovedKingAndRook(new Position(0, 4), new Position(0, 0)),
                _ => false
            };
        }
        private bool HasPawnInPosition(Player player , Position[] pawnPositions , Position skipPos)
        {
            foreach (Position pos in pawnPositions.Where(IsInSide))
            {
                Piece piece = this[pos];
                if (piece == null || piece.Color != player || piece.Type != TypePieces.Pawn)
                {
                    continue;
                }
                EnPassant move = new EnPassant(pos, skipPos);
                if (move.IsLegal(this))
                {
                    return true;
                }
            }
            return false;
        }
        public bool CanCaptureEnPassant(Player player)
        {
            Position skipPos = GetPawnSkipPosition(player.Opponent());

            if (skipPos == null)
            {
                return false;
            }
            Position[] pawnPositions = player switch
            {
                Player.White => new Position[] { skipPos + Direction.SouthWest, skipPos + Direction.SouthEeast },
                Player.Black => new Position[] { skipPos + Direction.NorthWest, skipPos + Direction.NorthEast },
                _ => Array.Empty<Position>()
            };
            
            return HasPawnInPosition(player, pawnPositions, skipPos);
        }
    }
}
