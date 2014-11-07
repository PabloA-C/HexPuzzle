using UnityEngine;
using System.Collections;

public class DifficultyScript : MonoBehaviour
{
		int difficulty;
		
		void Start ()
		{
				DontDestroyOnLoad (this);
		}
		
		public int getDifficulty ()
		{
				return difficulty;
		}
		
		public void setDifficulty (int difficulty)
		{
				this.difficulty = difficulty;
		}
}
