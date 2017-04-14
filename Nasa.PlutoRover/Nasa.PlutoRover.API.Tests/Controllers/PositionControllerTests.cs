using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nasa.PlutoRover.API.Controllers;
using System.Web.Http;
using System.Web.Http.Results;

namespace Nasa.PlutoRover.API.Tests
{
	[TestClass]
	public class PositionControllerTests
	{
		[TestMethod]
		public void GetCurrentPositionTest()
		{

			var controller = new PositionController();
			var result = controller.GetCurrentPosition() as OkNegotiatedContentResult<object>;

			Assert.IsNotNull(result);

		}
	}
}
