using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCommerce.Main.Events
{
	public class AddedToCartEvent : PubSubEvent<AddedToCartEvent.Payload>
	{
		public class Payload
		{
			public int ProductId { get; set; }
			public int Quantity { get; set; }
		}
	}

	public class SubtractedToCartEvent : PubSubEvent<SubtractedToCartEvent.Payload>
	{
		public class Payload
		{
			public int ProductId { get; set; }
			public int Quantity { get; set; }
		}
	}

	public class RemovedFromCartEvent : PubSubEvent<RemovedFromCartEvent.Payload>
	{
		public class Payload
		{
			public int ProductId { get; set; }
		}
	}
}
