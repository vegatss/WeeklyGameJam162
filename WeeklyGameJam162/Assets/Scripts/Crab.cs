using UnityEngine;
using System.Collections;
using System;

public class Crab : MonoBehaviour {

    private readonly KeyCode leftKey = KeyCode.A;
    private readonly KeyCode rightKey = KeyCode.D;
    private readonly KeyCode rotateLeftKey = KeyCode.Q;
    private readonly KeyCode rotateRightKey = KeyCode.E;

    private readonly float defaultMovementSpeed = 9f;
    private readonly float defaultRotationSpeed = 90f;
    private float movementSpeed;
    private float rotationSpeed = 90f;

    private Coroutine speedBoost;
    private bool speedBoosted;

    private readonly float maxHealth = 100f;
    private readonly float maxSatiety = 100f;
    private float health;
    private float satiety;

    private Animator crabAnimator;

    private void OnEnable() {
        crabAnimator = GetComponent<Animator>();
        health = maxHealth;
        satiety = maxSatiety;
        speedBoosted = false;
        movementSpeed = defaultMovementSpeed;
        rotationSpeed = defaultRotationSpeed;
    }
    private void Update() {
        GetInput();
	}
    private void OnDisable() {
        StopAllCoroutines();
    }
    private void GetInput() {
        int movingChecks = 0;
        if(Input.GetKey(leftKey)) {
            movingChecks++;
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime, Space.Self);
        }
        if(Input.GetKey(rightKey)) {
            movingChecks++;
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime, Space.Self);
        }
        if(Input.GetKey(rotateLeftKey)) {
            movingChecks++;
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime, Space.Self);
        }
        if(Input.GetKey(rotateRightKey)) {
            movingChecks++;
            transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime, Space.Self);
        }
        if(movingChecks > 0) {
            crabAnimator.SetBool("moving", true);
        }
        else {
            crabAnimator.SetBool("moving", false);
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
        rotationSpeed += amount * 10f;
        yield return new WaitForSeconds(seconds);
        movementSpeed -= amount;
        rotationSpeed -= amount * 10f;
        speedBoosted = false;
    }
    public void ChangeHealth(float amount) {
        health += Mathf.Clamp(amount, -maxHealth, maxHealth - health);
    }
    public void ChangeSatiety(float amount) {
        satiety += Mathf.Clamp(amount, -maxSatiety, maxSatiety - satiety);
    }
}
