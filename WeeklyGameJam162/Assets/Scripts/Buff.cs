using UnityEngine;

public class Buff : MonoBehaviour {

    private ScriptableBuff buff;
    private Crab crab;

    public void SetBuff(ScriptableBuff buff) {
        this.buff = buff;
    }
    private void Start() {
        crab = FindObjectOfType<Crab>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            buff.Activate(crab);
        }
    }
}
