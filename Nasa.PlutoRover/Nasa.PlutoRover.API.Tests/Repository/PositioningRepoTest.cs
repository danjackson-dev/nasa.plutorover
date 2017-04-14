using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nasa.PlutoRover.API.Models;
using Nasa.PlutoRover.API.Respository;

namespace Nasa.PlutoRover.API.Tests.Repository
{
	[TestClass]
	public class PositioningRepoTest
	{
		#region Repo Tests

		[TestMethod]
		public void GetPositioningTest()
		{
			Positioning result = (new PositioningRepo()).GetPositioning();

			Assert.IsNotNull(result);
			Assert.AreEqual(0, result.x);
			Assert.AreEqual(0, result.y);
			Assert.AreEqual(Positioning.CompassHeading.N, result.heading);
		}
		
		#endregion

		#region Object Method Tests

		[TestMethod]
		public void SaveJsonFile()
		{
			Positioning obj = new Positioning();
			bool result = obj.SaveToJson();

			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void MoveRover_Forward()
		{
			Positioning result = new Positioning();
			result.MoveRover('F');
			Assert.AreEqual(1, result.y);
		}

		[TestMethod]
		public void MoveRover_Backwards()
		{
			Positioning result = new Positioning();
			result.MoveRover('B');
			Assert.AreEqual(-1, result.y);
		}

		[TestMethod]
		public void MoveRover_Left()
		{
			Positioning result = new Positioning();
			result.MoveRover('L');
			Assert.AreEqual(Positioning.CompassHeading.W, result.heading);
		}

		[TestMethod]
		public void MoveRover_Right()
		{
			Positioning result = new Positioning();
			result.MoveRover('R');
			Assert.AreEqual(Positioning.CompassHeading.E, result.heading);
		}

		#endregion

	}
}
