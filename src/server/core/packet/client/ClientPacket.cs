using System.Net.Sockets;
using System.Text;
using sharpcraft.server.core.packet.stream;

namespace sharpcraft.server.core.types.client;

public abstract class ClientPacket : Packet
{
    public VarInt lenght { get; set; }
    public VarInt id { get; set;  }
    public byte[] data { get; set;  }
    public ClientPacket(int id)
    {
        this.lenght = new VarInt(0);
        this.id = new VarInt(id);
        this.data = new byte[0];
    }
    
    public abstract void Encode();

    public void Send(TcpClient tcpClient)
    {
        Encode();
        lenght = new VarInt(data.Length + id.GetSize());

        PacketWritter pw = new PacketWritter();
        pw.WriteVarInt(lenght.value);
        pw.WriteVarInt(id);
        pw.WriteByteArray(data);

        byte[] responseData = pw.ToArray();
        
        NetworkStream stream = tcpClient.GetStream();
        stream.Write(responseData, 0, responseData.Length);
        
        Console.WriteLine($"Sending packet: \n LEN: {lenght.value} \n ID: {id.value} \n STR DATA: {Encoding.UTF8.GetString(data).Replace("\n", "")}" +
                          $" \n RW DATA: {BitConverter.ToString(responseData, 0, responseData.Length).Replace("-", " ")}");

    }
}