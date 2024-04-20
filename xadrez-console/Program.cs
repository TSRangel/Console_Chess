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
                Console.WriteLine();
                Console.WriteLine($"Turno: {chessMatch.Turn}");
                Console.WriteLine($"Aguardando jogada: {chessMatch.ActualPlayer}");

                Console.Write("Origin: ");
                Position origin = Screen.ChessPositionReader().ToPosition();
                chessMatch.ValidateOriginPosition(origin);

                Console.Clear();
                Screen.ChessboardPrinter(chessMatch.Board, chessMatch.Board.Piece(origin).ValidMoviments());

                Console.Write("Destination: ");
                Position destination = Screen.ChessPositionReader().ToPosition();

                chessMatch.Play(origin, destination);
            }


        }
        catch (BoardException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }


    }
}