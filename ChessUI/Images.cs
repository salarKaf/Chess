using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ChessLogic;

namespace ChessUI
{
    public static class Images
    {
        private static readonly Dictionary<TypePieces, ImageSource> whiteSource = new()
        {
            { TypePieces.Pawn , LoadImage("Assets/PawnW.png")},
            { TypePieces.BiShop , LoadImage("Assets/BiShopW.png")},
            { TypePieces.knight , LoadImage("Assets/KnightW.png")},
            { TypePieces.Rook , LoadImage("Assets/RookW.png")},
            { TypePieces.Queen , LoadImage("Assets/QueenW.png")},
            { TypePieces.king , LoadImage("Assets/KingW.png")},

        };       
        
        
        private static readonly Dictionary<TypePieces, ImageSource> BlackSource = new()
        {
            { TypePieces.Pawn , LoadImage("Assets/PawnB.png")},
            { TypePieces.BiShop , LoadImage("Assets/BiShopB.png")},
            { TypePieces.knight , LoadImage("Assets/KnightB.png")},
            { TypePieces.Rook , LoadImage("Assets/RookB.png")},
            { TypePieces.Queen , LoadImage("Assets/QueenB.png")},
            { TypePieces.king , LoadImage("Assets/KingB.png")},

        };
        private static ImageSource LoadImage(string filePath)
        {
            return new BitmapImage(new Uri(filePath , UriKind.Relative));
        }

        public static ImageSource GetImage(Player color , TypePieces type)
        {
            return color switch
            {
                Player.White => whiteSource[type],
                Player.Black => BlackSource[type],
                _ => null

            };

        }

        public static ImageSource GetImage(Piece piece)
        {

            if(piece == null)
            { 
                return null;

            }
               
            return GetImage(piece.Color , piece.Type);

        }
    }
}
