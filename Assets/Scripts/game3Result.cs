using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class game3Result : MonoBehaviour
{
    public GameObject gameNameText, usernameText, usernumberText, gameResultText, gameTimeText, gameAnsText, quitButton;

    void Start()
    {
        gameNameText.GetComponent<Text>().text = global.gameName;
        usernameText.GetComponent<Text>().text = global.username;
        usernumberText.GetComponent<Text>().text = global.usernumber;
        gameResultText.GetComponent<Text>().text = global.gemeResult + "，猜了" + global.guessnumber + "次";
        gameTimeText.GetComponent<Text>().text = global.gameTime + "秒";
        gameAnsText.GetComponent<Text>().text = global.gameAns;
    }

    public void click_quitButton()
    {
        // 關閉遊戲
        Application.Quit();
    }
}