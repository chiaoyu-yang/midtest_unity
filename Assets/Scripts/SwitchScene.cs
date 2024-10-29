using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour
{
    public GameObject inputName, inputNumber, hinttext;

    public void click_start_button()
    {
        SceneManager.LoadScene("profile");
    }

    public void click_back2home()
    {
        SceneManager.LoadScene("home");
    }

    public void click_next2gamehome_button()
    {
        if (inputName.GetComponent<InputField>().text == "" || inputNumber.GetComponent<InputField>().text == "")
        {
            hinttext.SetActive(true);
            return;
        }

        global.username = inputName.GetComponent<InputField>().text;
        global.usernumber = inputNumber.GetComponent<InputField>().text;
        SceneManager.LoadScene("game_home");
    }

    public void click_next2gameone_button()
    {
        SceneManager.LoadScene("GuessNumber");
        global.gameName = "猜數字";
    }

    public void click_next2gametwo_button()
    {
        SceneManager.LoadScene("game2");
        global.gameName = "終極密碼";
    }

    public void click_next2result()
    {
        SceneManager.LoadScene("result");
    }

    // 回到遊戲主頁
    public void click_back2gamehome()
    {
        SceneManager.LoadScene("game_home");
    }
}