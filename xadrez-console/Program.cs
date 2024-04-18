using board;
using chess;

namespace xadrez_console;

class Program
{
    static void Main()
    {
        Board chessboard = new Board(8, 8);
        King whitePiece = new King(Color.White, chessboard);
        King yellowPiece = new King(Color.Yellow, chessboard);

        chessboard.InsertPiece(whitePiece, new Position(0, 0));
        chessboard.InsertPiece(yellowPiece, new Position(5, 5));

        Screen.ChessboardPrinter(chessboard);
    }
}