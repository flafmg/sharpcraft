using System.Net;
using System.Net.Sockets;
using System.Text;
using sharpcraft.server.core.packet;
using sharpcraft.server.core.packet.server;
using sharpcraft.server.core.types;

namespace sharpcraft.server;

public class Server
{
    private TcpListener tcpListener;
    private PacketManager packetManager;
    public bool isRunning { private set; get; }

    public Server(string ipAdress, int port)
    {
        IPAddress ip = IPAddress.Parse(ipAdress);
        tcpListener = new TcpListener(ip, port);
        packetManager = new PacketManager();
    }

    public void Start()
    {
        tcpListener.Start();
        byte[] buffer = new byte[1024];

        while (true)
        {
            TcpClient client = tcpListener.AcceptTcpClient();
            HandleClient(client, ref buffer);
        }
    }

    public void HandleClient(TcpClient client, ref byte[] buffer)
    {
        NetworkStream stream = client.GetStream();

        try
        {
            while (client.Connected)
            {
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                {
                    break;
                }
                
                packetManager.HandlePacket(client, buffer);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error handling client: " + ex.StackTrace);
        }
        finally
        {
            stream.Close();
            client.Close();
            PacketManager.CurrentState = State.HANDSHAKE;
        }
    }
    
}