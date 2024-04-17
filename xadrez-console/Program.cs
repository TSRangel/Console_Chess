using board;
using chess;

namespace xadrez_console;

class Program
{
    static void Main()
    {
        try
        {
            Board board = new Board(8, 8);

            board.InsertPiece(new King(Color.Black, board), new Position(0, 0));
            board.InsertPiece(new Rook(Color.White, board), new Position(1, 3));
            board.InsertPiece(new King(Color.White, board), new Position(0, 7));

            Screen.BoardPrinter(board);
        }
        catch (BoardException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}