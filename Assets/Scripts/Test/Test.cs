using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{

				TileScript uno = new TileScript (new Coordinate (0, 0), "Start");
				TileScript dos = new TileScript (new Coordinate (1, 0), "Turn");
				TileScript tres = new TileScript (new Coordinate (2, 0), "Finish");

				dos.rotation = 1;

				GameObject tileObject1 = Instantiate (Resources.Load ("Prefabs/TilePrefab")) as GameObject;
				tileObject1.transform.position = new Vector3 (-2, 0, 0);
				tileObject1.GetComponent<TilePrefabScript> ().setTile (uno);
				tileObject1.name = "Uno";
				tileObject1.transform.transform.parent = GameObject.Find ("Test").transform;
		
				GameObject tileObject2 = Instantiate (Resources.Load ("Prefabs/TilePrefab")) as GameObject;
				tileObject2.transform.position = new Vector3 (0, 0, 0);
				tileObject2.GetComponent<TilePrefabScript> ().setTile (dos);
				tileObject2.name = "Dos";
				tileObject2.transform.transform.parent = GameObject.Find ("Test").transform;

				GameObject tileObject3 = Instantiate (Resources.Load ("Prefabs/TilePrefab")) as GameObject;
				tileObject3.transform.position = new Vector3 (2, 0, 0);
				tileObject3.GetComponent<TilePrefabScript> ().setTile (tres);
				tileObject3.name = "Tres";
				tileObject3.transform.transform.parent = GameObject.Find ("Test").transform;
				

				

	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
