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
            _player     = player;
            _opponent   = opponent;
        }

        public override Tuple<int, int> playYourTurn(Board board, TimeSpan timesup)
        {
            Stopwatch timer = Stopwatch.StartNew();
            Tuple<int, int> toReturn = null;
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
            return toReturn;
        }
    }
}
