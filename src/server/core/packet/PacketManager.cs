
using System.Net.Sockets;
using sharpcraft.server.core.packet.serverbound;
using sharpcraft.server.core.packet.serverbound.status;
using sharpcraft.server.core.types.packet.steam;
using static sharpcraft.server.core.types.packet.stream.PacketState;
namespace sharpcraft.server.core.types.packet.stream;

public class PacketManager
{
    public static PacketState CurrentPacketState = PacketState.Handshake;
    private readonly Dictionary<PacketState, Dictionary<int, Packet>> PacketHandler;
    public PacketManager()
    {
        PacketHandler = new Dictionary<PacketState, Dictionary<int, Packet>>();
        
        PacketHandler.Add(Handshake, new Dictionary<int, Packet>());
        PacketHandler.Add(Status, new Dictionary<int, Packet>());
        PacketHandler.Add(Login, new Dictionary<int, Packet>());
        PacketHandler.Add(Transfer, new Dictionary<int, Packet>());
        PacketHandler.Add(Play, new Dictionary<int, Packet>());
        
        PacketHandler[Handshake].Add(0x00, new HandshakePacket());
        
        PacketHandler[Status].Add(0x00, new StatusRequestPacket());
        PacketHandler[Status].Add(0x01, new PingRequestPacket());
    }

    public void HandlePacket(byte[] rawData, TcpClient client)
    {
        int packetID = GetIDFromRawData(rawData);
        Console.WriteLine($"RECIVING ID: {packetID}, STATE: {CurrentPacketState.ToString()}");
        
        if (PacketHandler[CurrentPacketState].TryGetValue(packetID, out Packet packet))
        {
            packet.Receive(rawData);
            packet.Resolve(client);
        }
    }

    private int GetIDFromRawData(byte[] rawData)
    {
        PacketReader packetReader = new PacketReader(rawData);
        packetReader.ReadVarInt();
        return packetReader.ReadVarInt().Value;
    }
}