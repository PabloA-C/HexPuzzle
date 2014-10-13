using UnityEngine;
using System.Collections;

public class Coordinate
{
		private int x, y;

		// Use this for initialization
		void Start ()
		{
		
		}

		public Coordinate ()
		{

		}
		public Coordinate (int x, int y)
		{
				this.x = x;
				this.y = y;

		}

		public void setX (int x)
		{
				this.x = x;
		}

		public void setY (int y)
		{
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
