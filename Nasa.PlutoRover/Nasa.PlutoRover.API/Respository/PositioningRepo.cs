using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nasa.PlutoRover.API.Models;

namespace Nasa.PlutoRover.API.Respository
{
	public class PositioningRepo
	{

		public Positioning GetPositioning()
		{
			return new Positioning();
		}

		public Positioning MoveRover(char direction)
		{
			throw new NotImplementedException();
		}

	}
}