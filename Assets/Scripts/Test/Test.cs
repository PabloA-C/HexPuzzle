using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
		
				GameObject tileObject1 = Instantiate (Resources.Load ("Prefabs/TilePrefab")) as GameObject;
				tileObject1.transform.position = new Vector3 (-2, 0, 0);
				tileObject1.GetComponent<TilePrefabScript> ().setTile ("Start");
				tileObject1.name = "Uno";
				tileObject1.transform.transform.parent = GameObject.Find ("Test").transform;
		
				GameObject tileObject2 = Instantiate (Resources.Load ("Prefabs/TilePrefab")) as GameObject;
				tileObject2.transform.position = new Vector3 (0, 0, 0);
				tileObject2.GetComponent<TilePrefabScript> ().setTile ("Grass");
				tileObject2.name = "Dos";
				tileObject2.transform.transform.parent = GameObject.Find ("Test").transform;

				GameObject tileObject3 = Instantiate (Resources.Load ("Prefabs/TilePrefab")) as GameObject;
				tileObject3.transform.position = new Vector3 (2, 0, 0);
				tileObject3.GetComponent<TilePrefabScript> ().setTile ("Finish");
				tileObject3.name = "Tres";
				tileObject3.transform.transform.parent = GameObject.Find ("Test").transform;
				

				

	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
