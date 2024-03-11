using RaidFileStorageManager.Domain;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text.Json;

namespace RaidFileStorageManager
{
    public class RaidFileStorageManager<ArrayDataType, SortParameterType>
        where SortParameterType : struct
        where ArrayDataType : class, IRaidDataType<SortParameterType>
    {
        private const string MetaDataFileName = "Raid.MetaData";
        private const string RaidExtension = ".Raid";

        private readonly string raidFolderLocation;

        private MetaData<SortParameterType> metaData;

        public RaidFileStorageManager(string raidFolderLocation)
        {
            this.raidFolderLocation = raidFolderLocation;
            this.LoadMetaData();
        }

        public RaidFileStorageManager(string raidFolderLocation, int chunkSize)
        {
            this.raidFolderLocation = raidFolderLocation;
            if (File.Exists(this.GetRaidFileLocation()))
                throw new ArgumentException("You cannot set chunk size in case metadata already exists");

            this.metaData = new MetaData<SortParameterType>();
            this.metaData.ChunkSize = chunkSize;
        }

        public MetaData<SortParameterType> MetaData { get => metaData; }

        public void SaveArray(IEnumerable<ArrayDataType> data, bool isSorted)
        {
            if (!isSorted)
                data = data.OrderBy(x => x.GetSortParameter());

            if (this.metaData.FileCount == 0)
            {
                int savedCount = 0;
                int index = 1;
                while (true)
                {
                    var chunk = data
                        .Skip(savedCount)
                        .Take(this.metaData.ChunkSize)
                        .ToList();

                    if (chunk.Count == 0)
                    {
                        break;
                    }

                    savedCount += chunk.Count;
                    this.SaveFile(chunk, index.ToString());
                    index++;
                    this.metaData.FileCount++;
                    this.metaData.FilesMetaData
                        .Add(new FileMetaData<SortParameterType>(
                                chunk.Count,
                                chunk.First().GetSortParameter(),
                                chunk.Last().GetSortParameter()));
                }
            }


            this.SaveMetaData();
        }

        public IEnumerable<ArrayDataType> GetDataByChunks(int startChunk, int chunkCount)
        {
            List<ArrayDataType> result = new List<ArrayDataType>();
            for (int i = startChunk; i < startChunk + chunkCount; i++)
            {
                var loadedChunk = this.LoadChunk(i);
                result.AddRange(loadedChunk);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex">Included</param>
        /// <param name="endIndex">Excluded</param>
        /// <returns></returns>
        public IEnumerable<ArrayDataType> GetDataByCount(int startIndex, int endIndex)
        {
            List<ArrayDataType> result = new List<ArrayDataType>();
            int startChunk = startIndex / this.metaData.ChunkSize;
            int lastChunk = endIndex / this.metaData.ChunkSize;

            int skipCount = startIndex - ((startIndex / this.metaData.ChunkSize) * this.metaData.ChunkSize);
            var firstChunk = this.LoadChunk(startChunk)
                .Skip(skipCount)
                .ToList();

            if (firstChunk.Count <= endIndex-startIndex)
            {
                firstChunk = firstChunk
                    .Take(endIndex - startIndex)
                    .ToList();

                return firstChunk;
            }

            result.AddRange(firstChunk);

            for (int i = startChunk + 1; i <= lastChunk; i++)
            {
                var loadedChunk = this.LoadChunk(i);
                result.AddRange(loadedChunk);
            }

            if (result.Count <= endIndex - startIndex)
            {
                result = result
                    .Take(endIndex - startIndex)
                    .ToList();
            }

            return result;
        }

        public IEnumerable<ArrayDataType> GetDataSortParameter(int startChunk, int lastChunk)
        {
            throw new NotImplementedException();
        }

        private void SaveMetaData()
        {
            var serializedMetaData = JsonSerializer.Serialize(this.metaData);

            string metaDataPath = this.GetRaidFileLocation();
            File.WriteAllText(metaDataPath, serializedMetaData);
        }

        private void SaveFile(IEnumerable<ArrayDataType> dataToSave, string index)
        {
            string filePath = Path.Combine(this.raidFolderLocation, $"{index}{RaidExtension}");

            var serializedData = JsonSerializer.Serialize(dataToSave);
            using (StreamWriter streamWriter = new StreamWriter(filePath, false))
            {
                streamWriter.Write(serializedData);
                streamWriter.Flush();
            }
        }

        private IEnumerable<ArrayDataType> LoadChunk(int index)
        {
            var path = Path.Combine(this.raidFolderLocation, $"{index}{RaidExtension}");
            var data = File.ReadAllText(path);

            var parsedData = JsonSerializer.Deserialize<IEnumerable<ArrayDataType>>(data);
            return parsedData;
        }

        private void LoadMetaData()
        {
            if (Directory.GetFiles(this.raidFolderLocation).Length == 0)
            {
                this.metaData = new MetaData<SortParameterType>();
                return;
            }

            string metaDataPath = this.GetRaidFileLocation();
            string stringMetaData = File.ReadAllText(metaDataPath);
            this.metaData = JsonSerializer.Deserialize<MetaData<SortParameterType>>(stringMetaData);
        }

        private string GetRaidFileLocation()
        {
            string path = Path.Combine(this.raidFolderLocation, MetaDataFileName);
            return path;
        }
    }
}