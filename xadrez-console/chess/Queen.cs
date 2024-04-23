using enums;
using board;
using System.Data;

namespace chess
{
    class Queen : Piece
    {
        public Queen(Color color, Board board) : base(color, board) { }

        public override string ToString()
        {
            return "Q";
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
                position.Row += 1;
                position.Column += 1;
            }
            
            position.DefineValues(Position.Row, Position.Column + 1);
            while(Board.ValidPosition(position) && CanIMove(position))
            {
                boardHouses[position.Row, position.Column] = true;
                if(Board.Piece(position) != null)
                {
                    break;
                }
                position.Column += 1;
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

            position.DefineValues(Position.Row - 1, Position.Column);
            while(Board.ValidPosition(position) && CanIMove(position))
            {
                boardHouses[position.Row, position.Column] = true;
                if(Board.Piece(position) != null)
                {
                    break;
                }
                position.Row -= 1;
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
            
            position.DefineValues(Position.Row, Position.Column - 1);
            while(Board.ValidPosition(position) && CanIMove(position))
            {
                boardHouses[position.Row, position.Column] = true;
                if(Board.Piece(position) != null)
                {
                    break;
                }
                position.Column -= 1;
            }
            
            position.DefineValues(Position.Row + 1, position.Column - 1);
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

            position.DefineValues(Position.Row + 1, Position.Column - 1);
            while (Board.ValidPosition(position) && CanIMove(position))
            {
                boardHouses[position.Row, position.Column] = true;
                if (Board.Piece(position) != null)
                {
                    break;
                }
                position.Row += 1;
                position.Column -= 1;
            }

            position.DefineValues(Position.Row + 1, Position.Column); 
            while(Board.ValidPosition(position) && CanIMove(position))
            {
                boardHouses[position.Row, position.Column] = true;
                if(Board.Piece(position) != null)
                {
                    break;
                }
                position.Row += 1;
            }

            return boardHouses;
        }
    }
}
