﻿using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface IShippingService
	{
		void Ship(List<IShippable> items, List<string> shipmentNotice);
	}
}
