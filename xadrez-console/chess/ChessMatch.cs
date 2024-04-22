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
        public HashSet<Piece> Pieces { get; private set; }
        public HashSet<Piece> Captured { get; private set; }
        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            ActualPlayer = Color.White;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            InsertPieces();
            IsFinished = false;
        }

        public void Moviment(Position inicial, Position destination)
        {
            Piece piece = Board.RemovePiece(inicial);
            piece.IncreasesMoviment();
            Piece removedPiece = Board.RemovePiece(destination);
            if (removedPiece != null)
            {
                Captured.Add(removedPiece);
            }
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

        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if (!Board.Piece(origin).CanIMoveTo(destination))
            {
                throw new BoardException("Invalid destination");
            }
        }

        private void ChangePlayer()
        {
            if (ActualPlayer == Color.White)
            {
                ActualPlayer = Color.Black;
            }
            else
            {
                ActualPlayer = Color.White;
            }
        }

        public void InsertNewPiece(int row, char column, Piece piece)
        {
            Board.InsertPiece(piece, new ChessPosition(row, column).ToPosition());
            Pieces.Add(piece);
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> capturedPieces = new HashSet<Piece>();
            foreach (Piece piece in Captured)
            {
                if (piece.Color == color)
                {
                    capturedPieces.Add(piece);
                }
            }
            return capturedPieces;
        }

        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> piecesInGame = new HashSet<Piece>();
            foreach (Piece piece in Pieces)
            {
                if (piece.Color == color)
                {
                    piecesInGame.Add(piece);
                }
            }
            piecesInGame.ExceptWith(CapturedPieces(color));
            return piecesInGame;
        }

        public void InsertPieces()
        {
            InsertNewPiece(1, 'd', new King(Color.Black, Board));
            InsertNewPiece(1, 'c', new Rook(Color.Black, Board));
            InsertNewPiece(2, 'c', new Rook(Color.Black, Board));
            InsertNewPiece(2, 'd', new Rook(Color.Black, Board));
            InsertNewPiece(2, 'e', new Rook(Color.Black, Board));
            InsertNewPiece(1, 'e', new Rook(Color.Black, Board));

            InsertNewPiece(8, 'd', new King(Color.White, Board));
            InsertNewPiece(8, 'c', new Rook(Color.White, Board));
            InsertNewPiece(7, 'c', new Rook(Color.White, Board));
            InsertNewPiece(7, 'd', new Rook(Color.White, Board));
            InsertNewPiece(7, 'e', new Rook(Color.White, Board));
            InsertNewPiece(8, 'e', new Rook(Color.White, Board));
        }
    }
}
