namespace T2;

public class Card
{
    private string _face { get; }
    private string _suit { get; }
    private int _value { get; set; }

    public Card(string face, string suit)
    {
        _face = face;
        _suit = suit;
        SetValue();
    }

    public void SetValue()
    {
        int value;
        bool isNumeric = int.TryParse(_face, out value);
        if (isNumeric)
        {
            _value = value;
        }
        else
        {
            if (_face == "Sota")
            {
                _value = 8;
            }
            else if (_face == "Caballo")
            {
                _value = 9;
            }
            else if (_face == "Rey")
            {
                _value = 10;
            }
        }
    }

    public string GetFace()
    {
        return _face;
    }
    public string GetSuit()
    {
        return _suit;
    }
    public int GetValue()
    {
        return _value;
    }

    public void GetSum(int i)
    {
        Console.WriteLine("[" + i + "]: " + _face + " de " + _suit);
    }

}