using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    // Use this for initialization

    public static GameController current;
    public GameObject enemyHover;
    public GameObject enemySniper;
    public TextController infoText;
    public TextController enemyCounterText;
    public TextController ammoText;
    public float intermissionTime;
    private bool intermission;
    private bool gameStart;
    private int wave;

    private bool gameOver;

    private int enemies;

    void Awake()
    {
        current = this;
        wave = 0;
        intermission = true;

        
    }
	void Start () {
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(1.0f);
        infoText.UpdateText("Defend yourself!\nHold for automatic fire!\nDouble tap to fire railgun!");
        infoText.FadeIn();
        yield return new WaitForSeconds(3.0f);
        infoText.FadeOut();
        yield return new WaitForSeconds(3.0f);
        while (gameOver == false)
        {
            yield return StartCoroutine(Intermission());
            enemies = (5 + 5 * wave) * (int)Mathf.Pow(1.1f, wave);
            ReduceEnemies(0);
            yield return StartCoroutine(WaveTop(enemyHover, enemies, 1.0f * Mathf.Pow(0.9f, wave)));
            while(intermission == false && gameOver == false)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    IEnumerator Intermission()
    {
        wave++;
        infoText.UpdateText("Wave " + wave);
        infoText.FadeIn();
        enemyCounterText.FadeOut();
        if (wave != 1)
        {

            ammoText.FadeOut();
        }
        yield return new WaitForSeconds(ammoText.fadeSpeed);
        if (wave != 1)
        { 
            PlayerController.current.increaseAmmo(2 + wave);
            PlayerController.current.increaseHealth(1);
        }
        yield return new WaitForSeconds(intermissionTime - ammoText.fadeSpeed);
        intermission = false;
        infoText.FadeOut();
        UpdateAmmoText();
    }

    public void UpdateAmmoText()
    {
        ammoText.UpdateText("Railgun: " + PlayerController.current.ammo + "\nArmor: " + PlayerController.current.health);
        if (ammoText.IsEmpty())
        {
            ammoText.FadeIn();
        }
    }

    public void UpdateEnemyText()
    {
        enemyCounterText.UpdateText("Enemies: " + enemies);
        if (enemyCounterText.IsEmpty())
        {
            enemyCounterText.FadeIn();
        }
    }





    IEnumerator WaveTop(GameObject enemy, int enemyCount, float spawnDelay)
    {
        while(enemyCount > 0)
        {
            if (Random.value > 0.8)
            {
                Instantiate(enemySniper, new Vector3(Random.Range(-5, +6), 0.5f, 22f), Quaternion.Euler(0, 0, 0));
                enemyCount--;
                if (enemyCount < 1)
                {
                    break;
                }
            }
            Instantiate(enemy, new Vector3(Random.Range(-5, +6), 0.5f,22f), Quaternion.Euler(0,0,0));
            
            enemyCount--;
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    IEnumerator GameRestart(float delay)
    {
        yield return new WaitForSeconds(delay);
        Application.LoadLevel(Application.loadedLevel);
    }

    public void ReduceEnemies(int number)
    {
        enemies = enemies - number;
        UpdateEnemyText();
        if(enemies<1)
        {
            intermission = true;
            
        }
    }

    public void GameOver ()
    {
        infoText.FadeIn();
        infoText.UpdateText("Game Over");
        gameOver = true;
        StartCoroutine(GameRestart(5.0f));
    }
}
