using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Board
    {
        public int     _rows;
        public int     _cols;
        public int     _target;
        public char[,] _board;
        public int     _couner;
        public Board
        (
            int rows, 
            int cols, 
            int target
        )
        {
            _rows   = rows;
            _cols   = cols;
            _target = target;
            _couner = 0;
            createBoard();
        }
        public Board
        (
            Board toCopy
        )
        {
            _rows   = toCopy._rows;
            _cols   = toCopy._cols;
            _target = toCopy._target;
            _couner = toCopy._couner;
            copyBoard(toCopy._board);
        }
        private void createBoard()
        {
            _board  = new char[_rows, _cols];
            for(int i = 0; i < _rows; i++)
                for(int j = 0; j < _cols; j++)
                {
                    _board[i,j] = ' ';
                }
        }
        private void copyBoard
        (
            char[,] board
        )
        {
            _board = new char[_rows, _cols];
            for (int i = 0; i < _rows; i++)
                for (int j = 0; j < _cols; j++)
                {
                    _board[i, j] = board[i, j];
                }
        }
        public char checkIfTheGameEnded()
        {
            char player;
            bool boardIsFull = true;
            for (int i = 0; i < _rows; i++)
                for (int j = 0; j < _cols; j++)
                {
                    player = _board[i, j];
                    if (boardIsFull == true && _board[i, j] == ' ')
                        boardIsFull = false;
                    if(player == ' ')
                        continue;
                    if (checkHorizontal     (i, j, player) || 
                        checkVertical       (i, j, player) || 
                        checkDiagonalRight  (i, j, player) ||
                        checkDiagonalLeft   (i, j, player))
                        return player;
                }
            if (boardIsFull == true)
                return 'F';
            return ' ';
        }
        private bool checkVertical
        (
            int row, 
            int col, 
            char player
        )
        {
            for (int i = 0; i < _target; i++)
            {
                if (row + i >= _rows    || 
                    col >= _cols        || 
                    _board[row + i, col] != player)
                    return false;
            }
            return true;
        }
        private bool checkHorizontal
        (
            int row, 
            int col, 
            char player
        )
        {
            for (int i = 0; i < _target; i++)
            {
                if (row >= _rows        ||
                    col + i >= _cols    || 
                    _board[row, col + i] != player)
                    return false;
            }
            return true;
        }
        private bool checkDiagonalRight
        (
            int row, 
            int col, 
            char player
        )
        {
            for (int i = 0; i < _target; i++)
            {
                if (row + i >= _rows    || 
                    col + i >= _cols    || 
                    _board[row + i, col + i] != player)
                    return false;
            }
            return true;
        }
        private bool checkDiagonalLeft
        (
            int row, 
            int col, 
            char player
        )
        {
            for (int i = 0; i < _target; i++)
            {
                if (row - i < 0         || 
                    col + i >= _cols    || 
                    _board[row - i, col + i] != player)
                    return false;
            }
            return true;
        }
        public bool fillPlayerMove
        (
            int row, 
            int col, 
            char player
        )
        {
            if (row > _rows - 1 || col > _cols - 1 || _board[row, col] != ' ')
                return false;
            _board[row, col] = player;
            _couner++;
            return true;
        }
        public bool unFillPlayerMove
        (
            int row, 
            int col
        )
        {
            if (_board[row, col] == ' ')
                return false;
            char temp = _board[row, col];
            _board[row, col] = ' ';
            _couner--;
            return true;
        }
        public bool checkIfCellIsEmpty
        (
            int row, 
            int col
        )
        {
            if (_board[row, col] == ' ')
                return true;
            return false;
        }
        public void printTheBoard()
        {
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                { 
                    if(j == 0)
                        Console.Write("| ");
                    Console.Write(_board[i, j]);
                    Console.Write(" | ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }
    }
}
