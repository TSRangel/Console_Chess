using board;
using enums;

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
        public bool Check { get; private set; }
        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            ActualPlayer = Color.White;
            IsFinished = false;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            Check = false;
            InsertPieces();
        }

        public Piece Moviment(Position origin, Position destination)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncreasesMoviment();
            Piece removedPiece = Board.RemovePiece(destination);
            if (removedPiece != null)
            {
                Captured.Add(removedPiece);
            }
            Board.InsertPiece(piece, destination);

            if (piece is King && destination.Column == origin.Column + 2)
            {
                Position rookPosition = new Position(destination.Row, destination.Column + 1);
                Position newRookPosition = new Position(destination.Row, destination.Column - 1);
                Piece rook = Board.RemovePiece(rookPosition);
                rook.IncreasesMoviment();
                Board.InsertPiece(rook, newRookPosition);
            }

            if (piece is King && destination.Column == origin.Column - 2)
            {
                Position rookPosition = new Position(destination.Row, destination.Column - 2);
                Position newRookPositoin = new Position(destination.Row, destination.Column + 1);
                Piece rook = Board.RemovePiece(rookPosition);
                rook.IncreasesMoviment();
                Board.InsertPiece(rook, newRookPositoin);
            }

            return removedPiece;
        }

        public void UndoMoviment(Position origin, Position destination, Piece removedPiece)
        {
            Piece piece = Board.RemovePiece(destination);
            piece.DecreaseMoviment();
            if (removedPiece != null)
            {
                Board.InsertPiece(removedPiece, destination);
                Captured.Remove(removedPiece);
            }
            Board.InsertPiece(piece, origin);

            if (piece is King && destination.Column == origin.Column + 2)
            {
                Position originRook = new Position(origin.Row, origin.Column + 3);
                Position rookDestination = new Position(origin.Row, origin.Column + 1);
                Piece rook = Board.RemovePiece(rookDestination);
                rook.DecreaseMoviment();
                Board.InsertPiece(rook, originRook);
            }

            if (piece is King && destination.Column == origin.Column - 2)
            {
                Position originRook = new Position(origin.Row, origin.Column - 4);
                Position rookDestination = new Position(origin.Row, origin.Column - 1);
                Piece rook = Board.RemovePiece(rookDestination);
                rook.DecreaseMoviment();
                Board.InsertPiece(rook, originRook);
            }

        }

        public void Play(Position origin, Position destination)
        {
            Piece removedPiece = Moviment(origin, destination);

            if (InCheck(ActualPlayer))
            {
                UndoMoviment(origin, destination, removedPiece);
                throw new BoardException("It's not allowed to put yourself in check");
            }

            if (InCheck(Adversary(ActualPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if (CheckMate(Adversary(ActualPlayer)))
            {
                IsFinished = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }

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

        private Color Adversary(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece King(Color color)
        {
            foreach (Piece piece in PiecesInGame(color))
            {
                if (piece is King)
                {
                    return piece;
                }
            }
            return null;
        }

        public bool InCheck(Color color)
        {
            Piece king = King(color);
            if (king == null)
            {
                throw new BoardException("There is no king on the board.");
            }

            foreach (Piece piece in PiecesInGame(Adversary(color)))
            {
                bool[,] aux = piece.ValidMoviments();
                if (aux[king.Position.Row, king.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckMate(Color color)
        {
            if (!InCheck(color))
            {
                return false;
            }
            foreach (Piece piece in PiecesInGame(color))
            {
                bool[,] aux = piece.ValidMoviments();
                for (int i = 0; i < Board.Rows; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (aux[i, j])
                        {
                            Position origin = piece.Position;
                            Position destination = new Position(i, j);
                            Piece removedPiece = Moviment(origin, destination);
                            bool checkTest = InCheck(color);
                            UndoMoviment(origin, destination, removedPiece);
                            if (!checkTest)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
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
            InsertNewPiece(1, 'a', new Rook(Color.White, Board));
            InsertNewPiece(1, 'b', new Knight(Color.White, Board));
            InsertNewPiece(1, 'c', new Bishop(Color.White, Board));
            InsertNewPiece(1, 'd', new Queen(Color.White, Board));
            InsertNewPiece(1, 'e', new King(Color.White, Board, this));
            InsertNewPiece(1, 'f', new Bishop(Color.White, Board));
            InsertNewPiece(1, 'g', new Knight(Color.White, Board));
            InsertNewPiece(1, 'h', new Rook(Color.White, Board));
            InsertNewPiece(2, 'a', new Pawn(Color.White, Board));
            InsertNewPiece(2, 'b', new Pawn(Color.White, Board));
            InsertNewPiece(2, 'c', new Pawn(Color.White, Board));
            InsertNewPiece(2, 'd', new Pawn(Color.White, Board));
            InsertNewPiece(2, 'e', new Pawn(Color.White, Board));
            InsertNewPiece(2, 'f', new Pawn(Color.White, Board));
            InsertNewPiece(2, 'g', new Pawn(Color.White, Board));
            InsertNewPiece(2, 'h', new Pawn(Color.White, Board));

            InsertNewPiece(8, 'a', new Rook(Color.Black, Board));
            InsertNewPiece(8, 'b', new Knight(Color.Black, Board));
            InsertNewPiece(8, 'c', new Bishop(Color.Black, Board));
            InsertNewPiece(8, 'd', new Queen(Color.Black, Board));
            InsertNewPiece(8, 'e', new King(Color.Black, Board, this));
            InsertNewPiece(8, 'f', new Bishop(Color.Black, Board));
            InsertNewPiece(8, 'g', new Knight(Color.Black, Board));
            InsertNewPiece(8, 'h', new Rook(Color.Black, Board));
            InsertNewPiece(7, 'a', new Pawn(Color.Black, Board));
            InsertNewPiece(7, 'b', new Pawn(Color.Black, Board));
            InsertNewPiece(7, 'c', new Pawn(Color.Black, Board));
            InsertNewPiece(7, 'd', new Pawn(Color.Black, Board));
            InsertNewPiece(7, 'e', new Pawn(Color.Black, Board));
            InsertNewPiece(7, 'f', new Pawn(Color.Black, Board));
            InsertNewPiece(7, 'g', new Pawn(Color.Black, Board));
            InsertNewPiece(7, 'h', new Pawn(Color.Black, Board));

        }
    }
}
