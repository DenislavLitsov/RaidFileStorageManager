namespace RaidFileStorageManager.Domain
{
    [Serializable]
    public class MetaData<SortParameterType>
        where SortParameterType : struct
    {
        public MetaData()
        {
            this.FilesMetaData = new List<FileMetaData<SortParameterType>>();
        }

        public int FileCount { get; set; }

        public int ChunkSize { get; set; }

        public SortParameterType FirstSortValue { get; set; }

        public SortParameterType LastSortValue { get; set; }

        public ICollection<FileMetaData<SortParameterType>> FilesMetaData { get; set; }
    }
}