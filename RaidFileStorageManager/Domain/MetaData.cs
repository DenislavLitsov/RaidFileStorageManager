namespace RaidFileStorageManager.Domain
{
    [Serializable]
    public class MetaData<SortParameterType>
        where SortParameterType : struct
    {
        public MetaData()
        {
        }

        public int FileCount { get; set; }

        public SortParameterType FirstSortValue { get; set; }

        public SortParameterType LastSortValue { get; set; }

        public IEnumerable<FileMetaData<SortParameterType>> FilesMetaData { get; set; }
    }
}