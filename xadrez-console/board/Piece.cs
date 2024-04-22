using xadrez_console.board.enums;

namespace board
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int Moviments { get; set; }
        public Board Board { get; protected set; }

        public Piece(Color color, Board board)
        {
            Position = null;
            Color = color;
            Board = board;
            Moviments = 0;
        }

        public void IncreasesMoviment()
        {
            Moviments += 1;
        }

        public void DecreaseMoviment()
        {
            Moviments -= 1;
        }

        public bool IsThereAnyPossibleMoviment()
        {
            bool[,] boardHouses = ValidMoviments();
            for (int i = 0; i < Board.Rows; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (boardHouses[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanIMoveTo(Position position)
        {
            return ValidMoviments()[position.Row, position.Column];
        }

        abstract public bool[,] ValidMoviments();
    }
}
