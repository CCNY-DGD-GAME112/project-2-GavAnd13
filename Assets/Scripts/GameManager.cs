using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform playerTF;
    public Camera cam;
    public GameObject dudePre;
    public int dudeCount = 50;
    public GameObject weepingPre;
    public GameObject walkerPre;
    public float enemyTimer = 4f;

    public Vector2 spawnBoundNeg = new Vector2(-25, -25);
    public Vector2 spawnBoundPos = new Vector2(25, 25);
    public float spawnProtRange = 5f;

    public TextMeshProUGUI scoreText;

    public int score = 0;

    float timer = 0;

    private void Awake()
    {
        if (instance)
            Destroy(gameObject);
        instance = this;
    }
    private void Start()
    {
        for (int i = 1; i < dudeCount; i++)
            spawnDude();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            int enemyToSpawn = Random.Range(1, 4);
            if (enemyToSpawn > 1)
                spawnWeeping();
            else
                spawnWalker();
            timer = enemyTimer;
        }
    }

    void spawnDude()
    {
        GameObject dudeClone = Instantiate(dudePre, GenSpawnPosition(), Quaternion.identity);
        dudeClone.transform.GetChild(0).GetComponent<PersonOpacity>().playerTF = playerTF;
    }

    void spawnWeeping()
    {
        GameObject enemyClone = Instantiate(weepingPre, GenSpawnPosition(), Quaternion.identity);
        enemyClone.transform.GetChild(0).GetComponent<WeepingAngel>().playerTF = playerTF;
    }

    void spawnWalker()
    {
        GameObject enemyClone = Instantiate(walkerPre, GenSpawnPosition(), Quaternion.identity);
        enemyClone.transform.GetChild(0).GetComponent<Walker>().playerTF = playerTF;
    }

    Vector3 GenSpawnPosition()
    {
        float x = Random.Range(spawnBoundNeg.x, spawnBoundPos.x);
        while (x > playerTF.position.x - spawnProtRange && x < playerTF.position.x + spawnProtRange)
            x = Random.Range(spawnBoundNeg.x, spawnBoundPos.x);
        float z = Random.Range(spawnBoundNeg.y, spawnBoundPos.y);
        while (z > playerTF.position.z - spawnProtRange && z < playerTF.position.z + spawnProtRange)
            z = Random.Range(spawnBoundNeg.y, spawnBoundPos.y);
        return new Vector3(x, 0.5f, z);
    }

    public void Lose()
    {
        SceneManager.LoadScene("Lose");
    }
}
