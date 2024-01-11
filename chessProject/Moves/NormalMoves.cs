﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class NormalMoves : Move
    {
        public override MoveType Type => MoveType.Normal;

        public override Position FromPosition { get; }

        public override Position ToPosition { get; }

        public NormalMoves(Position from, Position to)
        {
            FromPosition = from;
            ToPosition = to;

        }


        public override void Execute(Board board)
        {
            Piece piece = board[FromPosition];
            board[ToPosition] = piece;
            board[FromPosition] = null;
            piece.IsPlaying = true;

        }
    }
}