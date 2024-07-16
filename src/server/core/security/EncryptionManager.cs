using System.Security.Cryptography;

namespace sharpcraft.server.core.security;

public class EncryptionManager
{
    private static RSACryptoServiceProvider rsa;

    static EncryptionManager()
    {
        rsa = new RSACryptoServiceProvider(1024);
        rsa.PersistKeyInCsp = false;
    }

    public static byte[] GetPublicKey()
    {
        return rsa.ExportRSAPublicKey();
    }

    public static RSAParameters GetPrivateKey()
    {
        return rsa.ExportParameters(true);
    }

    public static byte[] GenerateVerifyToken(int length = 4)
    {
        byte[] verifyToken = new byte[length];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(verifyToken);
        }
        return verifyToken;
    }
}