using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;

public class GameMaster : MonoBehaviour {

	public Sprite[] projectileSprites;
	public GameObject buff;
    // North, East, South, West
    public Transform[] spawnPoints;
    public TextMeshProUGUI scoreText;
    //public Buff[] buffs;
    private readonly float maxSpawnTime = 3.53f;
    private float currentSpawnTime;
    private IConsumable[] consumables;
    private int score;
    private bool spawning;
    public Crab crab;
    public GameObject statusPanel;
    public GameObject restartPanel;
    public GameObject startPanel;
    public TextMeshProUGUI endScore;

    private void Start() {
        consumables = new IConsumable[8];
        consumables[0] = new Food(projectileSprites[0], 30f, 30f, 3f, 6f, 30f, 0.8f);
        consumables[1] = new Trash(projectileSprites[1], 20f, 3f, 3f, 8f, 4f);
        consumables[2] = new Trash(projectileSprites[2], 24f, 1f, 1f, 12f, 2f);
        consumables[3] = new Trash(projectileSprites[3], 16f, 2f, 4f, 11f, 5f);
        consumables[4] = new Food(projectileSprites[4], 20f, 25f, 5f, 4f, 15f, 1.4f);
        consumables[5] = new Food(projectileSprites[5], 15f, 15f, 2f, 9f, 40f, 0.7f);
        consumables[6] = new Trash(projectileSprites[6], 32f, 1f, 6f, 8f, 6f);
        consumables[7] = new Trash(projectileSprites[7], 24f, 4f, 2f, 7f, 8f);
    }
    public void StartLevel() {
        Buff[] objectsInLevel = FindObjectsOfType<Buff>();
        if(objectsInLevel.Length > 0) {
            for(int i = 0; i < objectsInLevel.Length; i++) {
                objectsInLevel[i].gameObject.SetActive(false);
            }
        }        
        startPanel.SetActive(false);
        restartPanel.SetActive(false);
        statusPanel.SetActive(true);
        crab.gameObject.SetActive(true);
        spawning = true;
        score = 0;
        currentSpawnTime = 0f;
        StartCoroutine(ScoreCounter());
        StartCoroutine(SpawnBuff());        
    }
    private IEnumerator ScoreCounter() {
        currentSpawnTime = Mathf.Clamp(currentSpawnTime + 0.01f, 0.01f, maxSpawnTime);
        yield return new WaitForSeconds(1);
        ++score;
        scoreText.text = score.ToString();        
        if(gameObject.activeInHierarchy) {
            StartCoroutine(ScoreCounter());
        }
    }
    public void GameOver() {
        restartPanel.SetActive(true);
        statusPanel.SetActive(false);
        endScore.text = score.ToString();
        spawning = false;
        StopAllCoroutines();
    }
    private IEnumerator SpawnBuff() {
        // 0 is fish++
        // 1 is paper trash
        // 2 is cigarette butts
        // 3 is plastic straws
        // 4 is worms++
        // 5 is moss++
        // 6 is plastic bottles
        // 7 is wrappers        
        float spawnCooldown = -Mathf.Log(currentSpawnTime, 10) * 1.7f + 1f;
        int buffType = Random.Range(0, 8);
        Vector2 spawnPoint;        
        if(buffType == 0 || buffType == 5) {
            spawnPoint = new Vector3(Random.Range(-15f, 15f), spawnPoints[0].position.y);
        }
        else {
            int randomSwitch = Random.Range(0, 4);
            switch(randomSwitch) {
                case 0:
                    spawnPoint = new Vector3(Random.Range(-15f, 15f), spawnPoints[0].position.y);
                    break;
                case 1:
                    spawnPoint = new Vector3(spawnPoints[1].position.x, Random.Range(-9f, 9f));
                    break;
                case 2:
                    spawnPoint = new Vector3(Random.Range(-15f, 15f), spawnPoints[2].position.y);
                    break;
                case 3:
                    spawnPoint = new Vector3(spawnPoints[3].position.x, Random.Range(-9f, 9f));
                    break;
                default:
                    spawnPoint = new Vector3(spawnPoints[1].position.x, Random.Range(-9f, 9f));
                    break;
            }
        }
        Vector3 movementDirection = (Random.insideUnitCircle * 8) - spawnPoint;        
        GameObject clone = ObjectPooler.objectPooler.GetPooledObject("Buff");
        if(clone) {
            clone.GetComponent<Buff>().SetBuff(consumables[buffType], movementDirection);
            clone.transform.position = spawnPoint;
            clone.transform.rotation = Quaternion.identity;
            clone.SetActive(true);
        }
        yield return new WaitForSeconds(spawnCooldown);
        if(spawning) {
            StartCoroutine(SpawnBuff());
        }        
    }
}
