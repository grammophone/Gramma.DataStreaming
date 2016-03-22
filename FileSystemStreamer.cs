using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gramma.DataStreaming
{
	/// <summary>
	/// A provider of streams from the file system.
	/// </summary>
	public class FileSystemStreamer : Streamer
	{
		#region Private fields

		private string baseFilename = String.Empty;

		#endregion

		#region Public properties

		/// <summary>
		/// The base filename upon the which the specified 
		/// filenames in <see cref="OpenReadStream" /> and <see cref="OpenWriteStream"/> are
		/// relative, or the empty string. Default is empty string.
		/// </summary>
		public string BaseFilename
		{
			get
			{
				return baseFilename;
			}
			set
			{
				if (value == null) throw new ArgumentNullException(nameof(value));
				baseFilename = value;
			}
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Open a stream for reading.
		/// </summary>
		/// <param name="filename">The filename, prepended by <see cref="BaseFilename"/> if that is not empty.</param>
		/// <returns>Returns a stream for reading.</returns>
		/// <exception cref="FileNotFoundException">Thrown when the file was not found.</exception>
		/// <exception cref="IOException">Thrown when there is a general reading error.</exception>
		public override Stream OpenReadStream(string filename)
		{
			if (filename == null) throw new ArgumentNullException(nameof(filename));

			var fullFilename = GetFullFilename(filename);

			return new FileStream(fullFilename, FileMode.Open);
		}

		/// <summary>
		/// Open a stream for writing.
		/// </summary>
		/// <param name="filename">The filename, prepended by <see cref="BaseFilename"/> if that is not empty.</param>
		/// <param name="overwrite">If true and the file exists, overwrite, else throw an <see cref="IOException"/>.</param>
		/// <returns>Returns a stream for writing.</returns>
		/// <exception cref="IOException">Thrown when there is a general writing error.</exception>
		public override Stream OpenWriteStream(string filename, bool overwrite = true)
		{
			if (filename == null) throw new ArgumentNullException(nameof(filename));

			var fullFilename = GetFullFilename(filename);

			var mode = overwrite ? FileMode.Create : FileMode.CreateNew;

			return new FileStream(fullFilename, mode);
		}

		#endregion

		#region Private methods

		private string GetFullFilename(string filename)
		{
			if (String.IsNullOrEmpty(this.BaseFilename)) return filename;

			return Path.Combine(this.BaseFilename, filename);
		}

		#endregion
	}
}
