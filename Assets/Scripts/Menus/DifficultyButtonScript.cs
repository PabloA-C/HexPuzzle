using UnityEngine;
using System.Collections;

public class DifficultyButtonScript : MonoBehaviour
{
		
		public int difficulty;

		void OnMouseDown ()
		{
				GameObject.Find ("Difficulty").GetComponent<DifficultyScript> ().setDifficulty (difficulty);
				
				Application.LoadLevel ("Puzzle");
				
		}
	
		
}
