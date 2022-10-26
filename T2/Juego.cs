namespace T2;

public class Juego
{
    private List<Player> _players = new List<Player>();
    private Deck _mazo = new Deck();
    private Board _mesa = new Board();
    public void Jugar()
    {
        CreatePlayers();
        DealBoard();
        DealPlayers();
        Console.WriteLine("Comienza el Juego");
        Turnos();
    }

    public void DealPlayers()
    {
        if (_mazo.deckList.Count >= 6)
        {
            for (int i = 0; i < 3; i++)
            {
                _players[0].AddToHand(_mazo.GetRandomCard());
                _players[1].AddToHand(_mazo.GetRandomCard());
            }
        }
    }

    public void DealBoard()
    {
        if (_mazo.deckList.Count >= 4)
        {
            for (int i = 0; i < 4; i++)
            {
                _mesa.AddCardToBoard(_mazo.GetRandomCard());
            }
        }
    }
    public void CreatePlayers()
    {
        _players.Add(new Player("J1"));
        _players.Add(new Player("J2"));
        Console.WriteLine("Jugadores Creados c:");
        
    }

    public void Turnos()
    {
        string ganador;
        bool win = false;
        while (!win)
        {
            foreach (var player in _players)
            {
                if (player.GetHand().Count == 0 && _mazo.deckList.Count > 0)
                {
                    DealPlayers();
                }
                _mesa.ShowBoard();
                player.GetHandView();
                int option = GetInput(player.GetHand().Count);
                _mesa.AddCardToBoard(player.GetHand()[option]);
                player.RemoveFromHand(player.GetHand()[option]);
                GetCombination(_mesa.GetBoard());
                if (player.GetHand().Count == 0 && _mazo.deckList.Count == 0)
                {
                    win = true;
                }

            }
        }
    }

    public int GetInput(int i)
    {
        int inputUsuario = Convert.ToInt32(Console.ReadLine());
        while (inputUsuario < 0 || inputUsuario > i)
        {
            Console.WriteLine("Bruh elige un numero valido");
            inputUsuario = Convert.ToInt32(Console.ReadLine());
        }

        return inputUsuario;
    }
    //https://stackoverflow.com/questions/7802822/all-possible-combinations-of-a-list-of-values
    public void GetCombination(List<Card> list)
    {
        List<List<Card>> optionList = new List<List<Card>>();
        double count = Math.Pow(2, list.Count);
        for (int i = 1; i <= count - 1; i++)
        {
            List<Card> availablePlays = new List<Card>();
            string str = Convert.ToString(i, 2).PadLeft(list.Count, '0');
            
            for (int j = 0; j < str.Length; j++)
            {
                if (str[j] == '1')
                {
                    availablePlays.Add(list[j]);
                }
            }
            if (GetSum(availablePlays))
            {
                optionList.Add(availablePlays);
            }
        }

        foreach (var cardList in optionList)
        {
            Console.WriteLine(cardList);
        }
    }

    public bool GetSum(List<Card> cards)
    {
        int totalValue = 0;
        foreach (var card in cards)
        {
            totalValue = totalValue + card.GetValue();
        }

        if (totalValue == 15)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
