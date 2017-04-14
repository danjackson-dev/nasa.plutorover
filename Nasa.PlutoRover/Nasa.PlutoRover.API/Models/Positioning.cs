using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;

namespace Nasa.PlutoRover.API.Models
{
	public class Positioning
	{

		#region Constructors

		public Positioning() {}

		public Positioning(bool initializeFromJson)
		{
			if (initializeFromJson)
			{
				LoadFromJson();
			}
		}

		#endregion

		#region Public Properties

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

		#endregion

		#region Public Methods

		public void MoveRover(char direction)
		{

			switch (direction.ToString().ToUpper())
			{
				case "F":
					this.y++;
					break;
				case "B":
					this.y--;
					break;
				case "L":
					RotateRover('L');
					break;
				case "R":
					RotateRover('R');
					break;
			}

		}

		private void RotateRover(char direction)
		{

			switch (direction)
			{
				case 'L':
					if (this.heading == CompassHeading.N)
					{
						this.heading = CompassHeading.W;
					}
					else
					{
						this.heading--;
					}
					break;
				case 'R':
					if (this.heading == CompassHeading.W )
					{
						this.heading = CompassHeading.N;
					}
					else
					{
						this.heading++;
					}
					break;
			}

		}
		
		#endregion

		#region Private Methods

		private string _jsonFileLocation = "c:\\nasa\\plutorover\\position.json";

		/// <summary>Load properties from a persistent Json file</summary>
		private void LoadFromJson()
		{

			CheckJsonFile();

			using (StreamReader sr = new StreamReader(_jsonFileLocation))
			{
				string json = sr.ReadToEnd();
				dynamic fromJson = JsonConvert.DeserializeObject<dynamic>(json);
				this.x = fromJson.x;
				this.y = fromJson.y;
				this.heading = fromJson.heading;
			}

		}

		/// <summary>Save the current instance to a Json file for persistence</summary>
		/// <returns>Returns boolean if save is successful or not</returns>
		public bool SaveToJson()
		{

			CheckJsonFile();

			try
			{
				var serialisedObj = JsonConvert.SerializeObject(this);
				using (StreamWriter sw = new StreamWriter(_jsonFileLocation))
				{
					sw.Write(serialisedObj);
				}

				return true;
			}
			catch
			{
				return false;
			}

		}

		/// <summary>Check the existence of the Json file, and create if it does not</summary>
		private void CheckJsonFile()
		{

			if (!Directory.Exists("c:\\Nasa")) { Directory.CreateDirectory("c:\\Nasa"); }
			if (!Directory.Exists("c:\\Nasa\\PlutoRover")) { Directory.CreateDirectory("c:\\Nasa\\PlutoRover"); }
			if (!File.Exists(_jsonFileLocation)) { File.Create(_jsonFileLocation); }

		}

		#endregion

	}
}