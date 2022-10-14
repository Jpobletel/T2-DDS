namespace T2;

public class Board
{
    private List<Card> _board = new List<Card>();

    public void AddCardToBoard(Card card)
    {
        _board.Add(card);
    }

    public void ShowBoard()
    {
        if (_board.Count > 0)
        {
            int i = 0;
            Console.WriteLine("### MESA ###");
            foreach (var card in _board)
            {
                Console.WriteLine("###########");
                card.GetSum(i);
                i++;
            }
            Console.WriteLine("###########");
        }
    }
}