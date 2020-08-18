using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Crab : MonoBehaviour {

    private readonly KeyCode leftKey = KeyCode.A;
    private readonly KeyCode rightKey = KeyCode.D;
    private readonly KeyCode rotateLeftKey = KeyCode.Q;
    private readonly KeyCode rotateRightKey = KeyCode.E;
    private readonly KeyCode rotateRightAlt = KeyCode.RightArrow;
    private readonly KeyCode rotateLeftAlt = KeyCode.LeftArrow;

    private readonly float defaultMovementSpeed = 9f;
    private float movementSpeed;
    private readonly float rotationSpeed = 120f;

    private Coroutine speedBoost;
    private bool speedBoosted;

    private readonly float maxHealth = 100f;
    private readonly float maxSatiety = 100f;
    private float health;
    private float satiety;
    private readonly float hungrinessInterval = 0.2f;

    public Image healthBar;
    public Image satietyBar;
    public GameMaster gameMaster;

    private Animator crabAnimator;

    private void OnEnable() {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        crabAnimator = GetComponent<Animator>();
        health = maxHealth;
        satiety = maxSatiety;
        healthBar.fillAmount = 1f;
        satietyBar.fillAmount = 1f;
        speedBoosted = false;
        movementSpeed = defaultMovementSpeed;
        StartCoroutine(GetHungry());
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
        if(Input.GetKey(rotateLeftKey) || Input.GetKey(rotateLeftAlt)) {
            movingChecks++;
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime, Space.Self);
        }
        if(Input.GetKey(rotateRightKey) || Input.GetKey(rotateRightAlt)) {
            movingChecks++;
            transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime, Space.Self);
        }
        if(movingChecks > 0) {
            crabAnimator.SetBool("moving", true);
        }
        else {
            crabAnimator.SetBool("moving", false);
        }
        if(transform.position.x > 14f) {
            transform.position = new Vector3(14f, transform.position.y, 0f);
        }
        if(transform.position.x < -14f) {
            transform.position = new Vector3(-14f, transform.position.y, 0f);
        }
        if(transform.position.y > 8f) {
            transform.position = new Vector3(transform.position.x, 8f, 0f);
        }
        if(transform.position.y < -8f) {
            transform.position = new Vector3(transform.position.x, -8f, 0f);
        }
    }
    public void GetSpeedBoost(float seconds, float amount) {
        if(gameObject.activeInHierarchy) {
            if(speedBoosted) {
                movementSpeed = defaultMovementSpeed;
                StopCoroutine(speedBoost);
            }
            speedBoost = StartCoroutine(SpeedBoost(seconds, amount));
        }        
    }
    private IEnumerator SpeedBoost(float seconds, float amount) {
        speedBoosted = true;
        movementSpeed += amount;
        yield return new WaitForSeconds(seconds);
        movementSpeed = defaultMovementSpeed;
        speedBoosted = false;
    }
    private IEnumerator GetHungry() {
        yield return new WaitForSeconds(hungrinessInterval);
        ChangeSatiety(-1f);
        if(satiety <= 0) {
            satiety = 0;
            ChangeHealth(-1);
        }
        if(gameObject.activeInHierarchy) {
            StartCoroutine(GetHungry());
        }        
    }
    public void ChangeHealth(float amount) {        
        health += Mathf.Clamp(amount, -maxHealth, maxHealth - health);
        if(health <= 0) {
            Die();
        }
        healthBar.fillAmount = health / maxHealth;
    }
    public void ChangeSatiety(float amount) {
        satiety += Mathf.Clamp(amount, -maxSatiety, maxSatiety - satiety);
        satietyBar.fillAmount = satiety / maxSatiety;
    }
    private void Die() {
        gameObject.SetActive(false);
        gameMaster.GameOver();
    }
}
