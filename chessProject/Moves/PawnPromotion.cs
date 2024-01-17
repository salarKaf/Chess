using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic.Moves
{
    public class PawnPromotion : Move
    {
        public override MoveType Type => MoveType.PawnPromotion;
        public override Position FromPosition { get; }
        public override Position ToPosition { get; }
        public readonly TypePieces newType;

        public PawnPromotion(Position from,Position to , TypePieces newType)
        {
            FromPosition = from;
            ToPosition = to;
            this.newType = newType;
        }
        private Piece createPromotionPiece(Player color)
        {
            return newType switch
            {
                TypePieces.knight => new Knight(color),
                TypePieces.BiShop => new BiShop(color),
                TypePieces.Rook => new Rook(color),
                _=> new Queen(color)
            };
        }
        public override bool Execute(Board board)
        {
            Piece pawn = board[FromPosition];
            board[FromPosition] = null;

            Piece promotionPiece = createPromotionPiece(pawn.Color);
            promotionPiece.IsPlaying = true;
            board[ToPosition] = promotionPiece;

            return true;
        }
    }
}
