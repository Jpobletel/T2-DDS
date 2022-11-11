namespace T2;

public class Juego
{
    private List<Player> _players = new List<Player>();
    private Deck _mazo = new Deck();
    private Board _mesa = new Board();
    private SocketView _viewFirstPlayer = new SocketView( 4444 );
    private SocketView _viewSecondPlayer = new SocketView( 5555 );
    private List<SocketView> _viewsPlayer = new List<SocketView>(); 
    public void Jugar()
    {
        CreatePlayers();
        DealBoard();
        DealPlayers();
        Turnos();
        EndGameSummary();
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
            for (int i = 0; i < 4; i++) { _mesa.AddCardToBoard(_mazo.GetRandomCard()); }
        }
    }
    public void CreatePlayers()
    {
        _viewsPlayer.Add(_viewFirstPlayer);
        _viewsPlayer.Add(_viewSecondPlayer);
        _players.Add(new Player("J1"));
        _players.Add(new Player("J2"));

    }
    public void Turnos()
    {
        string ganador;
        bool win = false;
        while (!win)
        {
            int indexView = 0;
            _viewsPlayer[0].ShowBoard(_mesa);
            _viewsPlayer[1].ShowBoard(_mesa);
            foreach (var player in _players)
            {
                if (player.GetHand().Count == 0 && _mazo.deckList.Count > 0) { DealPlayers(); }
                PlayTurn(player, _viewsPlayer[indexView]);
                if (player.GetHand().Count == 0 && _mazo.deckList.Count == 0) { win = true; }
            }
        }
    }

    public void PlayTurn(Player player, SocketView view)
    {
        view.HandView(player);
        int option = view.GetInput(player.GetHand().Count); 
        _mesa.AddCardToBoard(player.GetHand()[option]);
        player.RemoveFromHand(player.GetHand()[option]);
        PlayOptions(GetCombination(_mesa.GetBoard()), player, view);
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
                if (str[j] == '1') { availablePlays.Add(list[j]); }
            }
            if (GetSum(availablePlays)) { optionList.Add(availablePlays); }
        }

        int index = 0;
        foreach (var cardList in optionList)
        {
            Console.WriteLine("[" + index + "]");
            foreach (var card in cardList) { Console.WriteLine("    " + card.GetFace() + " de " + card.GetSuit()); }
            index++;
        }

        return optionList;
    }
    public bool GetSum(List<Card> cards)
    {
        int totalValue = 0;
        foreach (var card in cards)
        { totalValue = + card.GetValue(); }
        if (totalValue == 15) { return true; }
        return false;
    }
    public void PlayOptions(List<List<Card>> optionList, Player player, SocketView view )
    {
        if (optionList.Count == 0) { view.NoCombinations(); }
        
        else if (optionList.Count == 1)
        {
            foreach (var card in optionList[0])
            {
                player.AddToGraveyard(card);
                _mesa.RemoveCardFromBoard(card);
            }

            CheckEscoba(player, view);
        }

        else
        {
            int input = view.GetInput(optionList.Count);
            foreach (var card in optionList[input])
            {
                player.AddToGraveyard(card);
                _mesa.RemoveCardFromBoard(card);
            }
            CheckEscoba(player, view);
        }
    }
    public void CheckEscoba(Player player, SocketView view)
    {
        if (_mesa.GetBoard().Count==0)
        {
            view.Escoba();
            player.AddEscoba();
        }
    }

    public void EndGameSummary()
    {
        Dictionary<string, int> playerOneSum = _players[0].GetSummary();
        Dictionary<string, int> playerTwoSum = _players[1].GetSummary();
        Comparator(playerOneSum, playerTwoSum, "Oros");
        Comparator(playerOneSum, playerTwoSum, "Sietes");
        Comparator(playerOneSum, playerTwoSum, "TotalCartas");
    }

    public void Comparator(Dictionary<string, int> playerOneSum, Dictionary<string, int> playerTwoSum, string key)
    {

        if (playerOneSum[key] == playerTwoSum[key])
        {
            foreach (var player in _players)
            {
                player.AddPoint();
            }
        }
        else if (playerOneSum[key] > playerTwoSum[key])
        {
            _players[0].AddPoint();
        }
        else
        {
            _players[1].AddPoint();
        }
    }
}
