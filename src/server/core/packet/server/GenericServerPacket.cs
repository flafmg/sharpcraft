using System.Net.Sockets;
using System.Text;
using sharpcraft.server.core.packet.server;
using sharpcraft.server.core.packet.stream;

namespace sharpcraft.server.core.types;

public class GenericServerPacket : ServerPacket
{
    public GenericServerPacket(byte[] bytes)
    {
        Decode(bytes);
    }
    
    public override void Decode(byte[] bytes)
    {
        PacketReader pr = new PacketReader(bytes);
        
        lenght = pr.ReadVarInt();
        id = pr.ReadVarInt();

        data = new byte[lenght.value - id.GetSize()];
        Array.Copy(bytes, pr.index, data, 0, data.Length);
        
        Console.WriteLine($"Receiving packet: \n LEN: {lenght.value} \n ID: {id.value} \n STR DATA: {Encoding.UTF8.GetString(data).Replace("\n", "")}" +
                          $" \n RW DATA: {BitConverter.ToString(bytes, 0, lenght.value).Replace("-", " ")}");

    }
    public override void Resolve(TcpClient tcpClient)
    {
        throw new NotImplementedException();
    }
}