using RaidFileStorageManager.Domain;

namespace RaidFileStorageManager.Sandbox
{
    public class DataClass : IRaidDataType<DateTime>
    {
        public DataClass(DateTime sortValue, int value)
        {
            SortValue = sortValue;
            Value = value;
        }

        public DateTime SortValue { get; }
        public int Value { get; }

        public DateTime GetSortParameter()
        {
            return this.SortValue;
        }
    }
}
