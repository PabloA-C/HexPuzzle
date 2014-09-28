using UnityEngine;
using System.Collections;

public class Coordinate
{
		private int x, y;

		// Use this for initialization
		void Start ()
		{
		
		}

		public Coordinate (int x, int y)
		{
				this.x = x;
				this.y = y;

		}
	
		public	int getX ()
		{
				return x;
		}

		public	int getY ()
		{
				return y;
		}


}
