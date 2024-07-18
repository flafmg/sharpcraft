using System.Net;
using System.Net.Sockets;
using System.Text;
using sharpcraft.server.core.configuration;
using sharpcraft.server.core.packet.serverbound;
using sharpcraft.server.core.types;
using sharpcraft.server.core.types.packet.steam;
using sharpcraft.server.core.types.packet.stream;


namespace sharpcraft.server;

 public class Server
{
    private readonly TcpListener tcpListener;
    private readonly PacketManager packetManager;
    private readonly byte[] buffer = new byte[1024];
    public bool isRunning { get; private set; }

    public Server()
    {
        ServerConfiguration.LoadConfig();
        
        string ipAddress = ServerConfiguration.ServerAddress;
        ushort port = ServerConfiguration.ServerPort;

        IPAddress ip = IPAddress.Parse(ipAddress);
        tcpListener = new TcpListener(ip, port);
        packetManager = new PacketManager();
    }

    public void Start()
    {
        tcpListener.Start();
        isRunning = true;
        Console.WriteLine("Server started, waiting for clients...");

        AcceptClients();
    }

    private void AcceptClients()
    {
        while (isRunning)
        {
            try
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                Task.Run(() => HandleClient(client));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accepting client: {ex.Message}{ex.StackTrace}");
            }
        }
    }

    private void HandleClient(TcpClient client)
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
                packetManager.HandlePacket(buffer, client);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error handling client: {ex.Message}{ex.StackTrace}");
        }
        finally
        {
            stream.Close();
            client.Close();
            PacketManager.CurrentPacketState = PacketState.Handshake;
        }
    }

    public void Stop()
    {
        isRunning = false;
        tcpListener.Stop();
        Console.WriteLine("Server stopped.");
    }
}