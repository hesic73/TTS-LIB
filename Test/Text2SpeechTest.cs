using TTS_LIB;
using System.Linq;

namespace Test
{
    [TestClass]
    public class Text2SpeechTest
    {
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }
        [TestMethod]
        public void Test_synchronous()
        {
            var agent = new TextToSpeechAgent("AIzaSyC6JbahH1pZ8MSPaKM5s30D4j7d3XNosdo");
            var test = "A frail-looking young man with pale skin and dark" +
                "circles under his eyes was sitting on a rusty bench across from the police" +
                " station.";

            var data = agent.plainTextToSpeech(test);
            Utils.WriteToFile("sentence.mp3", data);
            TestContext.WriteLine($"{data.Length}");
        }

        [TestMethod]
        public void Test_asynchronous()
        {
            var agent = new TextToSpeechAgent("AIzaSyC6JbahH1pZ8MSPaKM5s30D4j7d3XNosdo");
            var test = "A task that doesn\'t return a value is represented by the System.Threading.Tasks.Task class. A task that returns a value is represented by the System.Threading.Tasks.Task<TResult> class, which inherits from Task. The task object handles the infrastructure details and provides methods and properties that are accessible from the calling thread throughout the lifetime of the task. For example, you can access the Status property of a task at any time to determine whether it has started running, ran to completion, was canceled, or has thrown an exception. The status is represented by a TaskStatus enumeration.";

            var task = agent.plainTextToSpeechAsync(test);
            TestContext.WriteLine("¹Ø×¢ÓÀ³ûËþ·ÆÐ»Ð»ß÷£¡");
            var data = task.Result;
            Utils.WriteToFile("test_asynchronous.mp3", data);
            TestContext.WriteLine($"{data.Length}");
        }

        [TestMethod]
        public void Test_split()
        {
            var sentence = "A frail-looking young man with pale skin and dark" +
                "circles under his eyes was sitting on a rusty bench across from the police" +
                " station.";

            var long_string = string.Concat(Enumerable.Repeat(sentence, 60));
            TestContext.WriteLine($"{long_string.Length}");

            var strings = Utils.SplitLongString(long_string);

            TestContext.WriteLine($"{strings.Count}");

            int sum = 0;
            foreach (var s in strings)
            {
                sum += s.Length;
                TestContext.WriteLine(s.Substring(Math.Max(0, s.Length - 100)));
            }
            Assert.AreEqual(sum, long_string.Length);
        }
    }
}