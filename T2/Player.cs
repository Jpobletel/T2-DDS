namespace T2;

public class Player
{
    private string _name { get; }
    private int _puntaje = 0;
    private List<Card> _hand = new List<Card>();
    private List<List<Card>> _graveyard = new List<List<Card>>();
    private int _escobas = 0;

    public Player(string name)
    {
        _name = name;
    }

    public List<Card> GetHand()
    {
        return _hand;
    }
    public List<List<Card>> GetGraveyard()
    {
        return _graveyard;
    }
    
    public void AddToGraveyard(List<Card> cardList)
    {
        _graveyard.Add(cardList);
    }
    
    public void AddToHand(Card card)
    {
        _hand.Add(card);
    }

    public void RemoveFromHand(Card card)
    {
        _hand.Remove(card);
    }

    public void GetHandView()
    {
        if (_hand.Count > 0)
        {
            int i = 0;
            Console.WriteLine("Mano de " + _name);
            foreach (var card in _hand)
            {
                Console.WriteLine("###########");
                card.GetSummary(i);
                i++;
            }
            Console.WriteLine("###########");
        }
    }

    public void AddEscoba()
    {
        _escobas++;
    }
}