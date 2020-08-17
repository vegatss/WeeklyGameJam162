using System;
using UnityEngine;

public class Food : IConsumable {

    public Sprite foodSprite;
    public float healthAmount = 10f;
    public float satietyAmount = 10f;

    public Food(Sprite foodSprite, float healthAmount, float satietyAmount) {
        this.foodSprite = foodSprite != null ? foodSprite : throw new ArgumentNullException(nameof(foodSprite));
        this.healthAmount = healthAmount;
        this.satietyAmount = satietyAmount;
    }

    public void Consume(Crab crab) {
        crab.ChangeSatiety(satietyAmount);
        crab.ChangeHealth(healthAmount);
    }

    public Sprite GetSprite() {
        return foodSprite;
    }
}
