using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Collections;
using System.Reflection;

namespace Cs_Card_Game
{
	public class Card: IComparable <Card>
	{
		int rank;
		int suit;
		public Card() { }
		public Card(int _rank, int _suit)
		{
			rank = _rank;
			suit = _suit;
		}		
	public int Rank_get() => rank;
	public int Suit_get() => suit;		
		public string Check_rank(int num)
        {
            if (num==11)
            {
				return "В";
            }
			else if (num == 12)
			{
				return "Д";
			}
			else if (num == 13)
			{
				return "К";
			}
			else if (num == 14)
			{
				return "Т";
			}
			return " ";
        }
		public void Print()
        {
			string[] Suits = { "\u2660", "\u2665", "\u2666", "\u2663" };
			if (suit == 1)
			{
				if (rank > 10)
				{
					ForegroundColor = ConsoleColor.Black;					
					Write($"{Check_rank(rank).PadLeft(4)}{Suits[0].PadRight(1)}");
					//ResetColor();
				}
                else
                {
					ForegroundColor = ConsoleColor.Black;
					Write($"{Convert.ToString(rank).PadLeft(4)}{Suits[0].PadRight(1)}");
					//ResetColor();
				}				
			}
			else if (suit == 2)
			{
				if (rank > 10)
				{
					ForegroundColor = ConsoleColor.Red;
					Write($"{Check_rank(rank).PadLeft(4)}{Suits[1].PadRight(1)}");
					ForegroundColor = ConsoleColor.Black;
					//ResetColor();
				}
                else
                {
					ForegroundColor = ConsoleColor.Red;
					Write($"{Convert.ToString(rank).PadLeft(4)}{Suits[1].PadRight(1)}");
					ForegroundColor = ConsoleColor.Black;
					//ResetColor();
				}				
			}
			else if (suit == 3)
			{
				if (rank > 10)
				{
					ForegroundColor = ConsoleColor.Red;
					Write( $"{Check_rank(rank).PadLeft(4)}{Suits[2].PadRight(1)}");
					ForegroundColor = ConsoleColor.Black;
					//ResetColor();
				}
                else
                {
					ForegroundColor = ConsoleColor.Red;
					Write($"{Convert.ToString(rank).PadLeft(4)}{Suits[2].PadRight(1)}");
					ForegroundColor = ConsoleColor.Black;
					//ResetColor();
				}				
			}
			else if (suit == 4)
			{
				if (rank > 10)
				{
					ForegroundColor = ConsoleColor.Black;
					Write($"{Check_rank(rank).PadLeft(4)}{Suits[3].PadRight(1)}");
				}
                else
                {
					ForegroundColor = ConsoleColor.Black;
					Write($"{Convert.ToString(rank).PadLeft(4)}{Suits[3].PadRight(1)}");
				}				
			}
		}
        public int CompareTo(Card obj)
        {
			if (Rank_get() > obj.Rank_get()) return 1;
			return 0;
			//throw new NotImplementedException();
		}
    }
	public class Game_card
	{
		List<Player> players1=new List<Player>();
		List<Card> deck=new List<Card>();
		public Game_card() { }
		public void Creat_deck()
        {
			for (int i = 1; i < 5; i++)
			{
				for (int j = 6; j < 15; j++)
				{
					deck.Add(new Card(j, i));
				}
			}
		}
		public void New_Game()
        {
            try
            {
				Write($"Введите количество игроков (от 2 до 8): "); int quantity_p = int.Parse(ReadLine());
				if (quantity_p < 2 || quantity_p > 8)
				{
					WriteLine($"		ОШИБКА ВВОДА");
					return;
				}
				Creat_deck();

				Random rnd = new Random();
				Queue<Card> deck_tmp = new Queue<Card>();

				for (int i = 0; i < quantity_p; i++)
				{
					players1.Add(new Player());//
					players1[i].name = "Plaer " + (i + 1);
					players1[i].card_p = new Queue<Card>();
				}

				for (int j = 0; j < 36; j++)
				{
					int temp = 0;
					temp = rnd.Next(0, deck.Count());
					deck_tmp.Enqueue(deck[temp]);
					deck.RemoveAt(temp);
				}
				Razdacha(ref deck_tmp, quantity_p);
				deck_tmp.Clear();
				Game(deck_tmp);
				Win_Game();
			}
            catch (Exception ex)
            {

				Write($"\n\n		{ex.Message}\n\n		Нажмите \"Enter\" чтобы продолжить\n\n");
				ReadLine();
            }			
		}			
		public void Razdacha(ref Queue<Card> deck_tmp1,int q)
        {
			while (deck_tmp1.Any())
			{
				for (int i = 0; i < q; i++)
				{
					players1[i].card_p.Enqueue(deck_tmp1.Dequeue());
					if (!deck_tmp1.Any()) break;
				}
				if (!deck_tmp1.Any()) break;
			}
		}
		public void Game(Queue<Card> deck_tmp1)
        {
		goto1:
			bool k1 = false, k2 = false;
			goto2:
			Clear();
			for (int i = 0; i < players1.Count(); i++)
			{
				if (!players1[i].card_p.Any()) return;
				
				Write($"{players1[i].name} --> ");
				if (k1) 
				{
					players1[i].card_p.Peek().Print();
					Write($" <--");
				}
				
				if (k1&&k2) deck_tmp1.Enqueue(players1[i].card_p.Peek());
                foreach (var item in players1[i].card_p) {item.Print();}
				WriteLine();
				WriteLine($"------------------------------------------------------------------");
			}
			if (k1 && k2)
			{
				Win_raund(deck_tmp1);
				goto goto1;
			}
			switch (ReadKey(true).Key)
			{
				case ConsoleKey.Enter:
					{
						if (k1)
						{
							k2 = true;
						}
						goto goto2;
					}
				case ConsoleKey.Escape:
					{
						return;
					}
				//break;
				case ConsoleKey.LeftArrow:
					{
						k1 = true;
						goto goto2;
					}
					//break;
				case ConsoleKey.RightArrow:
					{
						k1 = false;
						goto goto2;
					}//break;

				default:
					goto goto2;
			}
		}
		public void Win_raund(Queue<Card> deck_tmp1)
        {
			Card max_el = deck_tmp1.Max();
			int num = 0;
			for (int i = 0; i < players1.Count(); i++)
            {
                if (players1[i].card_p.Dequeue().Rank_get()==max_el.Rank_get())
                {
					num = i;
                }
            }
			WriteLine($"\n\n		Raund win {players1[num].name}\n\n		Нажмите \"Enter\" чтобы продолжить");
            while (deck_tmp1.Any())
            {
				players1[num].card_p.Enqueue(deck_tmp1.Dequeue());
				if (!deck_tmp1.Any()) break;
			}
			ReadKey();
        }
		public void Win_Game()
        {
			Clear();
			int w = 0;
			for (int i = 0; i < players1.Count(); i++)
            {
				if (players1[i].card_p.Count() > players1[w].card_p.Count())
					w = i;
			}
            for (int i = 0; i < players1.Count(); i++)
            {
				if (!players1[i].card_p.Any()) 
				{
					WriteLine($"{players1[i].name} --> Empty <--");
					WriteLine();
					continue;
				}
				Write($"{players1[i].name} --> ");
				foreach (var item in players1[i].card_p) { item.Print(); }
				WriteLine();
				WriteLine();
			}
			WriteLine($"---------------------------");
			WriteLine($"{ players1[w].name}  WIN");
			WriteLine($"---------------------------");
			Write($"{players1[w].name} --> ");
			foreach (var item in players1[w].card_p) {item.Print();}
			WriteLine($"");
			WriteLine($"---------------------------");
		}
	
	}
}
