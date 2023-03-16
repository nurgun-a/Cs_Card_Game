using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cs_Card_Game
{
	class Program
    {
        static void Main(string[] args)
        {
            BackgroundColor = ConsoleColor.White;
            ForegroundColor = ConsoleColor.Black;
            
        g1:
            Clear();
            WriteLine(@"

                        Программа «Карточная игра» 

    стрелкой налево  - выкладываете карты
    стрелкой направо - возвращаете обратно
    Enter            - если карты выложены, играете раунд
    Esc              - завершить иргу
    
                        Игра будет продолжаться до тех пор, пока у какого-нибудь игрока не останется карт


");
           
            Game_card s = new Game_card();
            s.New_Game();
            WriteLine($"Если хотите возобновить игру, нажмите \"Enter\"");
			switch (ReadKey(true).Key)
			{
				case ConsoleKey.Enter:
					{
						goto g1; ;
					}
				default:
					break;
			}
		}
    }
}
