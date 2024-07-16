using sharpcraft.server.core.packet.stream;

namespace sharpcraft.server.core.types.client;

public class PingResponsePacket : ClientPacket
{
    private long Payload;

    public PingResponsePacket(long PayLoad) : base(0x01)
    {
        this.Payload = Payload;
    }

    public override void Encode()
    {
        PacketWritter pw = new PacketWritter();
        pw.WriteLong(Payload);
        data = pw.ToArray();
    }
}