﻿using System;
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

		[TestMethod]
		public void GetPositioningTest()
		{
			Positioning result = (new PositioningRepo()).GetPositioning();

			Assert.IsNotNull(result);
			Assert.AreEqual(0, result.x);
			Assert.AreEqual(0, result.y);
			Assert.AreEqual(Positioning.CompassHeading.N, result.heading);
		}

	}
}