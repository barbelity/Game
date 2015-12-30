using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class Heuristic
    {

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
                    if (j == board._cols - 1)
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
                    if (j == board._rows - 1)
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

                lastChar = ' ';
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
            /*
            //get the center
            for (i = board._rows / 3; i < (board._rows / 3) * 2; i++)
            {
                for (j = board._cols / 3; j < (board._cols / 3) * 2; j++)
                {
                    currentChar = board._board[i,j];
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
            } */
            return Convert.ToInt32(ans);
        }
    }
}
