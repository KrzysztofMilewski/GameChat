using GameChat.Core.Models;
using System;

namespace GameChat.Games.GameEngines
{
    public class FourInALineEngine
    {
        private enum FieldState
        {
            TakenByPlayer1 = 1,
            TakenByPlayer2 = 2,
            Empty = 0
        }

        private readonly User _player1;
        private readonly User _player2;
        private FieldState[,] _board;
        private bool _isGameActive;

        private void InitializeBoard()
        {
            _board = new FieldState[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                    _board[i, j] = FieldState.Empty;
            }
        }

        private void CoinFlip()
        {
            PlayerWhoHasTurn = _player1.Id;

            //var random = new Random();
            //var coinflip = random.NextDouble();

            //if (coinflip <= 0.5d)
            //    PlayerWhoHasTurn = _player1.Id;
            //else
            //    PlayerWhoHasTurn = _player2.Id;
        }

        private FieldState FromPlayerId(int playerId)
        {
            if (playerId == _player1.Id)
                return FieldState.TakenByPlayer1;
            else
                return FieldState.TakenByPlayer2;
        }

        private void ChangePlayer()
        {
            if (PlayerWhoHasTurn == _player1.Id)
                PlayerWhoHasTurn = _player2.Id;
            else
                PlayerWhoHasTurn = _player1.Id;
        }


        public int PlayerWhoHasTurn { get; private set; }

        public FourInALineEngine(int player1Id, int player2Id)
        {
            _isGameActive = false;
            _player1 = new User()
            {
                Id = player1Id
            };

            _player2 = new User()
            {
                Id = player2Id
            };

            InitializeBoard();
            CoinFlip();
        }

        public void PlaceDisc(int x, int playerId)
        {
            if (playerId != PlayerWhoHasTurn)
                return;

            if (x < 0 || x > 7)
                return;

            if (!SearchForEmptyBottomField(x, out int y))
            {
                return;
            }

            _board[x, y] = FromPlayerId(playerId);

            IsGameOver(x, y);

            ChangePlayer();
            //if (IsGameOver(x, y))
            //    AnnounceResult();
            //else
            //    ChangePlayer();
        }

        private void AnnounceResult()
        {
            //Console.WriteLine($"Game finished, player {PlayerWhoHasTurn} wins");
        }

        private bool IsGameOver(int x, int y)
        {
            #region Vertical check

            int verticalPointer = y - 1;
            int verticalStroke = 1;
            while (verticalPointer >= 0 && verticalStroke < 4)
            {
                if (_board[x, verticalPointer] == FromPlayerId(PlayerWhoHasTurn))
                    verticalStroke++;
                else
                    break;

                verticalPointer--;
            }

            if (verticalStroke >= 4)
                return true;

            #endregion


            #region Horizontal check

            int horizontalPointerLeft = x - 1;
            int horizontalStroke = 1;
            while (horizontalPointerLeft >= 0 && horizontalStroke < 4)
            {
                if (_board[horizontalPointerLeft, y] == FromPlayerId(PlayerWhoHasTurn))
                    horizontalStroke++;
                else
                    break;

                horizontalPointerLeft--;
            }

            int horizontalPointerRight = x + 1;
            while (horizontalPointerRight < 7 && horizontalStroke < 4)
            {
                if (_board[horizontalPointerRight, y] == FromPlayerId(PlayerWhoHasTurn))
                    horizontalStroke++;
                else
                    break;

                horizontalPointerRight++;
            }

            if (horizontalStroke >= 4)
            {
                Console.WriteLine("Horizontal stroke of 4");
                return true;
            }

            #endregion


            return false;
        }

        private bool SearchForEmptyBottomField(int x, out int y)
        {
            for (y = 0; y < 8; y++)
            {
                if (_board[x, y] == FieldState.Empty)
                    return true;
            }
            return false;
        }
    }
}
