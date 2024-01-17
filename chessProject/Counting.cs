using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Counting
    {
        private readonly Dictionary<TypePieces, int> whiteCount = new();
        private readonly Dictionary<TypePieces, int> blackCount = new();

        public int TotalCount
        {
            get; private set;
        }
        public Counting()
        {
            foreach (TypePieces type in Enum.GetValues(typeof(TypePieces)))
            {
                whiteCount[type] = 0;
                blackCount[type] = 0;
            }
        }
        public void Increment(Player color , TypePieces type)
        {
            if (color == Player.White)
            {
                whiteCount[type]++;
            }
            else if (color == Player.Black)
            {
                blackCount[type]++;
            }
            TotalCount++;
        }
        public int White(TypePieces type)
        {
            return whiteCount[type];
        }
        public int Black(TypePieces type)
        {
            return blackCount[type];
        }
    }
}
