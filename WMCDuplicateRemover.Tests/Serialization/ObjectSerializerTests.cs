using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using WMCDuplicateRemover.Code.Serialization;

namespace WMCDuplicateRemover.Tests.Serialization
{
    [TestFixture]
    public class ObjectSerializationTests
    {
        private string _serializedFilePath = @".\SerializedHashSet";

        [Test]
        public void TestHashSetStringsSerialization()
        {
            var eventLogCache = new HashSet<string>
            {
                "American Gothic: The Oxbow",
                "The Daily Show: June 3, 2016",
                "Star Talk: Larry Wilmore"
            };

            var serializer = new ObjectSerializer();
            serializer.WriteToBinaryFile(_serializedFilePath, eventLogCache);
            var deserializer = new ObjectDeserializer();
            var resultingHashSet = deserializer.ReadFromBinaryFile<HashSet<string>>(_serializedFilePath);

            Assert.AreEqual(eventLogCache, resultingHashSet);
        }

        [Test]
        public void TestSerializeToNullFile()
        {
            var testHashSet = new HashSet<string>();
            var serializer = new ObjectSerializer();
            Assert.Throws<ArgumentNullException>(() => serializer.WriteToBinaryFile(null, testHashSet));
        }

        [Test]
        public void TestDeserializeWithNullFilePath()
        {
            var testHashSet = new HashSet<string>();
            var deserializer = new ObjectDeserializer();
            Assert.AreEqual(new HashSet<string>(), deserializer.ReadFromBinaryFile<HashSet<string>>(null));
        }

        [TearDown]
        public void TearDown()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.Delete(_serializedFilePath);
        }
    }
}