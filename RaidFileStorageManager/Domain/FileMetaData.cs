namespace RaidFileStorageManager.Domain
{
    public class FileMetaData<SortParameterType>
        where SortParameterType : struct
    {
        public FileMetaData()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="dataCount">How many items exist in the array</param>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        public FileMetaData(int dataCount, SortParameterType startValue, SortParameterType endValue)
        {
            this.DataCount = dataCount;
            this.StartValue = startValue;
            this.EndValue = endValue;
        }


        /// <summary>
        /// How many items exist in the array
        /// </summary>
        public int DataCount { get; set; }
        public SortParameterType StartValue { get; set; }
        public SortParameterType EndValue { get; set; }
    }
}