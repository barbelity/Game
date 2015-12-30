using Player2DLL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Game
{
    class Program
    {
        public const int m_numberOfGames      = 1;
        public const int m_boardRows          = 3;
        public const int m_boardCols          = 3;
        public const int m_targetSize         = 3;
        public const int m_gameLevel          = 2;
        public const bool m_printTheGame      = true;
        public const bool m_printResult       = true;
        public const bool m_printAllResults   = true;
        static void Main(string[] args)
        {
			char playerTurn         = 'O';
            char winner             = ' ';
            Board board             = createEmptyBoard();
            int player1wins         = 0;
            int player2wins         = 0;
            Random random = new Random();
            for (int game = 0; game < m_numberOfGames; game++)
            {
                board       = createEmptyBoard();
				////////////////////////////////
				////////////////////////////////
				board.fillPlayerMove(1, 0, 'X');
				board.fillPlayerMove(1, 2, 'X');
				board.fillPlayerMove(0, 1, 'O');
				board.fillPlayerMove(2, 2, 'O');
				////////////////////////////////
				////////////////////////////////
                winner      = ' ';
                switchPlayers(ref playerTurn);
                do
                {
                    if (playerTurn == 'X')
                    {
                        Turn(board, new Player1('X','O'), 'O', ref winner, ref playerTurn);  //Your Turn
                    }
                    else if (playerTurn == 'O')
                    {
                        Turn(board, new Player2('O', 'X'), 'X', ref winner, ref playerTurn);
                    }
                    if(winner == ' ')
                        winner = board.checkIfTheGameEnded();
                    if (m_printTheGame)                                       //Watch the game
                    {
                        //Console.Clear();
						System.Console.WriteLine();
                        board.printTheBoard();
                    }
                } while (winner == ' ');

                if (m_printResult)
                    printWinnerLine(game, winner, board._couner);             //Watch the game result

                if (winner == 'X')
                    player1wins++;
                else if (winner == 'O')
                    player2wins++;
            }

            if(m_printAllResults)
                printAllGamesResult(player1wins, player2wins);                //Watch all games result
        }

        private static Board createEmptyBoard()
        {
            return new Board(m_boardRows, m_boardCols, m_targetSize);
        }

        private static void switchPlayers(ref char playerTurn)
        {
            if (playerTurn == 'X')
                playerTurn = 'O';
            else
                playerTurn = 'X';
        }

        private static void printWinnerLine(int game, char winner, int counter)
        {
            Console.WriteLine("The winner of Game " + (game + 1) + " is: " + winner + " after " + counter + " moves");
            //Console.ReadLine();
        }

        private static void printAllGamesResult(int player1wins, int player2wins)
        {
            Console.WriteLine("Player1 (X) wins:  " + player1wins + "\nPlayer2 (O) wins:  " + player2wins + "\nTies:              " + (m_numberOfGames - player1wins - player2wins));
            Console.ReadLine();
        }

        private static void Turn(Board board, Player player, char opponent, ref char winner, ref char playerTurn)
        {
            int stopMilliseconds;
			if (opponent == 'O')
				//stopMilliseconds = 100;
				stopMilliseconds = 10000;
			else
				stopMilliseconds = timeByLevel();
            TimeSpan timesup     = new TimeSpan(0, 0, 0, 0, stopMilliseconds);
            Stopwatch timer      = Stopwatch.StartNew();
            Tuple<int, int> move = player.playYourTurn(new Board(board), new TimeSpan(0, 0, 0, 0, stopMilliseconds));
            timer.Stop();
            TimeSpan timespan    = timer.Elapsed;
            if (timesup.TotalMilliseconds < timespan.TotalMilliseconds - 10 || !board.fillPlayerMove(move.Item1, move.Item2, playerTurn))
            {
                winner      = opponent;
            }
            else
                playerTurn  = opponent;
        }

        private static int timeByLevel()
        {
            if (m_gameLevel <= 1)
                return 5;
            else if (m_gameLevel == 2)
                return 20;
            else if (m_gameLevel == 3)
                return 50;
            else if (m_gameLevel == 4)
                return 100;
            else
                return 150;
        }
    }
}
