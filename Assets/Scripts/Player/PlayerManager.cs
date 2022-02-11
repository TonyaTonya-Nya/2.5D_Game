using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int numberOfCoins;
    public TextMeshProUGUI numberOfCoinsText;

    public float currentHealth = 100;
    public Slider healthBar;

    public static bool gameOver;
    public static bool winLevel;

    public GameObject gameOverPanel;

    public float timer = 0;

    private TriggerManager tMag;
    private int flagNum;

    void Start()
    {
        numberOfCoins = 0;
        gameOver = winLevel = false;

        tMag = GameObject.FindGameObjectWithTag("TriggerManager").GetComponent<TriggerManager>();

        for (int i = 0; i < tMag.trigger.Length; i++)
        {
            if (tMag.trigger[i].name == "Clear")
            {
                flagNum = i;
            }
        }
    }

    void Update()
    {

        if (currentHealth <= 100)
        {
            currentHealth += 0.005f;
        }

        //Display the number of coins
        numberOfCoinsText.text = "coins:" + numberOfCoins;

        //Update the slider value
        healthBar.value = currentHealth;

        //game over
        if (currentHealth < 0 && currentHealth > -20)
        {
            gameOver = true;
            gameOverPanel.SetActive(true);
            currentHealth = -500;
        }

        if (tMag.trigger[flagNum].status)
        {
            winLevel = true;
            timer += Time.deltaTime;
            if (timer > 3)
            {
                int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
                if (nextLevel == 4)
                    SceneManager.LoadScene(0);

                if (PlayerPrefs.GetInt("ReachedLevel", 1) < nextLevel)
                    PlayerPrefs.SetInt("ReachedLevel", nextLevel);

                SceneManager.LoadScene(nextLevel);
            }
        }


        //if( FindObjectsOfType<Enemy>().Length ==0)
        //{
        //    //Win Level
        //    winLevel = true;
        //    timer += Time.deltaTime;
        //    if(timer > 3)
        //    {
        //        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        //        if (nextLevel == 4)
        //            SceneManager.LoadScene(0);

        //        if(PlayerPrefs.GetInt("ReachedLevel", 1) < nextLevel)
        //            PlayerPrefs.SetInt("ReachedLevel", nextLevel);

        //        SceneManager.LoadScene(nextLevel);
        //    }

        //}
    }


    public void Damage(float n)
    {
        currentHealth -= n;
    }
}
