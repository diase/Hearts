using System;
using System.Collections.Generic;

namespace Heartsv3
{
    public class Game
    {
        private int CurrentPlayer;
        private List<int> Scores;
        private List<Player> Players;
        private List<int> Deck;
        private List<List<int>> InitialHands = new List<List<int>>();
        private int Winner;

        public Game()
        {
            Players = new List<Player>();
            CurrentPlayer = 0;
            Scores = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                Scores.Add(0);
                Players.Add(new Player());
            }
            
            //Dealer obj created
            Dealer d = new Dealer();
            Deck = d.Shuffle();

            InitialHands.Add(new List<int>());
            InitialHands.Add(new List<int>());
            InitialHands.Add(new List<int>());
            InitialHands.Add(new List<int>());
            MakeUserHands();
        }

        private void MakeUserHands()
        {
            for (int i = 0; i < 52; i++)
            {
                int card = Deck[i];
                if (i < 13)
                {
                    InitialHands[0].Add(card);
                }
                else if (i < 26)
                {
                    InitialHands[1].Add(card);
                }
                else if (i < 39)
                {
                    InitialHands[2].Add(card);
                }
                else
                {
                    InitialHands[3].Add(card);
                }

            }

            Players[0].SetHand(InitialHands[0]);
            Players[1].SetHand(InitialHands[1]);
            Players[2].SetHand(InitialHands[2]);
            Players[3].SetHand(InitialHands[3]);

        }

        private void PrintHand(int user)
        {
            List<int> h = Players[user].GetHand();
            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict.Add(0, "A");
            dict.Add(1, "2");
            dict.Add(2, "3");
            dict.Add(3, "4");
            dict.Add(4, "5");
            dict.Add(5, "6");
            dict.Add(6, "7");
            dict.Add(7, "8");
            dict.Add(8, "9");
            dict.Add(9, "10");
            dict.Add(10, "J");
            dict.Add(11, "Q");
            dict.Add(12, "K");
            List<string> toPrint = new List<string>();
            List<string> sp = new List<string>();
            List<string> he = new List<string>();
            List<string> di = new List<string>();
            List<string> cl = new List<string>();
            for (int i = 0; i < h.Count; i++)
            {
                string suit;
                string card = dict[(h[i] % 13)];
                if (h[i] < 13)
                {
                    //Console.WriteLine(h[i]);
                    suit = "S";
                    string toAdd = card + suit;
                    //Console.WriteLine(toAdd);
                    sp.Add(toAdd);
                    //Console.WriteLine("Spade Length now: " + spade.Count);
                }
                else if (h[i] < 26)
                {
                    suit = "H";
                    string toAdd = card + suit;
                    he.Add(toAdd);
                }
                else if (h[i] < 39)
                {
                    suit = "D";
                    string toAdd = card + suit;
                    di.Add(toAdd);
                }
                else
                {
                    suit = "C";
                    string toAdd = card + suit;
                    cl.Add(toAdd);
                }
                
            }

            toPrint = sp;
            toPrint.AddRange(di);
            toPrint.AddRange(cl);
            toPrint.AddRange(he);
            Console.WriteLine("Printing User hand: ");////////////////////////////////////////////
            Console.WriteLine(string.Join(" ", toPrint.ToArray()));
        }

        private int ConvertStringInt(string s)
        {
            string suit;
            string card;
            if (s.Length == 3)
            {
                suit = s.Substring(2, 1);
                card = s.Substring(0, 2);
            }
            else
            {
                suit = s.Substring(1, 1);
                card = s.Substring(0, 1);
            }

            int toAdd;
            if (suit == "S")
            {
                toAdd = 0;
            }
            else if ( suit == "H")
            {
                toAdd = 13;
            }
            else if (suit == "D")
            {
                toAdd = 26;
            }
            else
            {
                toAdd = 39;
            }
            Dictionary<string, int> dict = new Dictionary<string, int>();
            dict.Add("A", 0);
            dict.Add("2", 1);
            dict.Add("3", 2);
            dict.Add("4", 3);
            dict.Add("5", 4);
            dict.Add("6", 5);
            dict.Add("7", 6);
            dict.Add("8", 7);
            dict.Add("9", 8);
            dict.Add("10", 9);
            dict.Add("J", 10);
            dict.Add("Q", 11);
            dict.Add("K", 12);
            
            int newCard = dict[card] + toAdd;
            return newCard;
        }

        private void Pass()
        {
            List<int> p0Out;
            List<int> p1Out;
            List<int> p2Out;
            List<int> p3Out;
            List<int> p0In;
            List<int> p1In;
            List<int> p2In;
            List<int> p3In;

            Console.WriteLine("Write First Card to Pass: ");
            string c1 = Console.ReadLine();
            Console.WriteLine("Write Second Card to Pass: ");
            string c2 = Console.ReadLine();
            Console.WriteLine("Write Third Card to Pass: ");
            string c3 = Console.ReadLine();

            List<int> toPassIn = new List<int>();
            //Console.WriteLine(c1 + ConvertStringInt(c1));
            toPassIn.Add(ConvertStringInt(c1));
            //Console.WriteLine(c2 + ConvertStringInt(c2));
            toPassIn.Add(ConvertStringInt(c2));
            //Console.WriteLine(c3 + ConvertStringInt(c3));
            toPassIn.Add(ConvertStringInt(c3));

            p0Out = toPassIn;
            p1Out = Players[1].toPass();
            p2Out = Players[2].toPass();
            p3Out = Players[3].toPass();

            p0In = p3Out;
            //Console.WriteLine("p0In Length " + p0In.Count);
            p1In = p0Out;
            p2In = p1Out;
            p3In = p2Out;

            Players[0].AddToHand(p0In);
            Players[1].AddToHand(p1In);
            Players[2].AddToHand(p2In);
            Players[3].AddToHand(p3In);
            for (int i = 0; i < 3; i++)
            {
                //Console.WriteLine("Removing from P0");
                Players[0].UpdateHand(p0Out[i]);
                //Console.WriteLine("Removing from P1");
                Players[1].UpdateHand(p1Out[i]);
                //Console.WriteLine("Removing from P2");
                Players[2].UpdateHand(p2Out[i]);
                //Console.WriteLine("Removing from P3");
                Players[3].UpdateHand(p3Out[i]);
            }

            //PrintHand(0);
        }

        private int Find2Clubs()
        {
            List<int> p0Hand = Players[0].GetHand();
            List<int> p1Hand = Players[1].GetHand();
            List<int> p2Hand = Players[2].GetHand();
            List<int> p3Hand = Players[3].GetHand();

            int toReturn = 0;

            if (p0Hand.Contains(40))
            {
                toReturn = 0;
            }
            else if (p1Hand.Contains(40))
            {
                toReturn = 1;
            }
            else if (p2Hand.Contains(40))
            {
                toReturn = 2;
            }
            else if (p3Hand.Contains(40))
            {
                toReturn = 3;
            }

            return toReturn;
        }

        private void PrintCard(int x)
        {
            string suit;
            string card;
            if (x < 13)
            {
                suit = "S";
            }
            else if (x < 26)
            {
                suit = "H";
            }
            else if (x < 39)
            {
                suit = "D";
            }
            else
            {
                suit = "C";
            }

            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict.Add(0, "A");
            dict.Add(1, "2");
            dict.Add(2, "3");
            dict.Add(3, "4");
            dict.Add(4, "5");
            dict.Add(5, "6");
            dict.Add(6, "7");
            dict.Add(7, "8");
            dict.Add(8, "9");
            dict.Add(9, "10");
            dict.Add(10, "J");
            dict.Add(11, "Q");
            dict.Add(12, "K");

            card = dict[x % 13];
            Console.WriteLine(card + suit);
        }
        public void Play()
        {
            //Console.WriteLine();
            PrintHand(0);//takes in List<int> Players[0].Hand and translates and prints
            
            Pass();
            //will call Player.RemFromHand(List<int> h)
            //will call Player.AddToHand(List<int> in)
            //for all four players
            //user input needed to be taken in
            
            //find player with 2 of clubs
            int pw2 = Find2Clubs();//
            int toAdd = 0;
            List<int> hand = new List<int>();
            CurrentPlayer = pw2;
            Console.WriteLine(CurrentPlayer + "Has 2C");
            if (pw2 != 0)
            {
                //Console.WriteLine("Player Hand is First: ");
                //PrintHand(pw2);
                AI model = new AI(Players[CurrentPlayer].GetHand(), hand);
                toAdd = model.GetCard();
                //toAdd = Players[CurrentPlayer].move(hand);
                //Console.WriteLine("Player Hand is Now: ");
                //PrintHand(pw2);
                hand.Add(toAdd);
                //Console.WriteLine("Player: " + CurrentPlayer);
                PrintCard(toAdd);
                

            }
            else
            {
                Console.WriteLine("You Must Play 2 of Clubs");
                Players[0].UpdateHand(40);
                hand.Add(40);
                PrintCard(40);
            }

            for (int i = 0; i < 3; i++)
            {
                CurrentPlayer = (CurrentPlayer + 1) % 4;
                if (CurrentPlayer != 0)
                {
                    AI model = new AI(Players[CurrentPlayer].GetHand(), hand);
                    toAdd = model.GetCard();
                    
                    //toAdd = Players[CurrentPlayer].move(hand);
                    hand.Add(toAdd);
                    PrintCard(toAdd);
                }
                else
                {
                    string strCard;
                    int card;
                    PrintHand(0);
                    Console.WriteLine("Play a Card");
                    strCard = Console.ReadLine();
                    card = ConvertStringInt(strCard);
                    Players[0].UpdateHand(card);
                    //PrintCard(card);
                    hand.Add(card);
                }
            }
            
            DetScore1(hand);//here instead of setting back to first player. Current player remains at last
            Console.WriteLine("Winner: " + Winner);
            PrintScore();
            
            //iterative part. 12 hands left
            Console.WriteLine("Starting Iterative Part");
            for (int i = 0; i < 12; i++)
            {
                hand = new List<int>();
                int counter = 0;
                CurrentPlayer = Winner;
                //Console.WriteLine("i: " + i);
                Console.WriteLine("Starting with : " + CurrentPlayer);
                for (int j = 0; j < 4; j++)
                {
                    //Console.WriteLine("j: " + j);
                    //Console.WriteLine(CurrentPlayer);
                    counter += 1;
                    //Console.WriteLine();
                    //Console.WriteLine("Counter: " + counter);
                    if (CurrentPlayer == 0)
                    {
                        PrintHand(0);
                        Console.WriteLine("Play a Card: ");
                        string c = Console.ReadLine();
                        int x = ConvertStringInt(c);
                        Players[0].UpdateHand(x);
                        hand.Add(x);
                        //PrintCard(x);
                    }
                    else
                    {
                        AI model = new AI(Players[CurrentPlayer].GetHand(), hand);
                        toAdd = model.GetCard();
                        //toAdd = Players[CurrentPlayer].move(hand);
                        hand.Add(toAdd);
                        PrintCard(toAdd);
                    }

                    CurrentPlayer = (CurrentPlayer + 1) % 4;
                    
                    if (counter % 4 == 0)
                    {
                        //Console.WriteLine("counter = " + counter);
                        DetScore(hand);
                        Console.WriteLine("Winner: " + Winner);
                        PrintScore();
                        Console.WriteLine();
                    }
                    
                }
            }
        }

        private void PrintScore()
        {
            Console.WriteLine("Scores: ");
            Console.WriteLine("You: " + Scores[0] + " P1: " + Scores[1] + " P2: " + Scores[2] + " P3: " + Scores[3]);
            Console.WriteLine();
        }

        private void DetScore(List<int> hand)
        {
            int winner;
            List<int> inSuit = new List<int>();
            int a; //>=
            int b; //<
            if (hand[0] < 13)
            {
                a = 0;
                b = 13;
            }
            else if (hand[0] < 26)
            {
                a = 13;
                b = 26;
            }
            else if (hand[0] < 39)
            {
                a = 26;
                b = 39;
            }
            else
            {
                a = 39;
                b = 52;
            }

            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i] >= a && hand[i] < b)
                {
                    inSuit.Add(1);
                }
                else
                {
                    inSuit.Add(0);
                }
            }
            List<int> temp = new List<int>();
            for (int i = 0; i < hand.Count; i++)
            {
                if (inSuit[i] == 0)
                {
                    temp.Add(-1);
                }
                else
                {
                    temp.Add(hand[i]);
                }
            }
            //now calling FindMaxInd(temp) will tell max ind in hand. Must convert this to player
            int maxIndHand = FindMaxInd(temp);
            //Console.WriteLine("Max ind: " + maxIndHand);
            //int cp = (CurrentPlayer + 1) % 4;
            winner = (CurrentPlayer + maxIndHand)%4;
            Winner = winner;
            int points = 0;
            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i] >= 13 && hand[i] < 26)
                {
                    points += 1;
                }

                if (hand[i] == 11)
                {
                    points += 13;
                }
            }

            if (points != 26)
            {
                Scores[Winner] += points;
            }
            else
            {
                for (int i = 0; i < Scores.Count; i++)
                {
                    if (i != winner)
                    {
                        Scores[i] += 26;
                    }
                }
            }

        }
        private void DetScore1(List<int> hand)
        {
            int winner;
            List<int> inSuit = new List<int>();
            int a; //>=
            int b; //<
            if (hand[0] < 13)
            {
                a = 0;
                b = 13;
            }
            else if (hand[0] < 26)
            {
                a = 13;
                b = 26;
            }
            else if (hand[0] < 39)
            {
                a = 26;
                b = 39;
            }
            else
            {
                a = 39;
                b = 52;
            }

            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i] >= a && hand[i] < b)
                {
                    inSuit.Add(1);
                }
                else
                {
                    inSuit.Add(0);
                }
            }
            List<int> temp = new List<int>();
            for (int i = 0; i < hand.Count; i++)
            {
                if (inSuit[i] == 0)
                {
                    temp.Add(-1);
                }
                else
                {
                    temp.Add(hand[i]);
                }
            }
            //now calling FindMaxInd(temp) will tell max ind in hand. Must convert this to player
            int maxIndHand = FindMaxInd(temp);
            //Console.WriteLine("Max ind: " + maxIndHand);
            int cp = (CurrentPlayer + 1) % 4;
            winner = (cp + maxIndHand)%4;
            Winner = winner;
            int points = 0;
            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i] >= 13 && hand[i] < 26)
                {
                    points += 1;
                }

                if (hand[i] == 11)
                {
                    points += 13;
                }
            }

            if (points != 26)
            {
                Scores[Winner] += points;
            }
            else
            {
                for (int i = 0; i < Scores.Count; i++)
                {
                    if (i != winner)
                    {
                        Scores[i] += 26;
                    }
                }
            }

        }
        private int FindMaxInd(List<int> ls)
        {
            int maxInd = 0;
            for (int i = 0; i < ls.Count; i++)
            {
                if (ls[i] > ls[maxInd])
                {
                    if ((ls[i] % 13 == 0))
                    {
                        return i;
                    }
                    maxInd = i;
                }
            }

            return maxInd;
        }


    }
        
        
    
}