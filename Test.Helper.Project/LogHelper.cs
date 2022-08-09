using NUnit.Framework;

namespace Test.Helper.Project
{
    internal class LogHelper
    {
        public static void Log(string message)
        {
            var now = DateTime.UtcNow;
            TestContext.WriteLine($"{now:yyyy-MM-dd HH:mm:ss}> {message}");
        }
    }
}