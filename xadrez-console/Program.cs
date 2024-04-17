using board;

namespace xadrez_console;

class Program
{
    static void Main()
    {
        Board board = new Board(8, 8);

        Screen.BoardPrinter(board);
    }
}