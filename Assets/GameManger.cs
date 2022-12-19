using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    public Move player;
    public TMP_Text health;
    public TMP_Text coin;
    public TMP_Text time;
    public int MaxTime;
    int currentTime;

    private void Start()
    {
        StartCoroutine(TimeFlow());
    }

    private void Update()
    {
        health.text = "Health : " + player.Health.ToString();
        coin.text = "Coin : " + player.coinCount.ToString();
        time.text = "Time : " + currentTime.ToString();

        if (GameOverProcessor())
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public bool GameOverProcessor()
    {
        if (player.gameObject.transform.position.y < -10)
        {
            return true;
        }
        if (player.Health == 0)
        {
            return true;
        }
        if (currentTime == 0)
        {
            return true;
        }
        return false;
    }

    IEnumerator TimeFlow()
    {
        currentTime = MaxTime;
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currentTime--;
        }
    }
}
