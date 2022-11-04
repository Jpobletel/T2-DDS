namespace T2;

public abstract class View
{
    protected abstract void Write(string message);
    
    protected abstract string ReadLine();
    
    public virtual void Close() {}
    
    protected void WriteLine(string message) => Write(message + "\n");
    
    protected void WriteLine() => WriteLine("");
    
    public void Pause() => ReadLine();

    public void ShowBoard(Board board)
    {
        List<Card> boardList = board.GetBoard();
        if (boardList.Count > 0)
        {
            int i = 0;
            WriteLine("### MESA ###");
            foreach (var card in boardList)
            {
                WriteLine("###########");
                WriteLine(card.GetSummary(i));
                i++;
            }
            WriteLine("###########");
            WriteLine("-----------------------------------");
        }
        else
        {
            WriteLine("############");
            WriteLine("### MESA ###");
            WriteLine("### VACIA ###");
            WriteLine("############");
            WriteLine("-----------------------------------");
        }
    }

    public void HandView(Player player)
    {
        List<Card> hand = player.GetHand();
        if (hand.Count > 0)
        {
            int i = 0;
            foreach (var card in hand)
            {
                WriteLine("###########");
                WriteLine(card.GetSummary(i));
                i++;
            }
            WriteLine("###########");
        }
    }

    public int GetPlay(int numerOfCards)
    {
        
    }
    
    public int GetInput(int supLimit)
    {
        int inputInt;
        var inputUsuario = Console.ReadLine();
        bool success = int.TryParse(inputUsuario, out inputInt);
        while (!success || inputInt < 0 || inputInt >= supLimit)
        {
            inputUsuario  = Console.ReadLine();
            success = int.TryParse(inputUsuario, out inputInt);
        }

        return inputInt;
    }
}