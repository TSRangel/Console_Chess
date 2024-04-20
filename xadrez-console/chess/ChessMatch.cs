using board;
using xadrez_console.board.enums;

namespace chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color ActualPlayer { get; private set; }
        public bool IsFinished { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            ActualPlayer = Color.White;
            InsertPieces();
            IsFinished = false;
        }

        public void Moviment(Position inicial, Position destination)
        {
            Piece piece = Board.RemovePiece(inicial);
            piece.IncreasesMoviment();
            Piece removedPiece = Board.RemovePiece(destination);
            Board.InsertPiece(piece, destination);
        }

        public void Play(Position origin, Position destination)
        {
            Moviment(origin, destination);
            Turn++;
            ChangePlayer(); 
        }

        public void ValidateOriginPosition(Position position)
        {
            if (Board.Piece(position) == null)
            {
                throw new BoardException("There is no piece in this position.");
            }

            if (Board.Piece(position).Color != ActualPlayer)
            {
                throw new BoardException("This piece does not belong to actual player.");
            }

            if (!Board.Piece(position).IsThereAnyPossibleMoviment())
            {
                throw new BoardException("There is no possible moviments for this piece.");
            }
        }

        private void ChangePlayer()
        {
            if(ActualPlayer == Color.White)
            {
                ActualPlayer = Color.Black;
            } else
            {
                ActualPlayer = Color.White;
            }
        }

        public void InsertPieces()
        {
            Board.InsertPiece(new King(Color.Black, Board), new ChessPosition(1, 'd').ToPosition());
            Board.InsertPiece(new Rook(Color.Black, Board), new ChessPosition(1, 'c').ToPosition());
            Board.InsertPiece(new Rook(Color.Black, Board), new ChessPosition(2, 'c').ToPosition());
            Board.InsertPiece(new Rook(Color.Black, Board), new ChessPosition(2, 'd').ToPosition());
            Board.InsertPiece(new Rook(Color.Black, Board), new ChessPosition(2, 'e').ToPosition());
            Board.InsertPiece(new Rook(Color.Black, Board), new ChessPosition(1, 'e').ToPosition());

            Board.InsertPiece(new King(Color.White, Board), new ChessPosition(8, 'd').ToPosition());
            Board.InsertPiece(new Rook(Color.White, Board), new ChessPosition(8, 'c').ToPosition());
            Board.InsertPiece(new Rook(Color.White, Board), new ChessPosition(7, 'c').ToPosition());
            Board.InsertPiece(new Rook(Color.White, Board), new ChessPosition(7, 'd').ToPosition());
            Board.InsertPiece(new Rook(Color.White, Board), new ChessPosition(7, 'e').ToPosition());
            Board.InsertPiece(new Rook(Color.White, Board), new ChessPosition(8, 'e').ToPosition());
        }
    }
}
