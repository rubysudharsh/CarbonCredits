using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCarbon
{
	public class Items
	{
		public string Name { get; set; }
		public bool CanRelist { get; set; }
		public IList<string> Promotions { get; set; }
	}
}
