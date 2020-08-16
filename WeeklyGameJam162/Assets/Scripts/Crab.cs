using UnityEngine;
using System.Collections;
using System;

public class Crab : MonoBehaviour {

    private readonly KeyCode leftKey = KeyCode.A;
    private readonly KeyCode rightKey = KeyCode.D;
    private readonly KeyCode rotateLeftKey = KeyCode.Q;
    private readonly KeyCode rotateRightKey = KeyCode.E;

    private readonly float defaultMovementSpeed = 9f;
    private float movementSpeed;
    private float rotationSpeed = 90f;

    private Coroutine speedBoost;
    private bool speedBoosted;

    private void OnEnable() {
        speedBoosted = false;
        movementSpeed = defaultMovementSpeed;
    }
    private void Update() {
		GetInput();
	}
    private void OnDisable() {
        StopAllCoroutines();
    }
    private void GetInput() {
        if(Input.GetKey(leftKey)) {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime, Space.Self);
        }
        if(Input.GetKey(rightKey)) {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime, Space.Self);
        }
        if(Input.GetKey(rotateLeftKey)) {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime, Space.Self);
        }
        if(Input.GetKey(rotateRightKey)) {
            transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime, Space.Self);
        }
    }
    public void GetSpeedBoost(float seconds, float amount) {
        if(speedBoosted) {
            StopCoroutine(speedBoost);
        }
        speedBoost = StartCoroutine(SpeedBoost(seconds, amount));
    }
    private IEnumerator SpeedBoost(float seconds, float amount) {
        speedBoosted = true;
        movementSpeed += amount;
        yield return new WaitForSeconds(seconds);
        movementSpeed -= amount;
        speedBoosted = false;
    }
}
