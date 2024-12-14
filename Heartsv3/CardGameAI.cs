namespace Heartsv3
{

    using System;
    using System.Collections.Generic;
    using System.Text.Json;

    public class CardGameAI
    {
        private List<int> hand;
        private List<int> table;
        public CardGameAI(List<int> h, List<int> t)
        {
            hand = h;
            table = t;

        }
        public string ConstructPayload()
        {
            // Create the payload as a dynamic object
            var payload = new
            {
                model = "gpt-4",
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content =
                            $"Game state: Your hand is {JsonSerializer.Serialize(hand)}. " +
                            $"The cards on the table are {JsonSerializer.Serialize(table)}." +
                            $"You need to return an integer corresponding to a playing card as explained below: " +
                            $"The rules are: Each card is represented as an integer from 0 to 51 inclusive." +
                            $" 0 to 12 inclusive are Spades running from Ace to King." +
                            $" Ace always highest. Similarly, cards " +
                            $"running from 13 to 25 inclusive are Hearts." +
                            $"Cards running from 26 to 38 are Diamonds" +
                            $"Cards running from 39 to 51 inclusive are Clubs" +
                            $"You must play a card of the same suit as the first player if possible." +
                            $"You must return integer!" +
                            $"If 40 is in your hand you must return 40!" +
                            $"You cannot play the same card as another player" +
                            $"You can only play something from your hand! " +
                            $"Enter the integer which represents the card!"
                    }
                },
                max_tokens = 2,
                temperature = 0.1
                //sometimes gives garbage responses
            };

            // Serialize the payload to JSON
            return JsonSerializer.Serialize(payload);
        }
    }
}
