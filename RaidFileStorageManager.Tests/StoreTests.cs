using RaidFileStorageManager.Tests.Data;

namespace RaidFileStorageManager.Tests
{
    public class StoreTests
    {
        private ICollection<DateTimeData> dataTimeDataArray;
        private ICollection<IndexData> indexDataArray;

        private const int FileMaxCount = 100000;

        [SetUp]
        public void Setup()
        {
            this.dataTimeDataArray = new List<DateTimeData>();
            this.indexDataArray = new List<IndexData>();

            DateTime dateTimeNow = DateTime.Now;
            for (int i = 0; i < 1000000; i++)
            {
                this.dataTimeDataArray.Add(new DateTimeData(dateTimeNow.AddMinutes(-i*10), i * 2, i * 5));
                this.indexDataArray.Add(new IndexData(i, i * 2, i * 5));
            }
        }

        [Test]
        public void SaveFileIntoMultipleFiles()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void SaveFileIntoMultipleFilesIfFilesAlreadyExist_AtTheEnd()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void SaveFileIntoMultipleFilesIfFilesAlreadyExist_AtTheBeginningByAddingNewFilesAtFirstIndex_AndAddingToFirstFileIfSpaceExists()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void SaveFileIntoMultipleFilesIfFilesAlreadyExist_AtTheEnd_AddDataIntoLastExistingFileIfThereIsSpace()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void SaveFileIntoMultipleFilesIfFilesAlreadyExist_AtTheBeginning_AddDataIntoFirstExistingFileIfThereIsSpace()
        {
            throw new NotImplementedException();
        }

        [Test]
        // EG: data is with step 1 hour. And array data is with step 15 mins
        public void SaveFileIntoMultipleFilesIfFilesAlreadyExist_WithDataBeingBetweenManyValuesInExistingFiles()
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