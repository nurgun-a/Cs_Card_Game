using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cs_Card_Game
{
    class Player
    {
        public string name { get; set; }

        public Queue<Card> card_p;
        public Player() { }
        public Player(string _name, Queue<Card> _card_p)
        {
            name = _name;
            card_p = _card_p;
        }
    }
}
