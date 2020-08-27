using NUnit.Framework;

namespace Enklu.Commons.Unity.Logging.Test
{
    [Parallelizable(ParallelScope.None)]
    [TestFixture]
    public class Log_Tests
    {
        public class DummyLogTarget : ILogTarget
        {
            public bool Called = false;
            public LogLevel Level;
            public object Caller;
            public string Message;

            public LogLevel Filter { get; set; }

            public void OnLog(LogLevel level, object caller, string message, object meta)
            {
                Called = true;

                Level = level;
                Caller = caller;
                Message = message;
            }
        }

        private DummyLogTarget _target;

        [SetUp]
        public void Setup()
        {
            _target = new DummyLogTarget();
        }

        [TearDown]
        public void Teardown()
        {
            foreach (var target in Log.Targets)
            {
                Log.RemoveLogTarget(target);
            }
        }

        [Test]
        public void AddRemoveTarget()
        {
            Log.AddLogTarget(_target);

            Assert.AreSame(_target, Log.Targets[0]);

            Log.RemoveLogTarget(_target);

            Assert.AreEqual(0, Log.Targets.Length);
        }

        [Test]
        public void NullLog()
        {
            Assert.DoesNotThrow(() => Log.Info(this, null));
        }

        [Test]
        public void Replacements()
        {
            Log.AddLogTarget(_target);

            var str = "Hello";
            var num = "226";
            var toReplace = "{0}, Subject {1}";
            
            Log.Info(this, toReplace, str, num);
            
            Assert.AreEqual(string.Format(toReplace, str, num), _target.Message);
        }
    }
}