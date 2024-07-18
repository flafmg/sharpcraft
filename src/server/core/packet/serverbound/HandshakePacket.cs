using System.Net.Sockets;
using sharpcraft.server.core.types;
using sharpcraft.server.core.types.packet.steam;
using sharpcraft.server.core.types.packet.stream;
using sharpcraft.server.core.types.packet.stream.clientbound.status;

namespace sharpcraft.server.core.packet.serverbound;

public class HandshakePacket : Packet
{
    private VarInt ProtocolVersion;
    private string ServerAddress;
    private ushort ServerPort;
    private VarInt NextState;
    
    public override void Decode(PacketReader packetReader)
    {
        ProtocolVersion = packetReader.ReadVarInt();
        ServerAddress = packetReader.ReadString();
        ServerPort = (ushort)packetReader.ReadShort();
        NextState = packetReader.ReadVarInt();
    }

    public override void Resolve(TcpClient client)
    {
        PacketManager.CurrentPacketState = (PacketState)Enum.ToObject(typeof(PacketState), NextState.Value);
    }
}