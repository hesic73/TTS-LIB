using TTS_LIB;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Test
{
    [TestClass]
    public class TestLongAudio
    {
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod]
        public void Test1()
        {
            var path = "D:\\2023\\TTS_LIB\\Test\\bin\\Debug\\net7.0\\tmp.txt";
            var long_text = File.ReadAllText(path);
            var agent = new TextToSpeechAgent("AIzaSyC6JbahH1pZ8MSPaKM5s30D4j7d3XNosdo");
            var s = agent.longPlainTextToSpeechAsync(long_text);
            TestContext.WriteLine($"¹Ø×¢ÓÀ³ûËþ·ÆÐ»Ð»ß÷¡£");
            Utils.WriteToFile("long_article.mp3", s.Result);
        }


    }
}