using board;

namespace chess
{
    class ChessPosition
    {
        public int Row { get; set; }
        public char Column { get; set; }
        public ChessPosition(char column, int row)
        {
            Column = column;
            Row = row;
        }

        public Position ToPosition()
        {
            return new Position(Row -1, Column - 'a');
        }

        public override string ToString()
        {
            return $"{Row}{Column}";
        }
    }
}
