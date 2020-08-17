using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public Sprite[] projectileSprites;
	public GameObject buff;
    //public Buff[] buffs;
    private Vector3 spawnPoint = new Vector3(5f, 5f, 0);

    private void Start() {
        //buffs = new Buff[8];
        //buffs[0].SetBuff(new Food(projectileSprites[0], 20f, 40f));
        StartCoroutine(SpawnBuff());
    }
    private IEnumerator SpawnBuff() {
        yield return new WaitForSeconds(1f);
        GameObject clone = ObjectPooler.objectPooler.GetPooledObject("Buff");
        if(clone) {
            clone.GetComponent<Buff>().SetBuff(new Food(projectileSprites[0], 20f, 40f));
            clone.transform.position = spawnPoint;
            clone.transform.rotation = Quaternion.identity;
            clone.SetActive(true);
        }
    }
}
