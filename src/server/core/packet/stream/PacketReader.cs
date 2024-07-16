using System.Text;
using sharpcraft.server.core.types;

namespace sharpcraft.server.core.packet.stream;

public class PacketReader
{
    private byte[] data;
    public int index { get; private set; }
    
    public PacketReader(byte[] data, int index = 0)
    {
        this.data = data;
        this.index = index;
    }
    public VarInt ReadVarInt()
    {
        VarInt value = new VarInt(data, index);
        index += value.GetSize();
        return value;
    }

    public VarLong ReadVarLong()
    {
        VarLong value = new VarLong(data, index);
        index += value.GetSize();
        return value;
    }

    public string ReadString()
    {
        VarInt length = ReadVarInt();
        string value = Encoding.UTF8.GetString(data, index, length.value);
        index += length.value;
        return value;
    }

    public byte ReadByte()
    {
        return data[index++];
    }

    public bool ReadBoolean()
    {
        return data[index++] != 0;
    }

    public short ReadShort()
    {
        short value = BitConverter.ToInt16(data, index);
        index += 2;
        return value;
    }

    public int ReadInt()
    {
        int value = BitConverter.ToInt32(data, index);
        index += 4;
        return value;
    }

    public long ReadLong()
    {
        long value = BitConverter.ToInt64(data, index);
        index += 8;
        return value;
    }
    
    public Guid ReadUuid()
    {
        byte[] uuidBytes = new byte[16];
        Array.Copy(data, index, uuidBytes, 0, 16);
        index += 16;
        return new Guid(uuidBytes);
    }
}