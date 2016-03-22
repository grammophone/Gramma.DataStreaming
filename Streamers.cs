using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gramma.DataStreaming
{
	/// <summary>
	/// A collection of <see cref="IStreamer"/> instances,
	/// indexed by their <see cref="GenericContentModel.IKeyedElement{K}.Key"/>.
	/// </summary>
	public class Streamers : GenericContentModel.Map<string, IStreamer>
	{
	}
}
