using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public int SpawnCount = 3;

	void Start () {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		int remaining = SpawnCount;
		for (int i = 0; i < enemies.Length; i++) {
			double chance = 1f * remaining / (enemies.Length - i);
			if (Random.value <= chance) {
				remaining--;
			} else {
				enemies [i].SetActive (false);
			}
		}
	}

}
