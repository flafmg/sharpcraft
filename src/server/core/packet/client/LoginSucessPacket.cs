using sharpcraft.server.core.packet.stream;

namespace sharpcraft.server.core.types.client;

public class LoginSuccessPacket : ClientPacket
{
    private Guid uuid;
    private string username;

    public LoginSuccessPacket(Guid uuid, string username) : base(0x02)
    {
        this.uuid = uuid;
        this.username = username;
    }

    public override void Encode()
    {
        PacketWritter pw = new PacketWritter();
            
        pw.WriteUuid(uuid);  
        pw.WriteString(username);
        
        data = pw.ToArray();
    }
}
