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
}
