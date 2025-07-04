using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface IShippable
	{
		double Weight { get; }
		string GetName();
		double GetWeight();
	}
}
