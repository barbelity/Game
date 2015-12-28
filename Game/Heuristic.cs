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
            double ans = 0;
            double inARow = 0;
            char player = ' ',charAt;
            //rows
            for (int i = 0; i < board._rows; i++)
            {
                for (int j = 0; j < board._cols; j++)
                {
                    charAt = board._board[i, j];
                    switch (charAt)
                    {
                        case 'X':
                            if (player == charAt)
                            {
                                inARow++;
                                ans += Math.Pow(10, inARow);
                            }
                            else
                            {
                                ans += 1;
                                inARow = 0;
                                player = charAt;
                            }

                            break;
                        case 'O':
                            if (player == charAt)
                            {
                                inARow++;
                                ans -= Math.Pow(10, inARow);
                            }
                            else
                            {
                                ans -= 1;
                                inARow = 0;
                                player = charAt;
                            }

                            break;
                        default:
                            player = ' ';
                            inARow = 0;
                            break;
                    }
                }
            }

            inARow = 0;
            player = ' ';
            //cols
            for (int i = 0; i < board._cols; i++)
            {
                for (int j = 0; j < board._rows; j++)
                {
                    charAt = board._board[i, j];
                    switch (charAt)
                    {
                        case 'X':
                            if (player == charAt)
                            {
                                inARow++;
                                ans += Math.Pow(10, inARow);
                            }
                            else
                            {
                                ans += 1;
                                inARow = 0;
                                player = charAt;
                            }

                            break;
                        case 'O':
                            if (player == charAt)
                            {
                                inARow++;
                                ans -= Math.Pow(10, inARow);
                            }
                            else
                            {
                                ans -= 1;
                                inARow = 0;
                                player = charAt;
                            }

                            break;
                        default:
                            player = ' ';
                            inARow = 0;
                            break;
                    }
                }
            }

            inARow = 0;
            player = ' ';
            //x top to bot ->
            for (int i = 0; i < board._cols; i++)
            {
                int min = Math.Min(board._rows, board._cols);
                for (int j = i; j+i < min; j++)
                {
                    charAt = board._board[j, j+i];
                    switch (charAt)
                    {
                        case 'X':
                            if (player == charAt)
                            {
                                inARow++;
                                ans += Math.Pow(10, inARow);
                            }
                            else
                            {
                                ans += 1;
                                inARow = 0;
                                player = charAt;
                            }

                            break;
                        case 'O':
                            if (player == charAt)
                            {
                                inARow++;
                                ans -= Math.Pow(10, inARow);
                            }
                            else
                            {
                                ans -= 1;
                                inARow = 0;
                                player = charAt;
                            }

                            break;
                        default:
                            player = ' ';
                            inARow = 0;
                            break;
                    }
                }
            }

            inARow = 0;
            player = ' ';
            //x top to bot <-
            for (int i = 0; i < board._cols; i++)
            {
                int min = Math.Min(board._rows, board._cols);
                for (int j = i; j+i < min; j++)
                {
                    charAt = board._board[j+1, j];
                    switch (charAt)
                    {
                        case 'X':
                            if (player == charAt)
                            {
                                inARow++;
                                ans += Math.Pow(10, inARow);
                            }
                            else
                            {
                                ans += 1;
                                inARow = 0;
                                player = charAt;
                            }

                            break;
                        case 'O':
                            if (player == charAt)
                            {
                                inARow++;
                                ans -= Math.Pow(10, inARow);
                            }
                            else
                            {
                                ans -= 1;
                                inARow = 0;
                                player = charAt;
                            }

                            break;
                        default:
                            player = ' ';
                            inARow = 0;
                            break;
                    }
                }
            }
            
            return Convert.ToInt32(ans);
		}
	}
}
