using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Avalonia.Media;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MetronomeApp.Services;

namespace MetronomeApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, IDisposable
{
    private readonly MetronomeService _metronomeService;

    [ObservableProperty]
    private int currentBPM = 120;

    [ObservableProperty]
    private string timeSignature = "4/4";

    [ObservableProperty]
    private bool isPlaying = false;

    [ObservableProperty]
    private string playButtonText = "▶ PLAY";

    [ObservableProperty]
    private IBrush beat1Color = Brushes.Gray;

    [ObservableProperty]
    private IBrush beat2Color = Brushes.Gray;

    [ObservableProperty]
    private IBrush beat3Color = Brushes.Gray;

    [ObservableProperty]
    private IBrush beat4Color = Brushes.Gray;

    public ObservableCollection<string> TimeSignatures { get; } = new()
    {
        "4/4", "3/4", "2/4", "6/8"
    };

    public MainWindowViewModel()
    {
        _metronomeService = new MetronomeService();
        _metronomeService.BeatOccurred += OnBeatOccurred;
        _metronomeService.PlayingStateChanged += OnPlayingStateChanged;
    }

    [RelayCommand]
    private void IncreaseBPM()
    {
        if (CurrentBPM < 200)
        {
            CurrentBPM += 5;
            _metronomeService.BPM = CurrentBPM;
        }
    }

    [RelayCommand]
    private void DecreaseBPM()
    {
        if (CurrentBPM > 40)
        {
            CurrentBPM -= 5;
            _metronomeService.BPM = CurrentBPM;
        }
    }

    [RelayCommand]
    private async Task PlayStop()
    {
        if (IsPlaying)
        {
            _metronomeService.Stop();
        }
        else
        {
            await _metronomeService.Start();
        }
    }

    partial void OnTimeSignatureChanged(string value)
    {
        // Update beats per measure based on time signature
        var beats = value switch
        {
            "4/4" => 4,
            "3/4" => 3,
            "2/4" => 2,
            "6/8" => 6,
            _ => 4
        };

        _metronomeService.BeatsPerMeasure = beats;
        ResetBeatIndicators();
    }

    private void OnBeatOccurred(object? sender, BeatEventArgs e)
    {
        // Update beat indicators on UI thread
        Avalonia.Threading.Dispatcher.UIThread.Post(() =>
        {
            ResetBeatIndicators();

            // Light up the current beat
            var accentColor = Brushes.Red;
            var normalColor = Brushes.Green;
            var color = e.IsAccent ? accentColor : normalColor;

            switch (e.BeatNumber)
            {
                case 1: Beat1Color = color; break;
                case 2: Beat2Color = color; break;
                case 3: Beat3Color = color; break;
                case 4: Beat4Color = color; break;
            }
        });
    }

    private void OnPlayingStateChanged(object? sender, bool isPlaying)
    {
        // Update UI on UI thread
        Avalonia.Threading.Dispatcher.UIThread.Post(() =>
        {
            IsPlaying = isPlaying;
            PlayButtonText = isPlaying ? "⏸ STOP" : "▶ PLAY";

            if (!isPlaying)
            {
                ResetBeatIndicators();
            }
        });
    }

    private void ResetBeatIndicators()
    {
        Beat1Color = Brushes.Gray;
        Beat2Color = Brushes.Gray;
        Beat3Color = Brushes.Gray;
        Beat4Color = Brushes.Gray;
    }

    public void Dispose()
    {
        _metronomeService?.Dispose();
    }
}