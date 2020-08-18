using UnityEngine;
using System.Collections;
using System;

public class Trash : IConsumable {

    private Sprite trashSprite;
    private float damageAmount;
    private float slowAmount;
    private float slowDuration;
    private float lifetime;
    private float movementSpeed;

    public Trash(Sprite trashSprite, float damageAmount, float slowAmount, float slowDuration, float lifetime, float movementSpeed) {
        this.trashSprite = trashSprite != null ? trashSprite : throw new ArgumentNullException(nameof(trashSprite));
        this.damageAmount = damageAmount;
        this.slowAmount = slowAmount;
        this.slowDuration = slowDuration;
        this.lifetime = lifetime;
        this.movementSpeed = movementSpeed;
    }
    public float GetMovementSpeed() {
        return movementSpeed;
    }
    public float GetLifetime() {
        return lifetime;
    }
    public void Consume(Crab crab) {
        crab.ChangeHealth(-damageAmount);
        crab.GetSpeedBoost(slowDuration, -slowAmount);
    }
    public Sprite GetSprite() {
        return trashSprite;
    }
}
