using RaidFileStorageManager.Tests.Data;

namespace RaidFileStorageManager.Tests
{
    public class ReadTests
    {
        private const string DateTimeFolderPath = @"TestData\DateTime";
        private const string IndexFolderPath = @"TestData\Index";

        private const int FileMaxCount = 100000;

        private RaidFileStorageManager<DateTimeData, DateTime> dateTimeRaidManager;
        private RaidFileStorageManager<IndexData, int> indexRaidManager;

        [SetUp]
        public void Setup()
        {
            this.dateTimeRaidManager = new RaidFileStorageManager<DateTimeData, DateTime>(DateTimeFolderPath);
            this.indexRaidManager = new RaidFileStorageManager<IndexData, int>(IndexFolderPath);
        }

        [Test]
        public void _1_AssertMetaData()
        {
            Assert.That(this.dateTimeRaidManager.MetaData.FileCount, Is.EqualTo(11));
            Assert.That(this.indexRaidManager.MetaData.FileCount, Is.EqualTo(11));

            Assert.That(this.dateTimeRaidManager.MetaData.FilesMetaData.Count, Is.EqualTo(11));
            Assert.That(this.indexRaidManager.MetaData.FilesMetaData.Count, Is.EqualTo(11));

            Assert.That(this.dateTimeRaidManager.MetaData.FilesMetaData.First().DataCount, Is.EqualTo(FileMaxCount));
            Assert.That(this.indexRaidManager.MetaData.FilesMetaData.First().DataCount, Is.EqualTo(FileMaxCount));

            Assert.That(this.indexRaidManager.MetaData.FilesMetaData.First().StartValue, Is.EqualTo(0));
            Assert.That(this.indexRaidManager.MetaData.FilesMetaData.First().EndValue, Is.EqualTo(FileMaxCount - 1));
            Assert.That(this.indexRaidManager.MetaData.FilesMetaData.Last().EndValue, Is.EqualTo(1000003 - 1));
        }

        [Test]
        public void _1_LoadArrayByFileIndex()
        {
            var firstChunk = this.indexRaidManager.GetDataByChunks(1, 1);
            var secondChunk = this.indexRaidManager.GetDataByChunks(2, 1);
            var lastChunk = this.indexRaidManager.GetDataByChunks(11, 1);
            var twoChunks = this.indexRaidManager.GetDataByChunks(3, 2);

            Assert.That(firstChunk.Count(), Is.EqualTo(FileMaxCount));
            Assert.That(secondChunk.Count(), Is.EqualTo(FileMaxCount));
            Assert.That(lastChunk.Count(), Is.EqualTo(this.indexRaidManager.MetaData.FilesMetaData.Last().DataCount));
            Assert.That(twoChunks.Count(), Is.EqualTo(FileMaxCount*2));

            Assert.That(firstChunk.First().GetSortParameter(), Is.EqualTo(0));
            Assert.That(secondChunk.First().GetSortParameter(), Is.EqualTo(FileMaxCount));
            Assert.That(lastChunk.Last().GetSortParameter(), Is.EqualTo(1000002));
            Assert.That(twoChunks.First().GetSortParameter(), Is.EqualTo(FileMaxCount*2));
        }

        [Test]
        public void _2_LoadArrayByComparison()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void _3_LoadArrayByCountStartAndFinishIndex()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void _4_LoadArrayByStartComparisonAndEndCount()
        {
            throw new NotImplementedException();
        }
    }
}