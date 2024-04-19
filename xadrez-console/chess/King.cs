using board;

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
            bool[,] boardHouses = new bool[Board.Columns, Board.Rows];

            Position position = new Position(0, 0);

            position.DefineValues(Position.Row - 1, Position.Column);
            if (Board.ValidPosition(position) && CanIMove(position))
            {
                boardHouses[position.Row, position.Column] = true;
            }
        }

        private bool CanIMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }
    }
}
