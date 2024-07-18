using SharpNBT;

namespace sharpcraft.server.core.types.nbt;

public struct NBT
{
    public CompoundTag root;
    public FormatOptions formatOptions;
    
    public NBT(CompoundTag root, FormatOptions formatOptions)
    {
        this.root = root;
        this.formatOptions = formatOptions;
    }

    public byte[] ToBytes()
    {
        MemoryStream memoryStream = new MemoryStream();
        using var writer = new TagWriter(memoryStream, formatOptions);
        writer.WriteCompound(root);
        return memoryStream.ToArray();
    }
    
}