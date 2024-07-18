using System.Net.Sockets;
using sharpcraft.server.core.types;
using sharpcraft.server.core.types.packet.steam;
using sharpcraft.server.core.types.packet.stream.clientbound.status;

namespace sharpcraft.server.core.packet.serverbound.status;

public class PingRequestPacket : Packet
{
    private long PayLoad;

    public override void Decode(PacketReader packetReader)
    {
        PayLoad = packetReader.ReadLong();
    }

    public override void Resolve(TcpClient client)
    {
        PingReponsePacket pingReponsePacket = new PingReponsePacket(PayLoad);
        pingReponsePacket.Send(client);
    }
}