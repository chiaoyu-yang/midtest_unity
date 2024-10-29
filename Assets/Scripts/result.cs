using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class result : MonoBehaviour
{
    public GameObject gameNameText, usernameText, usernumberText, guessnumberText, quitButton;
    
    void Start()
    {
        gameNameText.GetComponent<Text>().text = global.gameName;
        usernameText.GetComponent<Text>().text = global.username;
        usernumberText.GetComponent<Text>().text = global.usernumber;
        guessnumberText.GetComponent<Text>().text = "總共猜了" + global.guessnumber + "次";
    }

    public void click_quitButton()
    {
        // 關閉遊戲
        Application.Quit();
    }
}