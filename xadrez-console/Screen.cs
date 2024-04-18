﻿using board;
using chess;

namespace xadrez_console
{
    class Screen
    {
        public static void ChessboardPrinter(Board board)
        {
            for (int i  = 0; i < board.Rows; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if(board.Piece(i, j) == null)
                    {
                        Console.Write("- ");
                    } else
                    {
                        PiecePrinter(board.Piece(i, j));
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PiecePrinter(Piece piece)
        {
            if(piece.Color == Color.White)
            {
                Console.Write($"{piece} ");
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{piece} ");
                Console.ForegroundColor = aux;
            }
        }

        public static ChessPosition ChessPositionReader()
        {
            string position = Console.ReadLine();
            int row = int.Parse($"{position[0]}");
            char column = position[1];
            return new ChessPosition(row, column);
        }
    }
}
