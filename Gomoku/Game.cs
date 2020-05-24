using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    class Game
    {
        private Board _board;

        private bool _isBlack = true;
        private bool _isGameOver = false;
        private bool _isAIMode = false;
        public bool IsGameOver { get {return _isGameOver; } }
        public bool IsBlack { get { return _isBlack; } }
        public bool IsAIMode { get { return _isAIMode; } set { this._isAIMode = value; } }

        public Game()
        {
            _board = new Board();
        }

        public void Reset()
        {
            _board.Reset();
            _isBlack = true;
            _isGameOver = false;

        }

        public bool CanBePlaced(int x, int y)
        {
            return _board.CanBePlaced(x, y);
        }

        public Chess PlaceAChess(int x, int y)
        {
            Chess chess;
            if (_isBlack) {
                chess = _board.PlaceAChess(x, y, Chess.ChessType.Black);
            } else
            {
                chess = _board.PlaceAChess(x, y, Chess.ChessType.White);
            }
            if (chess != null)
            {
                CheckWinner();
                _isBlack = !_isBlack;
                return chess;
            }
            return null;
        }

        private void CheckWinner()
        {
            _isGameOver = _board.CheckLineUp(_isBlack ? Chess.ChessType.Black : Chess.ChessType.White);
        }
    }
}
