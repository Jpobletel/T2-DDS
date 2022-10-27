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
                PlayOptions(GetCombination(_mesa.GetBoard()), player); 
                
                if (player.GetHand().Count == 0 && _mazo.deckList.Count == 0)
                {
                    win = true;
                }

            }
        }
    }

    public int GetInput(int i)
    {
        int inputInt;
        var inputUsuario = Console.ReadLine();
        bool success = int.TryParse(inputUsuario, out inputInt);
        while (!success || inputInt < 0 || inputInt >= i)
        {
            inputUsuario  = Console.ReadLine();
            success = int.TryParse(inputUsuario, out inputInt);
        }

        return inputInt;
    }
    //https://stackoverflow.com/questions/7802822/all-possible-combinations-of-a-list-of-values
    public List<List<Card>> GetCombination(List<Card> list)
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

        int index = 0;
        foreach (var cardList in optionList)
        {
            Console.WriteLine("[" + index + "]");
            foreach (var card in cardList)
            {
                Console.WriteLine("    " + card.GetFace() + " de " + card.GetSuit());
            }
            index++;
        }

        return optionList;
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

    public void PlayOptions(List<List<Card>> optionList, Player player)
    {
        if (optionList.Count == 0)
        {
            Console.WriteLine("No hay combinaciones :/");
        }
        
        else if (optionList.Count == 1)
        {
            player.AddToGraveyard(optionList[0]);
            foreach (var card in optionList[0])
            {
                _mesa.RemoveCardFromBoard(card);
            }

            CheckEscoba(player);
        }

        else
        {
            int input = GetInput(optionList.Count);
            player.AddToGraveyard(optionList[input]);
            foreach (var card in optionList[input])
            {
                _mesa.RemoveCardFromBoard(card);
            }
            CheckEscoba(player);
        }
    }

    public void CheckEscoba(Player player)
    {
        if (_mesa.GetBoard().Count==0)
        {
            Console.WriteLine("ESCOBAAA WOAAAAAAAAA!");
            player.AddEscoba();
        }
    }
}
