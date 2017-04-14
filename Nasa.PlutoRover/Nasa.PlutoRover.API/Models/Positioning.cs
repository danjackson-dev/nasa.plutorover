using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nasa.PlutoRover.API.Models
{
	public class Positioning
	{

		public int x { get; set; } = 0;
		public int y { get; set; } = 0;
		public CompassHeading heading { get; set; } = CompassHeading.N;

		public enum CompassHeading
		{
			N,
			E,
			S,
			W
		}

	}
}