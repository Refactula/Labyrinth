using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerScript : MonoBehaviour {

	public float DeathOffset = -1;
	public float DeathAngle = 90;
	public float DeathDuration = 1.5f;
	public float RestartDelay = 3f;
	public float WinDelay = 20f;

	private bool isAlive = true;
	private float deathAnimationProgress = 0;

	void Update() {
		if (!isAlive && deathAnimationProgress < 1) {
			float step = Mathf.Min(1f - deathAnimationProgress, Time.deltaTime / DeathDuration);
			deathAnimationProgress += step;
			gameObject.transform.Rotate (0, 0, step * 90);
			gameObject.transform.Translate (new Vector3 (0, step * DeathOffset, 0));
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	public bool IsAlive() {
		return isAlive;
	}

	public void die() {
		if (!isAlive) {
			return;
		}
		isAlive = false;
		GetComponent<FirstPersonController> ().enabled = false;
		StartCoroutine ("RestartLater");
	}

	public void win() {
		StartCoroutine ("WinFadeLater");
		StartCoroutine ("WinLater");
	}

	IEnumerator RestartLater() {
		yield return new WaitForSeconds(RestartDelay);
		SceneManager.LoadScene("LabirynthScene");
	}

	IEnumerator WinLater() {
		yield return new WaitForSeconds(WinDelay);
		Application.Quit();
	}

	IEnumerator WinFadeLater() {
		ScreenFader fader = GameObject.FindGameObjectWithTag ("WinFade").GetComponent<ScreenFader>();
		if (WinDelay > fader.FadeTime) {
			yield return new WaitForSeconds (WinDelay - fader.FadeTime);
		}
		fader.FadeIn = true;
	}
	
}
