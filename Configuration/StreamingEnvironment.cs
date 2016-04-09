using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grammophone.Configuration;

namespace Grammophone.DataStreaming.Configuration
{
	/// <summary>
	/// Provides the configured <see cref="IStreamer"/> implementations
	/// from <see cref="StreamingSettings"/> specified in XAML
	/// and resolves requested streams.
	/// The XAML file is specified in a <see cref="Grammophone.Configuration.XamlSettingsSection"/>
	/// with name "dataStreamingSection".
	/// </summary>
	public static class StreamingEnvironment
	{
		#region Private fields

		private static XamlConfiguration<StreamingSettings> xamlConfiguration = 
			new XamlConfiguration<StreamingSettings>("dataStreamingSection");

		private static FileSystemStreamer defaultStreamer = new FileSystemStreamer();

		#endregion

		#region Public properties

		/// <summary>
		/// The settings instance defined in the XAML file.
		/// </summary>
		public static StreamingSettings Settings
		{
			get
			{
				return xamlConfiguration.Settings;
			}
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Open a stream for reading from an <see cref="IStreamer"/> defined in <see cref="Settings"/>.
		/// </summary>
		/// <param name="streamerQualifiedFilename">
		/// An optionally compound filename with 2 parts separated by "|", whose first part is
		/// the <see cref="GenericContentModel.IKeyedElement{K}.Key"/> of the streamer
		/// and second part is passed to the streamer. If the filename ordeinary and not compound,
		/// a default <see cref="FileSystemStreamer"/> is used with the filename parameter as is.
		/// </param>
		/// <returns>Returns the requested <see cref="Stream"/>.</returns>
		public static Stream OpenReadStream(string streamerQualifiedFilename)
		{
			if (streamerQualifiedFilename == null) throw new ArgumentNullException(nameof(streamerQualifiedFilename));

			IStreamer streamer;
			string filename;

			ResolveStreamer(streamerQualifiedFilename, out streamer, out filename);

			return streamer.OpenReadStream(filename);
		}

		/// <summary>
		/// Open a stream for reading from an <see cref="IStreamer"/> defined in <see cref="Settings"/>.
		/// </summary>
		/// <param name="streamerKey">The <see cref="GenericContentModel.IKeyedElement{K}.Key"/> of the streamer.</param>
		/// <param name="filename">The filename to be passed to the streamer.</param>
		/// <returns>Returns the requested <see cref="Stream"/>.</returns>
		public static Stream OpenReadStream(string streamerKey, string filename)
		{
			if (streamerKey == null) throw new ArgumentNullException(nameof(streamerKey));
			if (filename == null) throw new ArgumentNullException(nameof(filename));

			var streamer = GetStreamerByKey(streamerKey);

			return streamer.OpenReadStream(filename);
		}

		/// <summary>
		/// Open a stream for writing from an <see cref="IStreamer"/> defined in <see cref="Settings"/>.
		/// </summary>
		/// <param name="streamerQualifiedFilename">
		/// An optionally compound filename with 2 parts separated by "|", whose first part is
		/// the <see cref="GenericContentModel.IKeyedElement{K}.Key"/> of the streamer
		/// and second part is passed to the streamer. If the filename ordeinary and not compound,
		/// a default <see cref="FileSystemStreamer"/> is used with the filename parameter as is.
		/// </param>
		/// <param name="overwrite">If true, the streamer will overwrite any existing file.</param>
		/// <returns>Returns the requested <see cref="Stream"/>.</returns>
		public static Stream OpenWriteStream(string streamerQualifiedFilename, bool overwrite = true)
		{
			if (streamerQualifiedFilename == null) throw new ArgumentNullException(nameof(streamerQualifiedFilename));

			IStreamer streamer;
			string filename;

			ResolveStreamer(streamerQualifiedFilename, out streamer, out filename);

			return streamer.OpenWriteStream(filename, overwrite);
		}

		/// <summary>
		/// Open a stream for writing from an <see cref="IStreamer"/> defined in <see cref="Settings"/>.
		/// </summary>
		/// <param name="streamerKey">The <see cref="GenericContentModel.IKeyedElement{K}.Key"/> of the streamer.</param>
		/// <param name="filename">The filename to be passed to the streamer.</param>
		/// <param name="overwrite">If true, the streamer will overwrite any existing file.</param>
		/// <returns>Returns the requested <see cref="Stream"/>.</returns>
		public static Stream OpenWriteStream(string streamerKey, string filename, bool overwrite = true)
		{
			if (streamerKey == null) throw new ArgumentNullException(nameof(streamerKey));
			if (filename == null) throw new ArgumentNullException(nameof(filename));

			var streamer = GetStreamerByKey(streamerKey);

			return streamer.OpenWriteStream(filename, overwrite);
		}

		#endregion

		#region Private methods

		private static void ResolveStreamer(string streamerQualifiedFilename, out IStreamer streamer, out string filename)
		{
			var filenameParts = streamerQualifiedFilename.Split('|');

			if (filenameParts.Length < 2)
			{
				streamer = defaultStreamer;
				filename = streamerQualifiedFilename;
			}

			string streamerKey = filenameParts[0];
			filename = filenameParts[1];

			streamer = GetStreamerByKey(streamerKey);
		}

		private static IStreamer GetStreamerByKey(string streamerKey)
		{
			if (String.IsNullOrEmpty(streamerKey)) return defaultStreamer;

			var settings = Settings;

			if (!settings.Streamers.ContainsKey(streamerKey))
				throw new StreamerException($"No streamer has been configured having key '{streamerKey}'.");

			return settings.Streamers[streamerKey];
		}

		#endregion
	}
}
