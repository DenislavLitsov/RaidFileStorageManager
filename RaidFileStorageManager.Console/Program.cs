using RaidFileStorageManager.Domain;

namespace RaidFileStorageManager.Sandbox
{
    internal class Program
    {
        private const string folderLocation = @"D:\DataSets\Test";

        static void Main(string[] args)
        {
            MetaData<DateTime> metaData = new MetaData<DateTime>();
            metaData.FirstSortValue = DateTime.Now.AddDays(-5);
            metaData.LastSortValue = DateTime.Now;
            metaData.FileCount = 1;
            metaData.FilesMetaData = new[]
            {
                new FileMetaData<DateTime>(512, DateTime.Now.AddDays(-5), DateTime.Now)
            };


            //RaidFileStorageManager.RaidFileStorageManager<DataClass, DateTime> asd = new RaidFileStorageManager<DataClass, DateTime>(folderLocation, metaData);
            //
            //
            //RaidFileStorageManager.RaidFileStorageManager<DataClass, DateTime> asd2 = new RaidFileStorageManager<DataClass, DateTime>(folderLocation);

        }
    }
}
