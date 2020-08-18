using UnityEngine;

public interface IConsumable {

    void Consume(Crab crab);
    Sprite GetSprite();
    float GetMovementSpeed();
    float GetLifetime();
}
