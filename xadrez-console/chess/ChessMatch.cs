using board;

namespace chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        private int turn;
        private Color actualPlayer;
        public bool IsFinished { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            turn = 1;
            actualPlayer = Color.White;
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

        public void InsertPieces()
        {
            Board.InsertPiece(new King(Color.Black, Board), new ChessPosition(1, 'c').ToPosition());
            Board.InsertPiece(new Rook(Color.Black, Board), new ChessPosition(2, 'c').ToPosition());
            Board.InsertPiece(new Rook(Color.Black, Board), new ChessPosition(3, 'c').ToPosition());
            Board.InsertPiece(new Rook(Color.Black, Board), new ChessPosition(4, 'c').ToPosition());
            Board.InsertPiece(new Rook(Color.Black, Board), new ChessPosition(5, 'c').ToPosition());

            Board.InsertPiece(new King(Color.White, Board), new ChessPosition(1, 'e').ToPosition());
            Board.InsertPiece(new Rook(Color.White, Board), new ChessPosition(2, 'e').ToPosition());
            Board.InsertPiece(new Rook(Color.White, Board), new ChessPosition(3, 'e').ToPosition());
            Board.InsertPiece(new Rook(Color.White, Board), new ChessPosition(4, 'e').ToPosition());
            Board.InsertPiece(new Rook(Color.White, Board), new ChessPosition(5, 'e').ToPosition());
        }
    }
}
