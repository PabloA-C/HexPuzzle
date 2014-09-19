using UnityEngine;
using System.Collections;

public class MapCreator : MonoBehaviour
{

		
		void Start ()
		{
				
	
		
		}
	
		public void run ()
		{
				
				int rand = (int)Mathf.Floor (Random.Range (0, 7));
				GameObject tile = null;
				switch (rand) {
				case 0:
						tile = Instantiate (Resources.Load ("Grass")) as GameObject;
						break;
				case 1:
						tile = Instantiate (Resources.Load ("Start")) as GameObject;
						break;
				case 2:
						tile = Instantiate (Resources.Load ("Finish")) as GameObject;
						break;
				case 3:
						tile = Instantiate (Resources.Load ("Straight")) as GameObject;
						break;
				case 4:
						tile = Instantiate (Resources.Load ("Turn")) as GameObject;
						break;
				case 5:
						tile = Instantiate (Resources.Load ("SharpTurn")) as GameObject;
						break;
				case 6:
						tile = Instantiate (Resources.Load ("Water")) as GameObject;
						break;
			
			
				}

				int rand2 = (int)Mathf.Floor (Random.Range (1, 7));
		
				if (rand != 1 && rand != 2) {
						tile.transform.Rotate (0, 0, rand2 * 60, Space.World);
				}
			

		
		}
	

	

}
