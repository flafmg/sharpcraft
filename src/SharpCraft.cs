using sharpcraft.server;

namespace sharpcraft;

public class SharpCraft
{
    public static void Main(string[] args)
    {
        Server server = new Server();
        Console.WriteLine("Starting server...");
        server.Start();
    }
}