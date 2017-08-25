using NUnit.Framework;

namespace CreateAR.Commons.Unity.Logging.Test
{
    [TestFixture]
    public class Log_Tests
    {
        public class DummyLogTarget : ILogTarget
        {
            public bool Called = false;
            public LogLevel Level;
            public object Caller;
            public string Message;

            public void OnLog(LogLevel level, object caller, string message)
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
        public void LogLevels()
        {
            Log.AddLogTarget(_target);
            
            Log.Debug(this, LogLevel.Debug.ToString());

            Assert.IsTrue(_target.Called);
            Assert.AreEqual(LogLevel.Debug, _target.Level);
            Assert.AreEqual(LogLevel.Debug.ToString(), _target.Message);
            Assert.AreEqual(this, _target.Caller);
            _target.Caller = null;
            _target.Called = false;

            Log.Error(this, LogLevel.Error.ToString());

            Assert.IsTrue(_target.Called);
            Assert.AreEqual(LogLevel.Error, _target.Level);
            Assert.AreEqual(LogLevel.Error.ToString(), _target.Message);
            Assert.AreEqual(this, _target.Caller);
            _target.Caller = null;
            _target.Called = false;

            Log.Warning(this, LogLevel.Warning.ToString());

            Assert.IsTrue(_target.Called);
            Assert.AreEqual(LogLevel.Warning, _target.Level);
            Assert.AreEqual(LogLevel.Warning.ToString(), _target.Message);
            Assert.AreEqual(this, _target.Caller);
            _target.Caller = null;
            _target.Called = false;

            Log.Fatal(this, LogLevel.Fatal.ToString());

            Assert.IsTrue(_target.Called);
            Assert.AreEqual(LogLevel.Fatal, _target.Level);
            Assert.AreEqual(LogLevel.Fatal.ToString(), _target.Message);
            Assert.AreEqual(this, _target.Caller);
            _target.Caller = null;
            _target.Called = false;

            Log.Info(this, LogLevel.Info.ToString());
            
            Assert.IsTrue(_target.Called);
            Assert.AreEqual(LogLevel.Info, _target.Level);
            Assert.AreEqual(LogLevel.Info.ToString(), _target.Message);
            Assert.AreEqual(this, _target.Caller);
        }

        [Test]
        public void Filter()
        {
            Log.AddLogTarget(_target);
            Log.Filter = LogLevel.Error;
            Log.Debug(this, "Test");

            Assert.IsFalse(_target.Called);
        }
    }
}