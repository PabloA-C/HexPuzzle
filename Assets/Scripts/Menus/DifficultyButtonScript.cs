using UnityEngine;
using System.Collections;

public class DifficultyButtonScript : MonoBehaviour
{

		public int difficulty;

		void OnMouseDown ()
		{
		
				Application.LoadLevel ("Puzzle");
		}
}
