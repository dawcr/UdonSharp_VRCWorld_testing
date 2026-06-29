static class GatherableResourceTypeExtensions
{
    public static string GetName(GatherableResourceType gatherableResourceType)
    {
        switch (gatherableResourceType)
        {
            case GatherableResourceType.Wood:
                return "Wood";
            case GatherableResourceType.Stone:
                return "Stone";
            default:
                return "Undefined";
        }
    }
    
    public static string GetName(int i)
    {
        switch ((GatherableResourceType)i)
        {
            case GatherableResourceType.Wood:
                return "Wood";
            case GatherableResourceType.Stone:
                return "Stone";
            default:
                return "Undefined";
        }
    }
}