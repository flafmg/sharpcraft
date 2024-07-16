using System.Net.Sockets;
using System.Text;
using sharpcraft.server.core.packet.stream;
using sharpcraft.server.core.types;
using sharpcraft.server.core.types.client;

namespace sharpcraft.server.core.packet.server;

public class HandshakePacket : ServerPacket
{
    
    public VarInt protocol { get; private set; }
    public string serverAddress { get; private set; }
    public ushort serverPort { get; private set; }
    public VarInt nextState { get; private set; }

    private State _nextState;
    public HandshakePacket(GenericServerPacket genericServerPacket)
    {
        lenght = genericServerPacket.lenght;
        id = genericServerPacket.id;
        data = genericServerPacket.data;
        
        Decode(data);

        switch (nextState.value)
        {
            case 0:
                _nextState = State.HANDSHAKE;
                break;
            case 1:
                _nextState = State.STATUS;
                break;
            case 2:
                _nextState = State.LOGIN;
                break;
            case 3:
                _nextState = State.PLAY;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        PacketManager.CurrentState = _nextState;
    }

    public override void Decode(byte[] bytes)
    {
        PacketReader pr = new PacketReader(bytes);
        
        protocol = pr.ReadVarInt();
        serverAddress = pr.ReadString();
        serverPort = (ushort)pr.ReadShort();
        nextState = pr.ReadVarInt();
    }

    public override void Resolve(TcpClient tcpClient)
    { }
}