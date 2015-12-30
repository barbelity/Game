﻿using System;
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
            Stopwatch timer = Stopwatch.StartNew();
            Tuple<int, int> toReturn = getBestPlay(3, ref board);
            Console.Write(timer.ElapsedMilliseconds.ToString());
            /*
            TimeSpan timespan;
            do
            {
                //Random Algorithm - Start
				int test = Heuristic.getHeuristic(board, _player);
                int randomRow;
                int randomCol;
                Random random = new Random();
                do
                {
                    randomRow = random.Next(0, board._rows);
                    randomCol = random.Next(0, board._cols);
                    toReturn  = new Tuple<int, int>(randomRow, randomCol);
                } while (!board.checkIfCellIsEmpty(randomRow, randomCol) && board.checkIfTheGameEnded() == ' ');
                //Random Algorithm - End

            timespan = timer.Elapsed;
            } while (timespan.TotalMilliseconds < timesup.TotalMilliseconds);
            timer.Stop();
            */
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
			//int bestValue;
			int val = 0;

			//stop condition
            if (depth == 0 || childWithMax.checkIfTheGameEnded() != ' ')
                return Heuristic.getHeuristic(childWithMax);

            foreach (Board child in GetChildren(childWithMax, needMax))
            {
                val = minMax(child, depth - 1, !needMax, alpha, beta);

				//update alpha-beta according to returned val
				if (needMax) {
					if (alpha < val)
						alpha = val;
                }
                else
					if (beta > val)
						beta = val;

				//perform prunning
				if (needMax) { 
					if (val >= beta)
						break;
                }
                else
					if (val <= alpha)
						break;

            }

			return val;
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


    }

}
