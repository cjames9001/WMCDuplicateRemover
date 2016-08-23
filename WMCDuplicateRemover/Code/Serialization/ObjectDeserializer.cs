using System;
using System.IO;

namespace WMCDuplicateRemover.Code.Serialization
{
    public class ObjectDeserializer
    {
        public T ReadFromBinaryFile<T>(string filePath)
        {
            try
            {
                using (var stream = File.Open(filePath, FileMode.Open))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return (T)binaryFormatter.Deserialize(stream);
                }
            }
            catch
            {
                return (T)Activator.CreateInstance(typeof(T));
            }
        }
    }
}