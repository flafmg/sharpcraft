using System.Security.Cryptography;
using System.Text;

namespace sharpcraft.server.core.util;

public class Util
{
    public static Guid GenerateOfflineUUID(string username)
    {
        SHA1 sha1 = SHA1.Create();
        byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(username));
        
        byte[] guidBytes = new byte[16];
        Array.Copy(hash, 0, guidBytes, 0, 16);

        guidBytes[6] &= 0x0F;
        guidBytes[6] |= 0x50; 
        guidBytes[8] &= 0x3F;
        guidBytes[8] |= 0x80;

        return new Guid(guidBytes);
    }
}