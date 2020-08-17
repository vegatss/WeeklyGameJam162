using UnityEngine;

public class Buff : MonoBehaviour {

    private IConsumable buff;
    private Crab crab;
    private SpriteRenderer spriteRenderer;

    public void SetBuff(IConsumable buff) {
        this.buff = buff;
        spriteRenderer.sprite = this.buff.GetSprite();
    }
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        crab = FindObjectOfType<Crab>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            buff.Consume(crab);
            gameObject.SetActive(false);
        }
    }
}
