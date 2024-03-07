namespace RaidFileStorageManager.Domain
{
    public interface IRaidDataType<SortParameterType>
    {
        SortParameterType GetSortParameter();
    }
}
