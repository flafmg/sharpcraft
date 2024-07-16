using sharpcraft.server;

namespace sharpcraft;

public class SharpCraft
{
    public static void Main(string[] args)
    {
        Server server = new Server("127.0.0.1", 25565);
        Console.WriteLine("starting server");
        server.Start();
    }
}