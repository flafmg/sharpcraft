using System.Text;
using sharpcraft.server.core.types;
using sharpcraft.server.core.types.nbt;

namespace sharpcraft.server.core.types.packet.steam;

public class PacketWriter
{
    private List<byte> data;
    private Packet packet;
    public PacketWriter(Packet packet)
    {
        this.packet = packet;
        data = new List<byte>();
    }
    
    public void WriteVarInt(VarInt value)
    {
        data.AddRange(value.bytes);
    }
    public void WriteVarInt(int value)
    {
        VarInt varIntValue = new VarInt(value);
        data.AddRange(varIntValue.bytes);
    }
    
    public void WriteVarLong(VarLong value)
    {
        data.AddRange(value.bytes);
    }
    public void WriteVarLong(long value)
    {
        VarLong varLongValue = new VarLong(value);
        data.AddRange(varLongValue.bytes);
    }
    public void WriteString(string value)
    {
        byte[] stringBytes = Encoding.UTF8.GetBytes(value);
        WriteVarInt(new VarInt(stringBytes.Length));
        data.AddRange(stringBytes);
    }

    public void WriteByte(byte value)
    {
        data.Add(value);
    }

    public void WriteNBT(NBT nbt)
    {
        WriteByteArray(nbt.ToBytes());
    }

    public void WriteBoolean(bool value)
    {
        data.Add(value ? (byte)1 : (byte)0);
    }

    public void WriteShort(short value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        data.AddRange(bytes);
    }

    public void WriteInt(int value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        data.AddRange(bytes);
    }

    public void WriteLong(long value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        data.AddRange(bytes);
    }

    public void WriteByteArray(byte[] value)
    {
        data.AddRange(value);
    }
    public void WriteUuid(Guid uuid)
    {
        byte[] uuidBytes = uuid.ToByteArray();
        WriteByteArray(uuidBytes);
    }
    public void WriteToData()
    {
        packet.data = data.ToArray();
    }
}