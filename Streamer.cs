using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataStreaming
{
	/// <summary>
	/// Abstract base class for streamers.
	/// </summary>
	public abstract class Streamer : IStreamer
	{
		#region Private fields

		private string key;

		#endregion

		#region Public properties

		/// <summary>
		/// The key of the streamer, used in streamer qualified filenames 
		/// as the prefix before "|".
		/// </summary>
		public string Key
		{
			get
			{
				return key;
			}
			set
			{
				if (value == null) throw new ArgumentNullException(nameof(value));
				key = value;
			}
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Open a stream for reading.
		/// </summary>
		/// <param name="filename">The filename according to the provider's conventions.</param>
		/// <returns>Returns a stream for reading.</returns>
		/// <exception cref="FileNotFoundException">Thrown when the file was not found.</exception>
		/// <exception cref="IOException">Thrown when there is a general reading error.</exception>
		public abstract Stream OpenReadStream(string filename);

		/// <summary>
		/// Open a stream for writing.
		/// </summary>
		/// <param name="filename">The filename according to the provider's conventions.</param>
		/// <param name="overwrite">If true and the file exists, overwrite, else throw an <see cref="IOException"/>.</param>
		/// <returns>Returns a stream for writing.</returns>
		/// <exception cref="IOException">Thrown when there is a general writing error.</exception>
		public abstract Stream OpenWriteStream(string filename, bool overwrite = true);

		#endregion
	}
}
