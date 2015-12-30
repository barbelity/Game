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
            for ( i = 0; i < board._rows; i++)
            {
                for ( j = 0; j < board._cols; j++)
                {
                    //inARow = 0;
                    currentChar = board._board[i, j];
                    if (lastChar == currentChar )
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

            inARow = 0;
            lastChar = ' ';
            //cols
            for ( i = 0; i < board._cols; i++)
            {
                for ( j = 0; j < board._rows; j++)
                {
                    inARow = 0;
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
                            default:
                                //currentChar = ' ';
                                break;
                        }
                        inARow = 0;
                        lastChar = currentChar;
                    }
                }

				lastChar = ' ';
            }
/*			
            int min = Math.Min(board._rows, board._cols);
            inARow = 0;
            lastChar = ' ';
            //x top to bot ->
            for ( i = 0; i < board._cols - board._target + 1; i++)
            {
                for ( j = 0; j < min; j++)
                {
                    inARow = 0;
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
                            default:
                                currentChar = ' ';
                                break;
                        }
                        inARow = 0;
                        lastChar = currentChar;
                    }
                }
            }

            inARow = 0;
            lastChar = ' ';
            //x top to bot <-
            for ( i = 0; i < board._rows - board._target + 1; i++)
            {
                for ( j = 0; j < min; j++)
                {
                    inARow = 0;
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
                            default:
                                currentChar = ' ';
                                break;
                        }
                        inARow = 0;
                        lastChar = currentChar;
                    }
                }
            }

            inARow = 0;
            lastChar = ' ';
            //x bot to top ->
            for ( i = 0; i < board._cols - board._target + 1; i++)
            {
                for ( j = 0; j < min; j++)
                {
                    inARow = 0;
                    currentChar = board._board[board._rows - j, i + j];
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
                            default:
                                currentChar = ' ';
                                break;
                        }
                        inARow = 0;
                        lastChar = currentChar;
                    }
                }
            }
			
            inARow = 0;
            lastChar = ' ';
            //x bot to top <-
            for ( i = 0; i < board._rows - board._target + 1; i++)
            {
                for ( j = i; j < min; j++)
                {
                    inARow = 0;
                    currentChar = board._board[board._rows - j, i + j];
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
                            default:
                                currentChar = ' ';
                                break;
                        }
                        inARow = 0;
                        lastChar = currentChar;
                    }
                }
            }
*/			
            return Convert.ToInt32(ans);
        }
    }
}
