using System.Net.Sockets;
using sharpcraft.server.core.packet.stream;
using sharpcraft.server.core.types;
using sharpcraft.server.core.types.client;

namespace sharpcraft.server.core.packet.server;

public class PingRequestPacket : ServerPacket
{
    public long PayLoad { private set; get; }

    public PingRequestPacket(GenericServerPacket gnsp)
    {
        lenght = gnsp.lenght;
        id = gnsp.id;
        data = gnsp.data;
    }
    
    public override void Decode(byte[] bytes)
    {
        PacketReader pr = new PacketReader(bytes);
        PayLoad = pr.ReadLong();
    }

    public override void Resolve(TcpClient tcpClient)
    {
        PingResponsePacket pingResponsePacket = new PingResponsePacket(PayLoad);
        pingResponsePacket.Send(tcpClient);
    }
}