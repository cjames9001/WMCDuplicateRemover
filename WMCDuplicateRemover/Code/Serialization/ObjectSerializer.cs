using System.IO;

namespace WMCDuplicateRemover.Code.Serialization
{
    public class ObjectSerializer
    {
        public void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (var stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }
    }
}