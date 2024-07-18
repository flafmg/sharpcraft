using System.Net.Sockets;
using sharpcraft.server.core.types;
using sharpcraft.server.core.types.packet.stream.clientbound.status;

namespace sharpcraft.server.core.packet.serverbound.status;

public class StatusRequestPacket : Packet
{
    public override void Resolve(TcpClient client)
    {
        StatusResponsePacket statusResponsePacket = new StatusResponsePacket();
        statusResponsePacket.Send(client);
    }
}