namespace RaidFileStorageManager.Tests.Data
{
    using RaidFileStorageManager.Domain;

    [Serializable]
    public class DateTimeData : IRaidDataType<DateTime>
    {
        public DateTimeData(DateTime dateTime, int value, int value2)
        {
            this.DateTime = dateTime;
            this.Value = value;
            this.Value2 = value2;
        }

        public DateTime DateTime { get; set; }
        public int Value { get; set; }
        public int Value2 { get; set; }

        public DateTime GetSortParameter()
        {
            return this.DateTime;
        }
    }
}
