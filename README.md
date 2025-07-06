# MetronomeApp

A cross-platform metronome application built with Avalonia UI and .NET 9.

## Features

- **Adjustable BPM**: Set tempo from 40 to 300 beats per minute in 5 BPM increments
- **Multiple Time Signatures**: Support for 4/4, 3/4, 2/4, and 6/8 time signatures
- **Visual Beat Indicators**: Color-coded beat visualization with accent highlighting
- **Audio Feedback**: System beep audio cues (Windows) with console output fallback
- **Modern MVVM Architecture**: Built with CommunityToolkit.Mvvm for clean separation of concerns

## Requirements

- .NET 9.0 or later
- Windows

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

- **UI Framework**: Avalonia UI for cross-platform desktop support
- **Audio**: NetCoreAudio library with system beep fallback
- **Architecture**: MVVM pattern with CommunityToolkit.Mvvm
- **Threading**: Proper UI thread handling for responsive interface

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

## License

[license information here]