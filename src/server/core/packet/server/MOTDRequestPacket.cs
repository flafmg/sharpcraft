using System.Net.Sockets;
using sharpcraft.server.core.types.client;

namespace sharpcraft.server.core.packet.server;

public class MOTDRequestPacket : ServerPacket
{
    public override void Decode(byte[] bytes)
    {
        
    }

    public override void Resolve(TcpClient tcpClient)
    {
        MOTDResponsePacket motdResponsePacket = new MOTDResponsePacket();
        motdResponsePacket.Send(tcpClient);
    }
}