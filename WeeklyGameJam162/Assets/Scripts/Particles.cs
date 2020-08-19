using UnityEngine;
using System.Collections;

public class Particles : MonoBehaviour {

	private void Start() {
		StartCoroutine(Disappear());
	}
	private IEnumerator Disappear() {
		yield return new WaitForSeconds(0.4f);
		gameObject.SetActive(false);
	}
}
