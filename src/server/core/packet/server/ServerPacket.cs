using System.Net.Sockets;
using sharpcraft.server.core.types;

namespace sharpcraft.server.core.packet.server;

public abstract class ServerPacket : Packet
{
    public VarInt lenght { get; set; }
    public VarInt id { get; set; }
    public byte[] data { get; set; }
    
    public abstract void Decode(byte[] bytes);
    public abstract void Resolve(TcpClient tcpClient);
}