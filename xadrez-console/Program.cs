using board;
using chess;

namespace xadrez_console;

class Program
{
    static void Main()
    {
        ChessPosition chessPosition = new ChessPosition('c', 8);
        Console.WriteLine(chessPosition);
        Console.WriteLine(chessPosition.ToPosition());
    }
}