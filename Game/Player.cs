using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public abstract class Player
    {
        public char _player;
        public char _opponent;
        abstract public Tuple<int, int> playYourTurn
        (
            Board board, 
            TimeSpan timesup
        );
    }
}
