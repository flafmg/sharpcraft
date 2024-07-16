namespace sharpcraft.server.core.types;

public class VarInt
{
    private int _value;
    private byte[] _bytes;

    public int value
    {
        get { return _value; }
        set
        {
            _value = value;
            _bytes = Encode(value);
        }
    }

    public byte[] bytes
    {
        get { return _bytes; }
        set
        {
            _bytes = value;
            _value = Decode(value);
        }
    }

    public VarInt(int value)
    {
        _value = value;
        _bytes = Encode(value);
    }

    public VarInt(byte[] bytes)
    {
        _bytes = bytes;
        _value = Decode(bytes);
    }

    public VarInt(byte[] bytes, int index)
    {
        _bytes = new byte[bytes.Length - index];
        Array.Copy(bytes, index, _bytes, 0, bytes.Length - index);
        _value = Decode(_bytes);
    }

    private byte[] Encode(int value)
    {
        byte[] rawValue = new byte[GetSize()];
        int index = 0;

        do
        {
            byte currentByte = (byte)(value & 0x7f);
            value >>= 7;

            if (value != 0)
            {
                currentByte |= 0x80;
            }

            rawValue[index++] = currentByte;
        } while (value != 0);

        return rawValue;
    }

    private int Decode(byte[] bytes)
    {
        int value = 0;
        int shift = 0;

        for (int i = 0; i < bytes.Length; i++)
        {
            byte currentByte = bytes[i];
            value |= (currentByte & 0x7F) << shift;
            shift += 7;

            if ((currentByte & 0x80) == 0)
            {
                break;
            }

            if (shift >= 32)
            {
                throw new FormatException("VarInt is too big!");
            }
        }

        return value;
    }

    public int GetSize()
    {
        int value = this.value;
        int size = 0;

        do
        {
            size++;
            value >>= 7;
        } while (value != 0);

        return size;
    }
}