using GodSharp.Opc.Da;

namespace HmExtension.Opc.da;

public static class OpcDaExtension
{

    public static TagValue Clone(this TagValue tv)
    {
        var tagInfo = new Tag(tv.ItemName, tv.ClientHandle, tv.ServerHandle);
        return new TagValue(tagInfo, tv.Value)
        {
            Timestamp = tv.Timestamp,
            Quality = tv.Quality,
            Additional = tv.Additional,
            RequestType = tv.RequestType
        };
    }

}