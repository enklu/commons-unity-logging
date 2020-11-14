using NUnit.Framework;

namespace Enklu.Commons.Unity.Logging.Test
{
    [Parallelizable(ParallelScope.None)]
    [TestFixture]
    public class LogFormatter_Tests
    {
        private DefaultLogFormatter _formatter;
        
        [SetUp]
        public void Setup()
        {
            _formatter = new DefaultLogFormatter();
            _formatter.Timestamp = false;
            _formatter.Level = false;
        }
        
        [Test]
        public void StringCaller()
        {
            var log = _formatter.Format(LogLevel.Info, "Custom", "Message");
            Assert.AreEqual("[Custom]\tMessage\n", log);
        }
    }
}