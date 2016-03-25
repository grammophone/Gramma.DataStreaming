# Gramma.DataStreaming
This .NET library abstracts streams of data. It is used to provide a standard .NET `Stream` for reading or writing based on configuration.

The providers of streams implement the `IStreamer` interface. These are registered in an instance of the configuration class `StreamingSettings`, inside its `Streamers` property. The `StreamingSettings` instance is defined in a XAML file, whose path is set in the `settingsXamlPath` attribute of a configuration section of type `XamlSettingsSection` named "dataStreamingSection" in the application's standard config file. This procedure is described in the required library [Gramma.Configuration](https://github.com/grammophone/Gramma.Configuration).

Once the `IStreamer` providers are registered, they can be used via the `StreamingEnvironment` static class. One can then use the `OpenReadStream` and `OpenWriteStream` overloads. The overloads taking two arguments, one for the streamers `Key` property and one for the (possibly virtual) filename are straightforward. The overloads taking a single `streamerQualifiedFilename` argument optionally combine the previous arguments into a single '<key>|<filename>' form. If the argument doesn't specify a key, being of simple '<filename>' form, a default streamer is used which uses the computer's file system.

This project depends on the following projects being in sibling directories:
* [Gramma.Configuration](https://github.com/grammophone/Gramma.Configuration)
* [Gramma.GenericContentModel](https://github.com/grammophone/Gramma.GenericContentModel)
