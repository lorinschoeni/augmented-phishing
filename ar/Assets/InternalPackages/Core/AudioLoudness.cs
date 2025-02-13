using UnityEngine;

namespace PhishAR.Core
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioLoudness : MonoBehaviour
    {
        public float CurrentValue { get; set; }
        public float LerpBy { get; set; } = .2f;

        private AudioSource _audioSource;

        // Read 1024 samples, which is about 80 ms on a 44khz stereo clip.
        private int _sampleDataLength = 1024;
        private float _updateStep = 0.1f;
        private float _currentUpdateTime = 0f;

        private float _clipLoudness;
        private float[] _clipSampleData;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _clipSampleData = new float[_sampleDataLength];
        }

        private void Update()
        {
            _currentUpdateTime += Time.deltaTime;
            if (_currentUpdateTime >= _updateStep)
            {
                _currentUpdateTime = 0f;
                if (_audioSource.clip == null) return;
                _audioSource.clip.GetData(_clipSampleData, _audioSource.timeSamples);
                _clipLoudness = 0f;
                foreach (var sample in _clipSampleData)
                {
                    _clipLoudness += Mathf.Abs(sample);
                }
                _clipLoudness /= _sampleDataLength;
            }
            CurrentValue = Mathf.Lerp(CurrentValue, _clipLoudness, LerpBy);
        }
    }
}
