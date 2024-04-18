using board;
using chess;

namespace xadrez_console;

class Program
{
    static void Main()
    {
        try
        {
            ChessMatch chessMatch = new ChessMatch();

            while (!chessMatch.IsFinished)
            {
                Console.Clear();
                Screen.ChessboardPrinter(chessMatch.Board);

                Console.Write("Origin: ");
                Position origin = Screen.ChessPositionReader().ToPosition();
                Console.Write("Destination: ");
                Position destination = Screen.ChessPositionReader().ToPosition();

                chessMatch.Moviment(origin, destination);
            }


        }
        catch (BoardException e)
        {
            Console.WriteLine(e.Message);
        }


    }
}