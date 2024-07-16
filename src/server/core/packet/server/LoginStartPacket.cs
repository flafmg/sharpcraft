using System.Net.Sockets;
using System.Text;
using sharpcraft.server.core.packet.stream;
using sharpcraft.server.core.types;
using sharpcraft.server.core.types.client;
using sharpcraft.server.core.util;

namespace sharpcraft.server.core.packet.server;

public class LoginStartPacket : ServerPacket
{
    public string Username { get; private set; }

    public LoginStartPacket(GenericServerPacket genericServerPacket)
    {
        lenght = genericServerPacket.lenght;
        id = genericServerPacket.id;
        data = genericServerPacket.data;
        
        Decode(data);
    }

    public override void Decode(byte[] bytes)
    {
        PacketReader pr = new PacketReader(bytes);
        Username = pr.ReadString();
    }

    public override void Resolve(TcpClient tcpClient)
    {
        Guid playerUUID = Util.GenerateOfflineUUID(Username);
        
        LoginSuccessPacket loginSuccessPacket = new LoginSuccessPacket(playerUUID, Username);
        loginSuccessPacket.Send(tcpClient);
    }
}