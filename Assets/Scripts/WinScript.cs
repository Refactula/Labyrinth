using UnityEngine;
using System.Collections;

public class WinScript : MonoBehaviour {

	public GameObject ExitWall;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			gameObject.transform.parent.gameObject.SetActive (false);
			ExitWall.SetActive (true);
			other.gameObject.GetComponent<PlayerScript> ().win ();
		}
	}

}
