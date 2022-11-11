using System.Net;
using System.Net.Sockets;


namespace T2;

public class SocketView : View
{
    public string Name = "J1";
    private TcpListener _listener;
    private TcpClient _client;
    private StreamReader _reader;
    private StreamWriter _writer;
    
    public SocketView (int port)
    {
        
        _listener = new TcpListener(IPAddress.Loopback, port);
        _listener.Start();
        _client = _listener.AcceptTcpClient();
        _reader = new StreamReader(_client.GetStream());
        _writer = new StreamWriter(_client.GetStream());
    }


    protected override void Write(string mensaje)
    {
        _writer.Write(mensaje);
        _writer.Flush();
    }

    protected override string ReadLine()
    {
        WriteLine("[INGRESE INPUT]");
        return _reader.ReadLine();
    }

    public override void Close()
    {
        WriteLine("[FIN JUEGO]");
        _client.Close();
        _listener.Stop();
    }
}