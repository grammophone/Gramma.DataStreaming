using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grammophone.DataStreaming
{
	/// <summary>
	/// Contract for providers of data streaming.
	/// </summary>
	public interface IStreamer : GenericContentModel.IKeyedElement<string>
	{
		/// <summary>
		/// Open a stream for reading.
		/// </summary>
		/// <param name="filename">The filename according to the provider's conventions.</param>
		/// <returns>Returns a stream for reading.</returns>
		/// <exception cref="FileNotFoundException">Thrown when the file was not found.</exception>
		/// <exception cref="IOException">Thrown when there is a general reading error.</exception>
		Stream OpenReadStream(string filename);

		/// <summary>
		/// Open a stream for writing.
		/// </summary>
		/// <param name="filename">The filename according to the provider's conventions.</param>
		/// <param name="overwrite">If true and the file exists, overwrite, else throw an <see cref="IOException"/>.</param>
		/// <returns>Returns a stream for writing.</returns>
		/// <exception cref="IOException">Thrown when there is a general writing error.</exception>
		Stream OpenWriteStream(string filename, bool overwrite = true);
	}
}
