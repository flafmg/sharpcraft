namespace sharpcraft.server.core.types;

public class Position
{
    private long _longValue;
    private int _x;
    private int _y;
    private int _z;

    public int X
    {
        get { return _x; }
        set
        {
            _x = value;
            UpdateLongValue();
        }
    }

    public int Y
    {
        get { return _y; }
        set
        {
            _y = value;
            UpdateLongValue();
        }
    }

    public int Z
    {
        get { return _z; }
        set
        {
            _z = value;
            UpdateLongValue();
        }
    }

    public long LongValue
    {
        get { return _longValue; }
        set
        {
            _longValue = value;
            UpdateCoordinates();
        }
    }

    public Position(long value)
    {
        LongValue = value;
    }

    public Position(byte[] bytes)
    {
        if (bytes.Length != 8)
            throw new ArgumentException("Byte array length must be 8");

        LongValue = BitConverter.ToInt64(bytes, 0);
    }

    public Position(int x, int y, int z)
    {
        _x = x;
        _y = y;
        _z = z;
        UpdateLongValue();
    }

    private void UpdateLongValue()
    {
        _longValue = ((long)(_x & 0x3FFFFFF) << 38) | ((long)(_z & 0x3FFFFFF) << 12) | (_y & 0xFFF);
    }

    private void UpdateCoordinates()
    {
        _x = (int)(_longValue >> 38);
        _y = (int)(_longValue << 52 >> 52);
        _z = (int)(_longValue << 26 >> 38);

        if (_x >= 1 << 25) { _x -= 1 << 26; }
        if (_y >= 1 << 11) { _y -= 1 << 12; }
        if (_z >= 1 << 25) { _z -= 1 << 26; }
    }
}
