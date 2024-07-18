using sharpcraft.server.core.types.packet.steam;

namespace sharpcraft.server.core.types.packet.stream.clientbound.status;

public class PingReponsePacket : Packet
{
    private long PayLoad;

    public PingReponsePacket(long payLoad)
    {
        this.PayLoad = payLoad;

        id = new VarInt(0x01);
    }
    
    public override void Encode(PacketWriter packetWriter)
    {
        packetWriter.WriteLong(PayLoad);
        packetWriter.WriteToData();
    }
}