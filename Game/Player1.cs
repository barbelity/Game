using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game
{
    public class Player1 : Player
    {
        public Player1(char player, char opponent)
        {
            _player = player;
            _opponent = opponent;
        }

        public override Tuple<int, int> playYourTurn(Board board, TimeSpan timesup)
        {
            Tuple<int, int> toReturn = null;
            if (board._cols * board._rows < 50)
            {
                toReturn = getBestPlay(3, ref board);
            }
            else if (board._cols * board._rows < 150)
            {
                toReturn = getBestPlay(2, ref board);
            }
            else
            {
                for (int i = 0; i < board._rows; i++)
                {
                    for (int j = 0; j < board._cols; j++)
                    {
                        if (board.checkIfCellIsEmpty(i, j))
                        {
                            return new Tuple<int, int>(i, j);

                        }
                    }
                }
            }
            return toReturn;
        }

        private Tuple<int, int> getBestPlay(int depth, ref Board board)
        {
            int score, max = int.MinValue;
            Tuple<int, int> ans = null;
            for (int i = 0; i < board._rows; i++)
            {
                for (int j = 0; j < board._cols; j++)
                {
                    if (board.checkIfCellIsEmpty(i, j))
                    {
                        Board temp = new Board(board);
                        temp.fillPlayerMove(i, j, 'X');
                        score = minMax(temp, depth - 1, false, int.MinValue + 1, int.MaxValue - 1);
                        if (score > max)
                        {
                            ans = new Tuple<int, int>(i, j);
                            max = score;
                        }
                    }
                }
            }
            return ans;
        }



        private int minMax(Board childWithMax, int depth, bool needMax, int alpha, int beta)
        {
            //stop condition
            if (depth == 0 || childWithMax.checkIfTheGameEnded() != ' ')
                return getHeuristic(childWithMax);

            int calcVal;
            //initial value depends on needMax
            int currVal = needMax ? alpha : beta;

            foreach (Board child in GetChildren(childWithMax, needMax))
            {
                calcVal = minMax(child, depth - 1, !needMax, alpha, beta);

                //change value depends on needMax
                currVal = needMax ? Math.Max(currVal, calcVal) : Math.Min(currVal, calcVal);

                //update alpha-beta according to returned val
                if (needMax)
                {
                    if (alpha < currVal)
                        alpha = currVal;
                }
                else
                    if (beta > currVal)
                    beta = currVal;

                //perform prunning
                if (needMax)
                {
                    if (currVal >= beta)
                        break;
                }
                else
                    if (currVal <= alpha)
                    break;

            }

            return currVal;
        }

        private IEnumerable<Board> GetChildren(Board father, bool needMax)
        {
            List<Board> ans = new List<Board>();
            for (int i = 0; i < father._rows; i++)
            {
                for (int j = 0; j < father._cols; j++)
                {
                    if (father.checkIfCellIsEmpty(i, j))
                    {
                        Board temp = new Board(father);
                        temp.fillPlayerMove(i, j, needMax ? 'X' : 'O');
                        ans.Add(temp);
                    }
                }
            }
            return ans;
        }

        //returns the calculated heuristic for the board given
        public static int getHeuristic(Board board)
        {
            int i, j;
            double ans = 0;
            double inARow = 0;
            char lastChar = ' ', currentChar;
            //rows
            for (i = 0; i < board._rows; i++)// all rows
            {
                for (j = 0; j < board._cols; j++) //one row
                {
                    //inARow = 0;
                    currentChar = board._board[i, j];
                    if (lastChar == currentChar)
                        inARow++;
                    else
                    {
                        switch (lastChar)
                        {
                            case 'X':
                                ans += Math.Pow(10, inARow);
                                break;
                            case 'O':
                                ans -= Math.Pow(10, inARow);
                                break;
                        }
                        inARow = 0;
                        lastChar = currentChar;
                    }
                }
                switch (lastChar)
                {
                    case 'X':
                        ans += Math.Pow(10, inARow);
                        break;
                    case 'O':
                        ans -= Math.Pow(10, inARow);
                        break;
                }
                inARow = 0;
                lastChar = ' ';
            }



            //cols
            for (i = 0; i < board._cols; i++)// all cols
            {
                inARow = 0;
                lastChar = ' ';
                for (j = 0; j < board._rows; j++) // one col
                {

                    currentChar = board._board[j, i];
                    if (lastChar == currentChar)
                        inARow++;
                    else
                    {
                        switch (lastChar)
                        {
                            case 'X':
                                ans += Math.Pow(10, inARow);
                                break;
                            case 'O':
                                ans -= Math.Pow(10, inARow);
                                break;
                        }
                        inARow = 0;
                        lastChar = currentChar;
                    }
                }
                switch (lastChar)
                {
                    case 'X':
                        ans += Math.Pow(10, inARow);
                        break;
                    case 'O':
                        ans -= Math.Pow(10, inARow);
                        break;
                }
            }

            int min = Math.Min(board._rows, board._cols);

            //x top to bot ->
            for (i = 0; i < board._cols - board._target + 1; i++)
            {
                inARow = 0;
                lastChar = ' ';
                for (j = 0; j + i < min; j++)
                {
                    currentChar = board._board[j, j + i];
                    if (lastChar == currentChar)
                        inARow++;
                    else
                    {
                        switch (lastChar)
                        {
                            case 'X':
                                ans += Math.Pow(10, inARow);
                                break;
                            case 'O':
                                ans -= Math.Pow(10, inARow);
                                break;

                        }
                        inARow = 0;
                        lastChar = currentChar;
                    }
                }
                switch (lastChar)
                {
                    case 'X':
                        ans += Math.Pow(10, inARow);
                        break;
                    case 'O':
                        ans -= Math.Pow(10, inARow);
                        break;

                }
            }


            //x top to bot <-
            for (i = 0; i < board._rows - board._target + 1; i++)
            {
                inARow = 0;
                lastChar = ' ';
                for (j = 0; j + i < min; j++)
                {
                    currentChar = board._board[j + i, j];
                    if (lastChar == currentChar)
                        inARow++;
                    else
                    {
                        switch (lastChar)
                        {
                            case 'X':
                                ans += Math.Pow(10, inARow);
                                break;
                            case 'O':
                                ans -= Math.Pow(10, inARow);
                                break;
                        }
                        inARow = 0;
                        lastChar = currentChar;
                    }
                }
                switch (lastChar)
                {
                    case 'X':
                        ans += Math.Pow(10, inARow);
                        break;
                    case 'O':
                        ans -= Math.Pow(10, inARow);
                        break;

                }
            }


            //x bot to top ->
            for (i = 0; i < board._cols - board._target + 1; i++)
            {
                inARow = 0;
                lastChar = ' ';
                for (j = 0; j + i < min; j++)
                {
                    currentChar = board._board[board._rows - j - 1, i + j];
                    if (lastChar == currentChar)
                        inARow++;
                    else
                    {
                        switch (lastChar)
                        {
                            case 'X':
                                ans += Math.Pow(10, inARow);
                                break;
                            case 'O':
                                ans -= Math.Pow(10, inARow);
                                break;
                        }
                        inARow = 0;
                        lastChar = currentChar;
                    }
                }
                switch (lastChar)
                {
                    case 'X':
                        ans += Math.Pow(10, inARow);
                        break;
                    case 'O':
                        ans -= Math.Pow(10, inARow);
                        break;

                }
            }


            //x bot to top <-
            for (i = 0; i < board._rows - board._target + 1; i++)
            {
                inARow = 0;
                lastChar = ' ';
                for (j = i; j + i + 1 < board._rows; j++)
                {
                    currentChar = board._board[board._rows - j - i - 1, j];
                    if (lastChar == currentChar)
                        inARow++;
                    else
                    {
                        switch (lastChar)
                        {
                            case 'X':
                                ans += Math.Pow(10, inARow);
                                break;
                            case 'O':
                                ans -= Math.Pow(10, inARow);
                                break;
                        }
                        inARow = 0;
                        lastChar = currentChar;

                    }
                }
                switch (lastChar)
                {
                    case 'X':
                        ans += Math.Pow(10, inARow);
                        break;
                    case 'O':
                        ans -= Math.Pow(10, inARow);
                        break;

                }
            }

            //get the center
            for (i = board._rows / 3; i < (board._rows / 3) * 2; i++)
            {
                for (j = board._cols / 3; j < (board._cols / 3) * 2; j++)
                {
                    currentChar = board._board[i, j];
                    switch (currentChar)
                    {
                        case 'X':
                            ans += 2;
                            break;
                        case 'O':
                            ans -= 2;
                            break;
                    }
                }
            }
            return Convert.ToInt32(ans);
        }
    }

}
