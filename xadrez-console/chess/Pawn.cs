using enums;
using board;

namespace chess
{
    class Pawn : Piece
    {
        public ChessMatch ChessMatch { get; set; }
        public Pawn(Color color, Board board, ChessMatch chessMatch) : base(color, board)
        {
            ChessMatch = chessMatch;
        }

        public override string ToString()
        {
            return "P";
        }

        public override bool[,] ValidMoviments()
        {
            bool[,] boardHouses = new bool[Board.Rows, Board.Columns];
            Position position = new Position(0, 0);

            if (Color == Color.White)
            {
                position.DefineValues(Position.Row - 1, Position.Column);
                if (Board.ValidPosition(position) && CanIMove(position))
                {
                    boardHouses[position.Row, position.Column] = true;
                    if (Moviments == 0 && Board.Piece(position) == null)
                    {
                        position.Row -= 1;
                    }
                    boardHouses[position.Row, position.Column] = true;
                }

                position.DefineValues(Position.Row - 1, Position.Column - 1);
                if (Board.ValidPosition(position) && IsThereAnyAdversary(position))
                {
                    boardHouses[position.Row, position.Column] = true;
                }

                position.DefineValues(Position.Row - 1, Position.Column + 1);
                if (Board.ValidPosition(position) && IsThereAnyAdversary(position))
                {
                    boardHouses[position.Row, position.Column] = true;
                }

                position.DefineValues(Position.Row, Position.Column - 1);
                if (Position.Row == 3 && Board.ValidPosition(position))
                {
                    if (IsThereAnyAdversary(position) && ChessMatch.EnPassantVulnerability == Board.Piece(position))
                    {
                        boardHouses[Position.Row - 1, Position.Column - 1] = true;
                    }
                }

                position.DefineValues(Position.Row, Position.Column + 1);
                if (Position.Row == 3 && Board.ValidPosition(position))
                {
                    if (IsThereAnyAdversary(position) && ChessMatch.EnPassantVulnerability == Board.Piece(position))
                    {
                        boardHouses[Position.Row - 1, Position.Column + 1] = true;
                    }
                }

            }
            else
            {
                position.DefineValues(Position.Row + 1, Position.Column);
                if (Board.ValidPosition(position) && CanIMove(position))
                {
                    boardHouses[position.Row, position.Column] = true;
                    if (Moviments == 0 && Board.Piece(position) == null)
                    {
                        position.Row += 1;
                    }
                    boardHouses[position.Row, position.Column] = true;
                }

                position.DefineValues(Position.Row + 1, Position.Column - 1);
                if (Board.ValidPosition(position) && IsThereAnyAdversary(position))
                {
                    boardHouses[position.Row, position.Column] = true;
                }

                position.DefineValues(Position.Row + 1, Position.Column + 1);
                if (Board.ValidPosition(position) && IsThereAnyAdversary(position))
                {
                    boardHouses[position.Row, position.Column] = true;
                }

                position.DefineValues(Position.Row, Position.Column - 1);
                if (Position.Row == 4 && Board.ValidPosition(position))
                {
                    if (IsThereAnyAdversary(position) && ChessMatch.EnPassantVulnerability == Board.Piece(position))
                    {
                        boardHouses[Position.Row + 1, Position.Column - 1] = true;
                    }
                }

                position.DefineValues(Position.Row, Position.Column + 1);
                if (Position.Row == 4 && Board.ValidPosition(position))
                {
                    if (IsThereAnyAdversary(position) && ChessMatch.EnPassantVulnerability == Board.Piece(position))
                    {
                        boardHouses[Position.Row + 1, Position.Column + 1] = true;
                    }
                }
            }

            return boardHouses;
        }

        private bool IsThereAnyAdversary(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece.Color != Color;
        }
    }
}
