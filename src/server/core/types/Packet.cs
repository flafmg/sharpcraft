namespace sharpcraft.server.core.types;

public interface Packet
{
    VarInt lenght { get; }
    VarInt id { get; }
    byte[] data { get; }
    
}