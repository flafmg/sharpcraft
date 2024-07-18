using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using sharpcraft.server.core.util;

namespace sharpcraft.server.core.configuration;

public class ServerConfiguration
{
    private static string ConfigurationPath = "server-config.json";
    
    internal static int ProtocolVersion = 767;
    internal static string VersionName = "1.21";
    internal static string FavIconBase64;

    public static string ServerAddress = "127.0.0.1";
    public static ushort ServerPort = 25565;
    
    public static int MaxPlayers = 20;
    public static string FavIconPath = "server-icon.png";
    public static string MoTD = "A minecraft server";

    public static void LoadConfig()
    {
        if (!File.Exists(ConfigurationPath))
        {
            SaveConfig();
            return;
        }

        try
        {
            FileStream fileStream = File.OpenRead(ConfigurationPath);
            JsonDocument jsonDocument = JsonDocument.Parse(fileStream);
            JsonElement rootElement = jsonDocument.RootElement;

            ServerAddress = rootElement.GetProperty("ServerAddress").GetString();
            ServerPort = rootElement.GetProperty("ServerPort").GetUInt16();
            
            MaxPlayers = rootElement.GetProperty("MaxPlayers").GetInt32();
            FavIconPath = rootElement.GetProperty("FavIconPath").GetString();
            MoTD = rootElement.GetProperty("MoTD").GetString();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading server config: {ex.Message}");
        }

        FavIconBase64 = Util.GetFavIconBase64(FavIconPath);
    }

    public static void SaveConfig()
    {
        try
        {
            FileStream fileStream = File.Create(ConfigurationPath);
            Utf8JsonWriter jsonWriter = new Utf8JsonWriter(fileStream, new JsonWriterOptions() { Indented = true });
            
            jsonWriter.WriteStartObject();
            
            jsonWriter.WriteString("ServerAddress", ServerAddress);
            jsonWriter.WriteNumber("ServerPort", ServerPort);
            
            jsonWriter.WriteNumber("MaxPlayers" ,MaxPlayers);
            jsonWriter.WriteString("FavIconPath", FavIconPath);
            jsonWriter.WriteString("MoTD", MoTD);
            
            jsonWriter.WriteEndObject();
            jsonWriter.Flush();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving configuration: {ex.Message}");
        }
    }


}