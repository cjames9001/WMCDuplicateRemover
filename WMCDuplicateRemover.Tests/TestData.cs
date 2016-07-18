using System.Collections;
namespace WMCDuplicateRemover.Tests
{
    public abstract class TestData : IEnumerable
    {
        protected readonly IEnumerable _testCases;

        public IEnumerator GetEnumerator()
        {
            return _testCases.GetEnumerator();
        }

        public TestData()
        {
            _testCases = CreateTestData();
        }

        protected abstract IEnumerable CreateTestData();
    }
}
