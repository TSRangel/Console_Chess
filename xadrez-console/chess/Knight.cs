using enums;
using board;

namespace chess
{
    class Knight : Piece
    {
        public Knight(Color color, Board board) : base(color, board) { }

        public override string ToString()
        {
            return "k";
        }

        public override bool[,] ValidMoviments()
        {
            bool[,] boardHouses = new bool[Board.Rows, Board.Columns];
            Position position = new Position(0, 0);

            position.DefineValues(Position.Row - 2, Position.Column - 1);
            boardHouses = VerifyValidMoviments(position, boardHouses);

            position.DefineValues(Position.Row - 1, Position.Column - 2);
            boardHouses = VerifyValidMoviments(position, boardHouses);

            position.DefineValues(Position.Row + 1, Position.Column - 2);
            boardHouses = VerifyValidMoviments(position, boardHouses);

            position.DefineValues(Position.Row + 2, Position.Column - 1);
            boardHouses = VerifyValidMoviments(position, boardHouses);

            position.DefineValues(Position.Row + 2, Position.Column + 1);
            boardHouses = VerifyValidMoviments(position, boardHouses);

            position.DefineValues(Position.Row + 1, Position.Column + 2);
            boardHouses = VerifyValidMoviments(position, boardHouses);

            position.DefineValues(Position.Row - 1, Position.Column + 2);
            boardHouses = VerifyValidMoviments(position, boardHouses);

            position.DefineValues(Position.Row - 2, Position.Column + 1);
            boardHouses = VerifyValidMoviments(position, boardHouses);

            return boardHouses;
        }

        private bool[,] VerifyValidMoviments(Position position, bool[,] boardHouses)
        {
            if (Board.ValidPosition(position) && CanIMove(position))
            {
                boardHouses[position.Row, position.Column] = true;
            }
            return boardHouses;
        }
    }
}
