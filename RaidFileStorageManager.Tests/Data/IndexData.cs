namespace RaidFileStorageManager.Tests.Data
{
    using RaidFileStorageManager.Domain;

    public class IndexData : IRaidDataType<int>
    {
        public IndexData(int index, int value, int value2)
        {
            this.Index = index;
            this.Value = value;
            this.Value2 = value2;
        }

        public int Index { get; set; }
        public int Value { get; set; }
        public int Value2 { get; set; }

        public int GetSortParameter()
        {
            return this.Index;
        }
    }
}
