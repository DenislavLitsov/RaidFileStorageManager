using RaidFileStorageManager.Domain;
using System.Runtime.CompilerServices;
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

        public void SaveArray(IEnumerable<ArrayDataType> data, bool isSorted)
        {
            if (!isSorted)
                data = data.OrderBy(x => x.GetSortParameter());


        }

        private void SaveMetaData()
        {
            var serializedMetaData = JsonSerializer.Serialize(this.metaData);

            string metaDataPath = this.GetRaidFileLocation();
            File.WriteAllText(metaDataPath, serializedMetaData);
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