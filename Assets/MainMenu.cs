using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadButton(string number)
    {
        SceneManager.LoadScene("Stage" + number);
    }

    public void Stage01()
    {
        SceneManager.LoadScene("Stage01");
    }

    public void Stage02()
    {
        SceneManager.LoadScene("Stage02");
    }
}
