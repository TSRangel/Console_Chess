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

        public Piece Moviment(Position inicial, Position destination)
        {
            Piece piece = Board.RemovePiece(inicial);
            piece.IncreasesMoviment();
            Piece removedPiece = Board.RemovePiece(destination);
            if (removedPiece != null)
            {
                Captured.Add(removedPiece);
            }
            Board.InsertPiece(piece, destination);
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
            InsertNewPiece(1, 'c', new Rook(Color.White, Board));
            InsertNewPiece(1, 'd', new King(Color.White, Board));
            InsertNewPiece(4, 'd', new Queen(Color.White, Board));
            InsertNewPiece(2, 'h', new Pawn(Color.White, Board));
            InsertNewPiece(2, 'g', new Pawn(Color.White, Board));

            InsertNewPiece(8, 'a', new King(Color.Black, Board));
            InsertNewPiece(8, 'b', new Bishop(Color.Black, Board));
            InsertNewPiece(6, 'b', new Bishop(Color.Black, Board));
            InsertNewPiece(3, 'f', new Pawn(Color.Black, Board));
        }
    }
}
