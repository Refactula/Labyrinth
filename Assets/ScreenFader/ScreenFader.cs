using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour {
	
	public bool InitialState = true;
	public bool FadeIn = false;
	public float FadeTime = 2.0f;
	public Color FadeColor = new Color (0.0f, 0.0f, 0.0f, 1.0f);
	public Material FadeMaterial = null;
    
	private List<ScreenFadeControl> fadeControls = new List<ScreenFadeControl> ();
	private bool fadeControlsEnabled;
	private float fadeValue;

	void Start() {
		initCameras ();
		setFadeValue (InitialState ? 1f : 0f);
	}

	private void initCameras() {
		foreach (ScreenFadeControl fadeControl in fadeControls) {
			Destroy (fadeControl);
		}
		fadeControls.Clear ();
		foreach (Camera c in Camera.allCameras) {
			var fadeControl = c.gameObject.AddComponent<ScreenFadeControl> ();
			fadeControl.fadeMaterial = FadeMaterial;
			fadeControls.Add (fadeControl);
		}
	}

	public void Update () {
		float targetFadeValue = FadeIn ? 1f : 0f;
		if (fadeValue != targetFadeValue) {
			float delta = targetFadeValue - fadeValue;
			setFadeValue(fadeValue + Mathf.Sign (delta) * Mathf.Min(Time.deltaTime / FadeTime, Mathf.Abs(delta)));
		}
	}

	private void setFadeValue(float value) {
		FadeColor.a = fadeValue = Mathf.Clamp01(value);
		FadeMaterial.color = FadeColor;
		setFadeControlsEnabled(value >= 0.001);
	}

	private void setFadeControlsEnabled(bool value) {
		if (fadeControlsEnabled == value) {
			return;
		}			
		fadeControlsEnabled = value;
		foreach (ScreenFadeControl fadeControl in fadeControls) {
			fadeControl.enabled = value;
		}
	}

}
