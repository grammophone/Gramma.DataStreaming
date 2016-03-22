using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gramma.DataStreaming.Configuration
{
	/// <summary>
	/// Provides the available <see cref="IStreamer"/> instances.
	/// </summary>
	public class StreamingSettings
	{
		#region Private fields

		private Streamers streamers = new Streamers();

		#endregion

		#region Public properties

		/// <summary>
		/// Collection of the configured streamers, 
		/// indexed by their <see cref="GenericContentModel.IKeyedElement{K}.Key"/>.
		/// </summary>
		public Streamers Streamers
		{
			get
			{
				return streamers;
			}
			set
			{
				if (value == null) throw new ArgumentNullException(nameof(value));
				streamers = value;
			}
		}

		#endregion
	}
}
