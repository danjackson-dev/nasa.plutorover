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
		private bool collisionDetected = false;

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

		private int _x = 0;
		public int x
		{
			get { return _x; }
			set { _x = value;  _preX = value; }
		}
		private int _y = 0;
		public int y
		{
			get { return _y; }
			set { _y = value; _preY = value; }
		}
		public CompassHeading heading { get; set; } = CompassHeading.N;

		public enum CompassHeading
		{
			N,
			E,
			S,
			W
		}

		#endregion

		#region Private Properties

		private int _preX;
		private int preX
		{
			get
			{
				return _preX;
			}
			set
			{
				_preX = value;
				this.x = !DetectCollision() ? _preX : this.x;
				_preX = this.x;
			}
		}
		private int _preY;
		private int preY {
			get
			{
				return _preY;
			}
			set
			{
				_preY = value;
				this.y = !DetectCollision() ? _preY : this.y;
				_preY = this.y;
			}
		}

		#endregion

		#region Public Methods

		public bool MoveRover(string directions)
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

				// Completed all commands until a collision has been detected, return false
				if (this.collisionDetected)
				{
					return false;
				}

			}

			// Completed all commands successfully
			return true;

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
						if (this.preY == gridMaxY)
						{
							this.preY = 0;
							break;
						}
						this.preY++; break;
					}
					// Move east
					if (this.heading == CompassHeading.E)
					{
						// Implement wrapping
						if (this.preX == gridMaxX) { this.preX = 0; break; }
						this.preX++; break;
					}
					// Move south
					if (this.heading == CompassHeading.S)
					{
						// Implement wrapping
						if (this.preY == 0) { this.preY = gridMaxY; break; }
						this.preY--; break;
					}
					// Move west
					if (this.heading == CompassHeading.W)
					{
						// Implement wrapping
						if (this.preX == 0) { this.preX = gridMaxX; break; }
						this.preX--; break;
					}
					break;

				// Move backwards
				case 'B':
					if (this.heading == CompassHeading.N)
					{
						if (this.preY == 0) { this.preY = gridMaxY; break; }
						this.preY--; break;
					}
					if (this.heading == CompassHeading.E)
					{
						if (this.preX == 0) { this.preX = gridMaxX; break; }
						this.preX--; break;
					}
					if (this.heading == CompassHeading.S)
					{
						if (this.preY == gridMaxY) { this.preY = 0; break; }
						this.preY++; break;
					}
					if (this.heading == CompassHeading.W)
					{
						if (this.preX == gridMaxX) { this.preX = 0; break; }
						this.preX++; break;
					}
					break;
			}

		}

		private bool DetectCollision()
		{
			// An array of collisions on the map
			string[] collisionArray = new string[] { "50,50", "25,25" };

			string nextCoordinates = this.preX.ToString() + "," + this.preY.ToString();

			for (int i = 0; i < collisionArray.Length; i++)
			{
				if (nextCoordinates == collisionArray[i])
				{
					this.collisionDetected = true;
					return true;
				}
			}

			return false;
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
				this.preX = this.x;
				this.preY = this.y;
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