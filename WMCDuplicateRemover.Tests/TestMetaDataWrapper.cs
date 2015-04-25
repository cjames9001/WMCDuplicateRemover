using System;
using System.Collections;
namespace WMCDuplicateRemover.Tests
{
    public abstract class TestMetaDataWrapper : IEnumerable
    {
        protected readonly IEnumerable _metaDataWrappers;

        public IEnumerator GetEnumerator()
        {
            return _metaDataWrappers.GetEnumerator();
        }

        public TestMetaDataWrapper()
        {
            _metaDataWrappers = CreateTestData();
        }

        protected abstract IEnumerable CreateTestData();
    }
}
