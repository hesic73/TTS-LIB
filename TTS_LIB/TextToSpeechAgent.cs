using Google.Apis.Texttospeech.v1beta1;
using Google.Apis.Texttospeech.v1beta1.Data;
using Google.Apis.Services;
using System.Threading.Tasks;

namespace TTS_LIB
{
    public class TextToSpeechAgent
    {
        private TexttospeechService _service;
        // in most cases there is no need to change this
        // So only change it when necessary instead of passing it
        // as a parameter every time calling the API
        private AudioConfig _audioConfig;
        public AudioConfig AudioConfig
        {
            get { return _audioConfig; }
            set { _audioConfig = value; }
        }

        private VoiceSelectionParams _voiceSelectionParams;
        public VoiceSelectionParams VoiceSelectionParams
        {
            get { return _voiceSelectionParams; }
            set { _voiceSelectionParams = value; }
        }

        public TextToSpeechAgent(string api_key)
        {
            _audioConfig = new AudioConfig { AudioEncoding = "Mp3" };
            _voiceSelectionParams = new VoiceSelectionParams
            {
                LanguageCode = "en-US",
                SsmlGender = "MALE"
            };
            _service = new TexttospeechService(new BaseClientService.Initializer
            {
                ApplicationName = "Text to Speech",
                ApiKey = api_key,
            });
        }

        // TO DO: more settings
        public byte[] plainTextToSpeech(string text)
        {
            var synthesisInput = new SynthesisInput { Text = text };
            var ss_request = new SynthesizeSpeechRequest
            {
                AudioConfig = _audioConfig,
                Input = synthesisInput,
                Voice = _voiceSelectionParams
            };
            var request = _service.Text.Synthesize(ss_request);
            var response = request.Execute();

            return Convert.FromBase64String(response.AudioContent);
        }

        public Task<byte[]> plainTextToSpeechAsync(string text)
        {
            var synthesisInput = new SynthesisInput { Text = text };
            var ss_request = new SynthesizeSpeechRequest
            {
                AudioConfig = _audioConfig,
                Input = synthesisInput,
                Voice = _voiceSelectionParams
            };
            var request = _service.Text.Synthesize(ss_request);

            return Task<byte[]>.Run(()=>
            {
                var response = request.Execute();

                return Convert.FromBase64String(response.AudioContent);
            });

        }
    }
}