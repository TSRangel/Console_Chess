﻿using board;
using System.Security.Cryptography.X509Certificates;

namespace xadrez_console
{
    class Screen
    {
        public static void BoardPrinter(Board board)
        {
            for (int i  = 0; i < board.Rows; i++)
            {
                for (int j = 0; j < board.Columns; j++)
                {
                    if(board.Piece(i, j) == null)
                    {
                        Console.Write("- ");
                    } else
                    {
                        Console.Write($"{board.Piece(i, j)} ");
                    }
                }
                //Console.WriteLine();
            }
        }
    }
}