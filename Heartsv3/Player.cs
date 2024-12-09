using System;
using System.Collections.Generic;

namespace Heartsv3
{
    public class Player
    {
        private List<int> Hand;

        public Player()
        {
            Hand = new List<int>();
        }

        public int move(List<int> hand)
        {
            //check if have card 40 for 2C
            if (hand.Count == 0)
            {
                if (Hand.Contains(40))
                {
                    Hand.Remove(40);
                    return 40;
                }
                else
                {
                    List<int> spades = new List<int>();
                    List<int> hearts = new List<int>();
                    List<int> diamonds = new List<int>();
                    List<int> clubs = new List<int>();
                    for (int i = 0; i < Hand.Count; i++)
                    {
                        if (Hand[i] < 13)
                        {
                            spades.Add(Hand[i]);
                        }
                        else if (Hand[i] < 26)
                        {
                            hearts.Add(Hand[i]);
                        }
                        else if (Hand[i] < 39)
                        {
                            diamonds.Add(Hand[i]);
                        }
                        else
                        {
                            clubs.Add(Hand[i]);
                        }
                    }

                    int toPlay;
                    if (spades.Count != 0)
                    {
                        toPlay = FindMax(spades);
                    }
                    else if (diamonds.Count != 0)
                    {
                        toPlay = FindMax(diamonds);
                    }
                    else if (clubs.Count != 0)
                    {
                        toPlay = FindMax(clubs);
                    }
                    else if (hearts.Count != 0)
                    {
                        toPlay = FindMax(hearts);
                    }
                    else
                    {
                        toPlay = FindMax(Hand);
                    }

                    Hand.Remove(toPlay);
                    return toPlay;
                }
            }
            else
            {
                //det suit of first card
                string suit;
                if (hand[0] < 13)
                {
                    suit = "S";
                }
                else if (hand[0] < 26)
                {
                    suit = "H";
                }
                else if (hand[0] < 39)
                {
                    suit = "D";
                }
                else
                {
                    suit = "C";
                }

                List<int> inSuit = new List<int>();
                for (int i = 0; i < Hand.Count; i++)
                {
                    if (suit == "S")
                    {
                        if (Hand[i] < 13)
                        {
                            inSuit.Add(Hand[i]);
                        }
                    }
                    if (suit == "H")
                    {
                        if (Hand[i] < 26)
                        {
                            inSuit.Add(Hand[i]);
                        }
                    }
                    if (suit == "D")
                    {
                        if (Hand[i] < 39)
                        {
                            inSuit.Add(Hand[i]);
                        }
                    }
                    if (suit == "C")
                    {
                        if (Hand[i] < 52)
                        {
                            inSuit.Add(Hand[i]);
                        }
                    }
                }

                int toPlay;//must be returned at end. also must do Hand.Remove(toPlay)
                if (inSuit.Count == 0)
                {
                    //play heart
                    List<int> hearts = new List<int>();
                    for (int i = 0; i < Hand.Count; i++)
                    {
                        if (Hand[i] < 26)
                        {
                            hearts.Add(Hand[i]);
                        }
                    }

                    if (hearts.Count != 0)
                    {
                        toPlay = FindMax(hearts);
                    }
                    else
                    {
                        toPlay = FindMax(Hand);
                    }
                }
                else
                {
                    toPlay = FindMax(inSuit);
                }

                Hand.Remove(toPlay);
                return toPlay;
            }
            
        }

        public List<int> toPass()
        {
            Random rng = new Random();
            List<int> selectedElements = new List<int>();
            int count = Hand.Count;

            while (selectedElements.Count < 3)
            {
                int randomIndex = rng.Next(count); // Random index between 0 and count - 1
                //int element = Hand[randomIndex];
                int element = Hand[randomIndex];

                if (!selectedElements.Contains(element))
                {
                    selectedElements.Add(element);
                }
                
            }
            return selectedElements;
           
        }

        private int FindMax(List<int> ls)
        {
            int maxInd = 0;
            for (int i = 0; i < ls.Count; i++)
            {
                if (ls[i] > ls[maxInd])
                {
                    maxInd = i;
                }
            }

            return ls[maxInd];
        }

        public void AddToHand(List<int> h)
        {
            for (int i = 0; i < h.Count; i++)
            {
                Hand.Add(h[i]);
            }
        }

        public void SetHand(List<int> h)
        {
            Hand = h;
        }

        public List<int> GetHand()
        {
            return Hand;
        }

        public void UpdateHand(int c)
        {
            //Console.WriteLine("Removing " + c);
            Hand.Remove(c);
        }
    }
}