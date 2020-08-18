using UnityEngine;

public class Buff : MonoBehaviour {

    private IConsumable buff;
    private Crab crab;
    private SpriteRenderer spriteRenderer;
    private Vector3 movementDirection;

    private readonly float rotationSpeed = 45f;
    private float timer;

    public void SetBuff(IConsumable buff, Vector3 movementDirection) {
        this.buff = buff;
        spriteRenderer.sprite = this.buff.GetSprite();
        this.movementDirection = movementDirection;
        timer = buff.GetLifetime();
        crab = FindObjectOfType<Crab>();
    }
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        crab = FindObjectOfType<Crab>();
    }
    private void Update() {
        if(timer > 0) {
            timer -= Time.deltaTime;
        }
        else if(gameObject.activeInHierarchy) {
            gameObject.SetActive(false);
        }
        transform.Translate(movementDirection.normalized * buff.GetMovementSpeed() * Time.deltaTime, Space.World);
        transform.Rotate(new Vector3(0f, 0f, rotationSpeed * Time.deltaTime), Space.Self);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            buff.Consume(crab);
            gameObject.SetActive(false);
        }
    }
}
