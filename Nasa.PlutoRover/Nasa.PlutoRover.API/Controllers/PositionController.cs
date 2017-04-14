using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nasa.PlutoRover.API.Controllers
{
    public class PositionController : ApiController
    {

		public IHttpActionResult GetCurrentPosition()
		{
			return Ok(new object());
		}

    }
}
