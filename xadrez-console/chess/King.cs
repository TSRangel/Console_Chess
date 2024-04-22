using board;
using xadrez_console.board.enums;

namespace chess
{
    class King : Piece
    {
        public King(Color color, Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "K";
        }

        public override bool[,] ValidMoviments()
        {
            bool[,] boardHouses = new bool[Board.Rows, Board.Columns];

            Position position = new Position(0, 0);

            position.DefineValues(Position.Row - 1, Position.Column);
            boardHouses = VerifyValidMoviments(position, boardHouses);

            position.DefineValues(Position.Row - 1, Position.Column + 1);
            boardHouses = VerifyValidMoviments(position, boardHouses);

            position.DefineValues(Position.Row, Position.Column + 1);
            boardHouses = VerifyValidMoviments(position, boardHouses);

            position.DefineValues(Position.Row + 1, Position.Column + 1);
            boardHouses = VerifyValidMoviments(position, boardHouses);

            position.DefineValues(Position.Row + 1, Position.Column);
            boardHouses = VerifyValidMoviments(position, boardHouses);

            position.DefineValues(Position.Row +1, Position.Column -1);
            boardHouses = VerifyValidMoviments(position, boardHouses);

            position.DefineValues(Position.Row, Position.Column - 1);
            boardHouses = VerifyValidMoviments(position, boardHouses);

            position.DefineValues(Position.Row - 1, Position.Column - 1);
            boardHouses = VerifyValidMoviments(position, boardHouses);

            return boardHouses;
        }

        private bool CanIMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }

        private bool[,] VerifyValidMoviments(Position position, bool[,] boardHouses)
        {
            if(Board.ValidPosition(position) && CanIMove(position))
            {
                boardHouses[position.Row, position.Column] = true;
            }
            return boardHouses;
        }
    }
}
