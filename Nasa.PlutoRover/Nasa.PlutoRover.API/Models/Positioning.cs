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

		#region Constants

		private const string _jsonFileLocation = "c:\\nasa\\plutorover\\position.json";
		private const int gridMaxX = 99;	// 99 as grid position begins at 0 (100x100 grid)
		private const int gridMaxY = 99;

		#endregion


		#region Constructors

		public Positioning()
		{
			LoadFromJson();
		}

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

		public void MoveRover(string directions)
		{

			foreach (char direction in directions.ToCharArray())
			{

				switch (direction)
				{
					case 'F':
						DriveRover(direction);
						break;
					case 'B':
						DriveRover(direction);
						break;
					case 'L':
						RotateRover(direction);
						break;
					case 'R':
						RotateRover(direction);
						break;
				}

			}

		}

		#endregion

		#region Private Methods

		private void DriveRover(char direction)
		{

			switch (direction)
			{
				// Move forward
				case 'F':
					// Move north
					if (this.heading == CompassHeading.N)
					{
						// Implement wrapping.
						if (this.y == gridMaxY) { this.y = 0; break; }
						this.y++; break;
					}
					// Move east
					if (this.heading == CompassHeading.E)
					{
						// Implement wrapping
						if (this.x == gridMaxX) { this.x = 0; break; }
						this.x++; break;
					}
					// Move south
					if (this.heading == CompassHeading.S)
					{
						// Implement wrapping
						if (this.y == 0) { this.y = gridMaxY; break; }
						this.y--; break;
					}
					// Move west
					if (this.heading == CompassHeading.W)
					{
						// Implement wrapping
						if (this.x == 0) { this.x = gridMaxX; break; }
						this.x--; break;
					}
					break;

				// Move backwards
				case 'B':
					if (this.heading == CompassHeading.N)
					{
						if (this.y == 0) { this.y = gridMaxY; break; }
						this.y--; break;
					}
					if (this.heading == CompassHeading.E)
					{
						if (this.x == 0) { this.x = gridMaxX; break; }
						this.x--; break;
					}
					if (this.heading == CompassHeading.S)
					{
						if (this.y == gridMaxY) { this.y = 0; break; }
						this.y++; break;
					}
					if (this.heading == CompassHeading.W)
					{
						if (this.x == gridMaxX) { this.x = 0; break; }
						this.x++; break;
					}
					break;
			}

		}

		/// <summary>Rotate the rover left and right</summary>
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
					if (this.heading == CompassHeading.W)
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