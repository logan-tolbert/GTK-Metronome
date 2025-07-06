using System;
using System.Threading;
using System.Threading.Tasks;

using NetCoreAudio;

namespace MetronomeApp.Services
{
    public class MetronomeService : IDisposable
    {
        private readonly Player _player;
        private Timer? _timer;
        private bool _isPlaying;
        private int _currentBeat = 1;
        private int _beatsPerMeasure = 4;
        private double _bpm = 120;
        private bool _disposed;

        public event EventHandler<BeatEventArgs>? BeatOccurred;
        public event EventHandler<bool>? PlayingStateChanged;

        public MetronomeService()
        {
            _player = new Player();
        }

        public bool IsPlaying => _isPlaying;
        public int CurrentBeat => _currentBeat;
        public double BPM
        {
            get => _bpm;
            set
            {
                _bpm = value;
                if (_isPlaying)
                {
                    // Restart timer with new interval
                    Stop();
                    _ = Task.Run(async () => await Start());
                }
            }
        }
        public int BeatsPerMeasure
        {
            get => _beatsPerMeasure;
            set => _beatsPerMeasure = value;
        }

        public async Task Start()
        {
            if (_isPlaying) return;

            _isPlaying = true;
            _currentBeat = 1;

            // Calculate interval in milliseconds
            var interval = (int)(60000 / _bpm);

            _timer = new Timer(OnTimerTick, null, 0, interval);

            PlayingStateChanged?.Invoke(this, true);
        }

        public void Stop()
        {
            if (!_isPlaying) return;

            _isPlaying = false;
            _timer?.Dispose();
            _timer = null;
            _currentBeat = 1;

            PlayingStateChanged?.Invoke(this, false);
        }

        private void OnTimerTick(object? state)
        {
            if (!_isPlaying) return;

            try
            {
                // Play the appropriate click sound
                var isAccent = _currentBeat == 1;
                _ = Task.Run(async () => await PlayClick(isAccent));

                // Notify about the beat
                BeatOccurred?.Invoke(this, new BeatEventArgs(_currentBeat, isAccent));

                // Advance to next beat
                _currentBeat = _currentBeat >= _beatsPerMeasure ? 1 : _currentBeat + 1;
            }
            catch (Exception ex)
            {
                // Handle any audio playback errors
                Console.WriteLine($"Error playing metronome click: {ex.Message}");
            }
        }

        private async Task PlayClick(bool isAccent)
        {
            // For now, we'll use a simple approach - you can add actual audio files later
            // This is a placeholder that will work without audio files
            try
            {
                // TODO: Replace with actual audio file paths
                // await _player.Play("Assets/click-accent.wav") for accent
                // await _player.Play("Assets/click-normal.wav") for normal

                // For now, let's use system beep as a placeholder
                Console.Beep(isAccent ? 800 : 600, 100);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Audio playback error: {ex.Message}");
            }
        }

        public void Dispose()
        {
            if (_disposed) return;

            Stop();
            // Player doesn't implement IDisposable, so we don't need to dispose it
            _disposed = true;
        }
    }

    public class BeatEventArgs : EventArgs
    {
        public int BeatNumber { get; }
        public bool IsAccent { get; }

        public BeatEventArgs(int beatNumber, bool isAccent)
        {
            BeatNumber = beatNumber;
            IsAccent = isAccent;
        }
    }
}