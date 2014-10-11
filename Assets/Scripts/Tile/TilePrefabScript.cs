using UnityEngine;
using System.Collections;

public class TilePrefabScript : MonoBehaviour
{

		private string name;
		private string type;
		private Vector3 position;
		private int rotation;
		private TilePrefabScript tilePrefabScript;
		private GameObject resource;

		// Use this for initialization
		void Start ()
		{
					
		}
	
		
		// Update is called once per frame
		void Update ()
		{

				if (Input.GetKeyDown ("space")) {
						Destroy (resource);
						resource = Instantiate (Resources.Load ("Grass")) as GameObject;	
						resource.name = "Sprite";
						resource.transform.position = transform.position;
						resource.transform.Rotate (0, 0, 0, Space.World);
						resource.transform.transform.parent = transform;


				}
						
		}

		public void setTile (string type)
		{

				resource = Instantiate (Resources.Load (type)) as GameObject;	
				resource.name = "Sprite";
				resource.transform.position = transform.position;
				resource.transform.Rotate (0, 0, 0, Space.World);
				resource.transform.transform.parent = transform;

		}
}
/*


	GameObject tileObject = Instantiate (Resources.Load (type)) as GameObject;
	tileObject.name = name;
	tileObject.transform.position = new Vector3 (tileWidth * x + oddRowOffset, tileHeight * y + gridVerticalOffset, 0);
	tileObject.transform.Rotate (0, 0, tile.getRotation () * 60, Space.World);
	tileObject.transform.parent = tileGrid.transform;

resource = Instantiate (Resources.Load ("Grass")) as GameObject;
				resource.name = name;
				resource.transform.position = new Vector3 (0, 10, 0);
				resource.transform.Rotate (0, 0, 0, Space.World);




 */