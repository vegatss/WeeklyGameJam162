using System;
using UnityEngine;

public class Food : IConsumable {

    private Sprite foodSprite;
    private float healthAmount;
    private float satietyAmount;
    private float speedBoost;
    private float speedDuration;
    private float lifetime;
    private float movementSpeed;

    public Food(Sprite foodSprite, float healthAmount, float satietyAmount, float speedBoost, float speedDuration, float lifetime, float movementSpeed) {
        this.foodSprite = foodSprite != null ? foodSprite : throw new ArgumentNullException(nameof(foodSprite));
        this.healthAmount = healthAmount;
        this.satietyAmount = satietyAmount;
        this.speedBoost = speedBoost;
        this.speedDuration = speedDuration;
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
        crab.ChangeSatiety(satietyAmount);
        crab.ChangeHealth(healthAmount);
        crab.GetSpeedBoost(speedDuration, speedBoost);
    }
    public Sprite GetSprite() {
        return foodSprite;
    }
}
