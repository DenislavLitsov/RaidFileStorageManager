using RaidFileStorageManager.Tests.Data;

namespace RaidFileStorageManager.Tests
{
    public class StoreTests
    {
        private const string DateTimeFolderPath = @"TestData\DateTime";
        private const string IndexFolderPath = @"TestData\Index";

        private ICollection<DateTimeData> dataTimeDataArray;
        private ICollection<IndexData> indexDataArray;

        private RaidFileStorageManager<DateTimeData, DateTime> dateTimeRaidManager;
        private RaidFileStorageManager<IndexData, int> indexRaidManager;

        private const int FileMaxCount = 100000;

        [SetUp]
        public void Setup()
        {
            this.dataTimeDataArray = new List<DateTimeData>();
            this.indexDataArray = new List<IndexData>();

            DateTime dateTimeNow = DateTime.Now;
            for (int i = 0; i < 1000003; i++)
            {
                this.dataTimeDataArray.Add(new DateTimeData(dateTimeNow.AddMinutes(-i*10), i * 2, i * 5));
                this.indexDataArray.Add(new IndexData(i, i * 2, i * 5));
            }

            if (Directory.Exists(DateTimeFolderPath))
                Directory.Delete(DateTimeFolderPath, true);

            if (Directory.Exists(IndexFolderPath))
                Directory.Delete(IndexFolderPath, true);

            Directory.CreateDirectory(DateTimeFolderPath);
            Directory.CreateDirectory(IndexFolderPath);

            this.dateTimeRaidManager = new RaidFileStorageManager<DateTimeData, DateTime>(DateTimeFolderPath, FileMaxCount);
            this.indexRaidManager = new RaidFileStorageManager<IndexData, int>(IndexFolderPath, FileMaxCount);
        }

        [Test]
        public void _1_SaveFileIntoMultipleFiles()
        {
            this.dateTimeRaidManager.SaveArray(this.dataTimeDataArray, true);
            this.indexRaidManager.SaveArray(this.indexDataArray, true);

            Assert.That(Directory.GetFiles(DateTimeFolderPath).Length, Is.EqualTo(12));
            Assert.That(Directory.GetFiles(IndexFolderPath).Length, Is.EqualTo(12));

            Assert.That(this.dateTimeRaidManager.MetaData.FileCount, Is.EqualTo(11));
            Assert.That(this.indexRaidManager.MetaData.FileCount, Is.EqualTo(11));

            Assert.That(this.dateTimeRaidManager.MetaData.FilesMetaData.Count, Is.EqualTo(11));
            Assert.That(this.indexRaidManager.MetaData.FilesMetaData.Count, Is.EqualTo(11));

            Assert.That(this.dateTimeRaidManager.MetaData.FilesMetaData.First().DataCount, Is.EqualTo(FileMaxCount));
            Assert.That(this.indexRaidManager.MetaData.FilesMetaData.First().DataCount, Is.EqualTo(FileMaxCount));

            Assert.That(this.indexRaidManager.MetaData.FilesMetaData.First().StartValue, Is.EqualTo(0));
            Assert.That(this.indexRaidManager.MetaData.FilesMetaData.First().EndValue, Is.EqualTo(FileMaxCount-1));
            Assert.That(this.indexRaidManager.MetaData.FilesMetaData.Last().EndValue, Is.EqualTo(1000003-1));
        }

        [Test]
        public void _2_SaveFileIntoMultipleFilesIfFilesAlreadyExist_AtTheEnd()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void _3_SaveFileIntoMultipleFilesIfFilesAlreadyExist_AtTheBeginningByAddingNewFilesAtFirstIndex_AndAddingToFirstFileIfSpaceExists()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void _4_SaveFileIntoMultipleFilesIfFilesAlreadyExist_AtTheEnd_AddDataIntoLastExistingFileIfThereIsSpace()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void _5_SaveFileIntoMultipleFilesIfFilesAlreadyExist_AtTheBeginning_AddDataIntoFirstExistingFileIfThereIsSpace()
        {
            throw new NotImplementedException();
        }

        [Test]
        // EG: data is with step 1 hour. And array data is with step 15 mins
        public void _6_SaveFileIntoMultipleFilesIfFilesAlreadyExist_WithDataBeingBetweenManyValuesInExistingFiles()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void AssertMetaDataAfterEachTest()
        {
            throw new NotImplementedException();
        }
    }
}