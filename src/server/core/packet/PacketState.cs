namespace sharpcraft.server.core.types.packet.stream;

public enum PacketState
{
    Handshake = 0, 
    Status = 1, 
    Login = 2, 
    Transfer = 3, 
    Configuration = 4, 
    Play = 5
}