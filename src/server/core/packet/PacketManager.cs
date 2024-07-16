using System.Net.Sockets;
using System.Text;
using sharpcraft.server.core.packet.server;
using sharpcraft.server.core.types;
using sharpcraft.server.core.types.client;

namespace sharpcraft.server.core.packet;

public class PacketManager
{
   private Dictionary<State, Dictionary<byte, Func<GenericServerPacket, ServerPacket>>> serverPackets;
   internal static State CurrentState = State.HANDSHAKE;

   public PacketManager()
   {
      serverPackets = new Dictionary<State, Dictionary<byte, Func<GenericServerPacket, ServerPacket>>>
      {
         { State.HANDSHAKE, new Dictionary<byte, Func<GenericServerPacket, ServerPacket>>() },
         { State.STATUS, new Dictionary<byte, Func<GenericServerPacket, ServerPacket>>() },
         { State.LOGIN, new Dictionary<byte, Func<GenericServerPacket, ServerPacket>>() },
         { State.PLAY, new Dictionary<byte, Func<GenericServerPacket, ServerPacket>>() }
      };
      
      serverPackets[State.HANDSHAKE].Add(0x00, genericPacket => new HandshakePacket(genericPacket));
      
      serverPackets[State.STATUS].Add(0x00, packet => new MOTDRequestPacket());
      serverPackets[State.STATUS].Add(0x01, packet => new PingRequestPacket(packet));
      
      serverPackets[State.LOGIN].Add(0x00, packet => new LoginStartPacket(packet));
   }

   public void HandlePacket(TcpClient client, byte[] data)
   {
      GenericServerPacket gnsp = new GenericServerPacket(data);
      Console.WriteLine($" - STATE: {CurrentState.ToString()}");
      if (serverPackets[CurrentState].TryGetValue((byte)gnsp.id.value, out var packetHandler))
      {
         ServerPacket packet = packetHandler(gnsp);
         packet.Resolve(client);
      }
   }
}

public enum State
{
   HANDSHAKE, STATUS, LOGIN, PLAY
}