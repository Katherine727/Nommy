using UnityEngine;
using System.Collections;

public class FoodSpawner : MonoBehaviour {

	public float TimeDelay = 1; //Czas w SEKUNDACH na pojawienie sie nowego jedzenia
	public GameObject foodPrefab; //Prefab fooda PRZECIAGNIETY z folderu prefabs! Nie obiekt ze sceny!

	float timeCounter;

	object spawnedObject;

	// Use this for initialization
	void Start () {
		SpawnObject();
	}
	
	// Update is called once per frame
	void Update () {
		if(spawnedObject.Equals(null)){ //gdy nie ma jedzonko bo ktos je zjadl
			timeCounter -= Time.deltaTime; //odliczamy czas

			if(timeCounter<=0){ //i gdy trzeba tworzymy nowe jedzonko
				SpawnObject();
			}
		}
	}

	void SpawnObject() {
		spawnedObject = Instantiate(foodPrefab,this.transform.position,Quaternion.identity);
		(spawnedObject as GameObject).transform.parent = this.gameObject.transform;
		timeCounter = TimeDelay;

		AfterSpawn();
	}

	void AfterSpawn() {
		//TODO some particles or something
	}
}
