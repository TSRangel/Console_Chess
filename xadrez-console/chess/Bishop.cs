using board;
using enums;

namespace chess
{
    class Bishop : Piece
    {
        public Bishop(Color color, Board board) : base(color, board) {}

        public override string ToString()
        {
            return "B";
        }

        public override bool[,] ValidMoviments()
        {
            bool[,] boardHouses = new bool[Board.Rows, Board.Columns];
            Position position = new Position(0, 0);

            position.DefineValues(Position.Row + 1, Position.Column + 1);
            while(Board.ValidPosition(position) && CanIMove(position))
            {
                boardHouses[position.Row, position.Column] = true;
                if(Board.Piece(position) != null)
                {
                    break;
                }
                position.Row = position.Row + 1;
                position.Column = position.Column + 1;
            }

            position.DefineValues(Position.Row - 1, Position.Column + 1);
            while(Board.ValidPosition(position) && CanIMove(position))
            {
                boardHouses[position.Row, position.Column] = true;
                if(Board.Piece(position) != null)
                {
                    break;
                }
                position.Row -= 1;
                position.Column += 1;
            }

            position.DefineValues(Position.Row - 1, Position.Column - 1);
            while(Board.ValidPosition(position) && CanIMove(position))
            {
                boardHouses[position.Row, position.Column] = true;
                if(Board.Piece(position) != null)
                {
                    break;
                }
                position.Row -= 1;
                position.Column -= 1;
            }

            position.DefineValues(Position.Row + 1, Position.Column - 1);
            while(Board.ValidPosition(position) && CanIMove(position))
            {
                boardHouses[position.Row, position.Column] = true;
                if(Board.Piece(position) != null)
                {
                    break;
                }
                position.Row += 1;
                position.Column -= 1;
            }
                
            return boardHouses;
        }
    }
}
