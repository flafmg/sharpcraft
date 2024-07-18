using System.Net.Sockets;
using System.Text;
using sharpcraft.server.core.types.packet.steam;

namespace sharpcraft.server.core.types;

public abstract class Packet
{
    public VarInt lenght { get; set; }
    public VarInt id { get; set; }
    public byte[] data { get; set; }


    public virtual void Decode(PacketReader packetReader){ }

    public virtual void Encode(PacketWriter packetWriter) { }

    public virtual void Resolve(TcpClient client) { }

    public void Send(TcpClient client)
    {
        Encode(new PacketWriter(this));
        lenght = new VarInt(id.GetSize() + data.Length);
        
        byte[] rawData = new byte[lenght.Value + lenght.GetSize()];
        lenght.bytes.CopyTo(rawData, 0);
        id.bytes.CopyTo(rawData, lenght.GetSize());
        data.CopyTo(rawData, id.GetSize() + lenght.GetSize());

        NetworkStream networkStream = client.GetStream();
        networkStream.Write(rawData);
    }
    public void Receive(byte[] rawData)
    {
        data = rawData;
        
        PacketReader packetReader = new PacketReader(this);
        lenght = packetReader.ReadVarInt();
        id = packetReader.ReadVarInt();

        int offset = lenght.GetSize() + id.GetSize();

        data = new byte[lenght.Value];
        Array.Copy(rawData, offset, data, 0, lenght.Value);

        Console.WriteLine($"RECIEVED: \n" +
                          $"LEN: {lenght.Value}, \n" +
                          $"ID: {id.Value}, \n" +
                          $"DATA: {BitConverter.ToString(data).Replace("-", " ")}, \n" +
                          $"STR: {Encoding.UTF8.GetString(data)}");
        
        Decode(new PacketReader(this));
    }
}