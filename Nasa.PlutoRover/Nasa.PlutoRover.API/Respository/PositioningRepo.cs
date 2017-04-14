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

			Positioning position = GetPositioning();

			switch (direction.ToString().ToUpper())
			{
				case "F":
					position.y++;
					break;
				case "B":
					position.y--;
					break;
			}

			return position;

		}

	}
}