using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public float FollowingDistance;
	public GameObject Target;

	private NavMeshAgent navMeshAgent;

	void Start () {
		if (Target == null) {
			Target = GameObject.FindGameObjectWithTag ("Player");
		}
		navMeshAgent = GetComponent<NavMeshAgent> ();
	}

	void Update() {
		follow ();
	}

	private void follow() {
		if (Target == null) {
			return;
		}
		PlayerScript player = Target.GetComponent<PlayerScript> ();
		if (player != null && !player.IsAlive ()) {
			return;
		}
		Vector3 targetPosition = Target.transform.position;
		if (Vector3.Distance (targetPosition, transform.position) <= FollowingDistance) {
			navMeshAgent.SetDestination (targetPosition);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject == Target) {
			PlayerScript playerScript = other.gameObject.GetComponent<PlayerScript> ();
			if (playerScript != null) {
				playerScript.die ();
			}
		}
	}

}
