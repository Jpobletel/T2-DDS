namespace T2;

public class Board
{
    private List<Card> _board = new List<Card>();

    public void AddCardToBoard(Card card)
    {
        _board.Add(card);
    }
    public void RemoveCardFromBoard(Card card)
    {
        _board.Remove(card);
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
                card.GetSummary(i);
                i++;
            }
            Console.WriteLine("###########");
            Console.WriteLine("-----------------------------------");
        }
        else
        {
            Console.WriteLine("############");
            Console.WriteLine("### MESA ###");
            Console.WriteLine("### VACIA ###");
            Console.WriteLine("############");
            Console.WriteLine("-----------------------------------");
        }
    }

    public List<Card> GetBoard()
    {
        return _board;
    }

}