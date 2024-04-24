using board;
using enums;

namespace chess
{
    class King : Piece
    {
        public ChessMatch ChessMatch { get; set; }
        public King(Color color, Board board, ChessMatch chessMatch) : base(color, board)
        {
            ChessMatch = chessMatch;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool ValidateCastlingViability(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece is Rook && piece.Color == Color && piece.Moviments == 0;
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

            position.DefineValues(Position.Row + 1, Position.Column - 1);
            boardHouses = VerifyValidMoviments(position, boardHouses);

            position.DefineValues(Position.Row, Position.Column - 1);
            boardHouses = VerifyValidMoviments(position, boardHouses);

            position.DefineValues(Position.Row - 1, Position.Column - 1);
            boardHouses = VerifyValidMoviments(position, boardHouses);

            if (Moviments == 0 && !ChessMatch.Check)
            {
                position.DefineValues(Position.Row, Position.Column + 3);
                if (ValidateCastlingViability(position))
                {
                    Position newKingsPosition = new Position(Position.Row, Position.Column + 2);
                    Position newRookPosiiton = new Position(Position.Row, Position.Column + 1);
                    if(Board.Piece(newKingsPosition) == null && Board.Piece(newRookPosiiton) == null)
                    {
                        boardHouses[newKingsPosition.Row, newKingsPosition.Column] = true;
                    } 
                }

                position.DefineValues(Position.Row, Position.Column - 4);
                if (ValidateCastlingViability(position))
                {
                    Position newKingsPosition = new Position(Position.Row, Position.Column - 2);
                    Position newRookPosiiton = new Position(Position.Row, Position.Column - 1);
                    Position newAvailablePosition = new Position(Position.Row, Position.Column -3);
                    if (Board.Piece(newKingsPosition) == null && Board.Piece(newRookPosiiton) == null && Board.Piece(newAvailablePosition) == null)
                    {
                        boardHouses[newKingsPosition.Row, newKingsPosition.Column] = true;
                    }
                }
            }


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
