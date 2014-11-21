using UnityEngine;
using System.Collections;

public class DifficultyScript : MonoBehaviour
{
		int difficulty;
		
		private AudioClip song;
		private AudioClip select1;
		private AudioClip select2;
		private AudioClip click1;
		private AudioClip click2;
		
		void Start ()
		{
				DontDestroyOnLoad (this);
				Screen.fullScreen = false;
				select1 = Resources.Load ("Sounds/select1")as AudioClip;
				select2 = Resources.Load ("Sounds/select2")as AudioClip;
				click1 = Resources.Load ("Sounds/click1")as AudioClip;
				click2 = Resources.Load ("Sounds/click2")as AudioClip;
	
				
		}
		
		
		public void playSelection ()
		{
				
				int randomVal = (int)Mathf.Ceil (Random.Range (0, 2));
		
				if (randomVal == 1) {
						audio.PlayOneShot (select1);
				} else {
						audio.PlayOneShot (select2);
				}
		}
		
		public void playPlacement ()
		{
				int randomVal = (int)Mathf.Ceil (Random.Range (0, 2));
		
				if (randomVal == 1) {
						audio.PlayOneShot (click1);
				} else {
						audio.PlayOneShot (click2);
				}
		
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
