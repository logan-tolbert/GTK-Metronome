# MetronomeApp

A cross-platform metronome application built with Avalonia UI and .NET 9.

## Features

- **Adjustable BPM**: Set tempo from 40 to 300 beats per minute in 5 BPM increments
- **Multiple Time Signatures**: Support for 4/4, 3/4, 2/4, and 6/8 time signatures
- **Visual Beat Indicators**: Color-coded beat visualization with accent highlighting
- **Audio Feedback**: Cross-platform audio support via NetCoreAudio library
- **Modern MVVM Architecture**: Built with CommunityToolkit.Mvvm for clean separation of concerns

## Requirements

- .NET 9.0 or later
- Cross-platform support: Windows, macOS, Linux

## Getting Started

### Building from Source

1. Clone the repository
2. Navigate to the project directory
3. Build the project:
   ```bash
   dotnet build
   ```

### Running the Application

```bash
dotnet run
```

Or for a release build:

```bash
dotnet run --configuration Release
```

- The executable created will be located in `bin\Release\publish`. Move the file to you desired directory, Double-click `MetronomeApp.exe` to run the release build.

## Usage

1. **Set BPM**: Use the + and - buttons to adjust the tempo
2. **Choose Time Signature**: Select from the dropdown (4/4, 3/4, 2/4, 6/8)
3. **Start/Stop**: Click the play button to start or stop the metronome
4. **Visual Feedback**: Watch the beat indicators light up in sync with the audio

## Technical Details

### Architecture
- **UI Framework**: Avalonia UI 11.3.2 for cross-platform desktop support
- **Audio Library**: NetCoreAudio 2.0.0 for cross-platform audio playback and recording
- **MVVM Pattern**: CommunityToolkit.Mvvm 8.2.1 with observable properties and relay commands
- **Threading**: Proper UI thread handling for responsive interface
- **Target Framework**: .NET 9.0

### Key Components

#### MVVM with CommunityToolkit.Mvvm
```csharp
[ObservableProperty]
private int currentBPM = 120;

[RelayCommand]
private void IncreaseBPM()
{
    if (CurrentBPM < 200)
    {
        CurrentBPM += 5;
        _metronomeService.BPM = CurrentBPM;
    }
}
```

#### Avalonia UI Data Binding
```xml
<TextBlock Text="{Binding CurrentBPM}" 
           FontSize="72" 
           FontWeight="Bold"/>
           
<Button Command="{Binding PlayStopCommand}"/>
```

#### Service Pattern Architecture
```csharp
public MainWindowViewModel()
{
    _metronomeService = new MetronomeService();
    _metronomeService.BeatOccurred += OnBeatOccurred;
    _metronomeService.PlayingStateChanged += OnPlayingStateChanged;
}
```

#### Visual Beat Indicators
```xml
<Ellipse Width="20" Height="20" 
         Fill="{Binding Beat1Color}" 
         Margin="5"/>
```

#### Thread-Safe UI Updates
```csharp
Avalonia.Threading.Dispatcher.UIThread.Post(() =>
{
    ResetBeatIndicators();
    // Update UI safely from background thread
});
```

### Dependencies
- **Avalonia**: Cross-platform .NET UI framework
- **NetCoreAudio**: Cross-platform audio library supporting both playback and recording
- **CommunityToolkit.Mvvm**: Source generators for MVVM pattern implementation

## Project Structure

```
MetronomeApp/
├── Services/           # Core metronome logic
├── ViewModels/         # MVVM view models
├── Views/              # UI layouts
├── Models/             # Data models
└── Assets/             # Application resources
```

## Future Enhancements

- Custom audio file support for click sounds
- Tap tempo functionality
- Preset saving and loading
- Advanced time signature support
- Visual metronome mode
- **Tuner functionality**: The existing NetCoreAudio library supports microphone recording, making it feasible to add pitch detection and tuning capabilities

## License

[license information here]