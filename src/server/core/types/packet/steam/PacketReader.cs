using System.Text;
using sharpcraft.server.core.types;

namespace sharpcraft.server.core.types.packet.steam;

public class PacketReader
{
    private byte[] Data;
    public int Index { get; private set; }
    
    public PacketReader(Packet packet)
    {
        this.Data = packet.data;
    }

    public PacketReader(byte[] data)
    {
        this.Data = data;
    }
    public VarInt ReadVarInt()
    {
        VarInt value = new VarInt(Data, Index);
        Index += value.GetSize();
        return value;
    }

    public VarLong ReadVarLong()
    {
        VarLong value = new VarLong(Data, Index);
        Index += value.GetSize();
        return value;
    }

    public string ReadString()
    {
        VarInt length = ReadVarInt();
        string value = Encoding.UTF8.GetString(Data, Index, length.Value);
        Index += length.Value;
        return value;
    }

    public byte ReadByte()
    {
        return Data[Index++];
    }

    public bool ReadBoolean()
    {
        return Data[Index++] != 0;
    }

    public short ReadShort()
    {
        short value = BitConverter.ToInt16(Data, Index);
        Index += 2;
        return value;
    }

    public int ReadInt()
    {
        int value = BitConverter.ToInt32(Data, Index);
        Index += 4;
        return value;
    }

    public long ReadLong()
    {
        long value = BitConverter.ToInt64(Data, Index);
        Index += 8;
        return value;
    }
    
    public Guid ReadUuid()
    {
        byte[] uuidBytes = new byte[16];
        Array.Copy(Data, Index, uuidBytes, 0, 16);
        Index += 16;
        return new Guid(uuidBytes);
    }
}