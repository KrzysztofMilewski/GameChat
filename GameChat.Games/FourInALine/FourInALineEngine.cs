using System;
using System.Collections.Generic;
using System.Linq;

namespace GameChat.Games.FourInALine
{

    public class FourInALineEngine
    {
        private readonly int _player1;
        private readonly int _player2;
        private GameBoard _board;

        private void OnGameEnded()
        {
            GameEnded = true;
        }

        private void CoinFlip()
        {
            var random = new Random();
            var coinflip = random.NextDouble();

            if (coinflip <= 0.5d)
                PlayerWhoHasTurn = _player1;
            else
                PlayerWhoHasTurn = _player2;
        }

        private FieldState FromPlayerId(int playerId)
        {
            if (playerId == _player1)
                return FieldState.TakenByPlayer1;
            else
                return FieldState.TakenByPlayer2;
        }

        private void ChangePlayer()
        {
            if (PlayerWhoHasTurn == _player1)
                PlayerWhoHasTurn = _player2;
            else
                PlayerWhoHasTurn = _player1;
        }

        public int PlayerWhoHasTurn { get; private set; }
        public bool GameEnded { get; private set; }
        public int? WinnerId
        {
            get
            {
                if (GameEnded)
                    return PlayerWhoHasTurn;
                else
                    return null;
            }
        }

        public FourInALineEngine(int player1, int player2)
        {
            _player1 = player1;
            _player2 = player2;
            _board = new GameBoard();

            CoinFlip();
        }


        public List<BoardField> GetBoardState()
        {
            var board = _board.GetBoardForDisplay();
            return board;
        }

        public void PlaceDisc(int x, int playerId)
        {
            if (GameEnded)
                return;

            if (playerId != PlayerWhoHasTurn)
                return;

            if (x < 0 || x > 7)
                return;

            if (!SearchForEmptyBottomField(x, out int y))
                return;


            _board[x, y] = FromPlayerId(playerId);

            if (IsGameOver(x, y))
                OnGameEnded();
            else
                ChangePlayer();
        }

        private bool IsGameOver(int x, int y)
        {
            var player = FromPlayerId(PlayerWhoHasTurn);
            #region Vertical check

            var column = _board.GetColumn(x);
            var verticalStroke = 0;

            for (int i = y; i >= 0; i--)
            {
                if (column[i].FieldState == player)
                    verticalStroke++;
                else
                    break;
            }

            if (verticalStroke >= 4)
                return true;

            #endregion


            #region Horizontal check

            var row = _board.GetRow(y);
            var horizontalPointerLeft = x;
            while (horizontalPointerLeft >= 0)
            {
                if (row[horizontalPointerLeft].FieldState == player)
                    horizontalPointerLeft--;
                else
                    break;
            }

            var horizontalPointerRight = x;
            while (horizontalPointerRight <= 7)
            {
                if (row[horizontalPointerRight].FieldState == player)
                    horizontalPointerRight++;
                else
                    break;
            }

            var horizontalStroke = horizontalPointerRight - horizontalPointerLeft;

            if (horizontalStroke >= 5)
                return true;

            #endregion


            return false;
        }

        private bool SearchForEmptyBottomField(int x, out int y)
        {
            y = -1;
            var column = _board.GetColumn(x);

            var emptyField = column.FirstOrDefault(f => f.FieldState == FieldState.Empty);

            if (emptyField == null)
                return false;

            y = emptyField.Y;
            return true;
        }

    }
}
