using sharpcraft.server.core.types.packet.steam;
using static sharpcraft.server.core.configuration.ServerConfiguration;
namespace sharpcraft.server.core.types.packet.stream.clientbound.status;

public class StatusResponsePacket : Packet
{
    private string JsonReponse = @"
    {
        ""version"": {
            ""name"": ""%VersionName%"",
            ""protocol"": %Protocol%
        },
        ""players"": {
            ""max"": %MaxPlayers%,
            ""online"": -1
        },
        ""description"": {
            ""text"": ""%MoTD%""
        },
        ""favicon"": ""data:image/png;base64,%FavIconBase64%""
    }";

    public StatusResponsePacket()
    {
        JsonReponse = JsonReponse.Replace("%VersionName%", VersionName)
            .Replace("%Protocol%", ProtocolVersion.ToString())
            .Replace("%MaxPlayers%", MaxPlayers.ToString())
            .Replace("%MoTD%", MoTD.Replace("&", "§"))
            .Replace("%FavIconBase64%", FavIconBase64);

        id = new VarInt(0x00);
    }
    
    public override void Encode(PacketWriter packetWriter)
    {
        packetWriter.WriteString(JsonReponse);
        packetWriter.WriteToData();
    }
}