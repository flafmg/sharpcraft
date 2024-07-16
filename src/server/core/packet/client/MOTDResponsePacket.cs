using System.Text;
using sharpcraft.server.core.packet.stream;

namespace sharpcraft.server.core.types.client;

public class MOTDResponsePacket : ClientPacket
{
    private const string Motd = "{\"version\":{\"name\":\"1.16.5\",\"protocol\":754},\"players\":{\"max\":100,\"online\":-1},\"description\":{\"text\":\"§dWhy everyone in IT is a furry?§r\"},\"favicon\": \"data:image/png;base64,%iconhash%\"}";
    public MOTDResponsePacket() : base(0){}
    
    public override void Encode()
    {
        string iconHash = GetImage();
        string finalMotd = Motd.Replace("%iconhash%", iconHash);
        
        PacketWritter pw = new PacketWritter();
        
        pw.WriteString(finalMotd);
        data = pw.ToArray();
    }
    
    public static string GetImage()
    {
        string imagePath = "server-icon.png";
        
        if (!File.Exists(imagePath))
        {
            return "";
        }

        byte[] imageBytes = File.ReadAllBytes(imagePath);
        string base64Image = Convert.ToBase64String(imageBytes);

        return base64Image;
    }
}