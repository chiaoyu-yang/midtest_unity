using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class game2 : MonoBehaviour
{   
    // 終極密碼
    public GameObject rangeText, inputField, enterButton, responseText, ResultButton;
    
    // 初始min=1, max=99;
    public int min = 1, max = 99, answer, guessNumber=0;

    void Start()
    {
        // 隨機產生答案
        answer = Random.Range(2, 99);  // 正確地放在 Start() 裡
        rangeText.GetComponent<Text>().text = min + " ~ " + max;
        responseText.GetComponent<Text>().text = "開始猜";

        ResultButton.SetActive(false);
    }

    // 迴圈，只要還沒猜中就繼續
    public void click_enterButton()
    {
        // 如果guess不是數字，就不執行
        if (!int.TryParse(inputField.GetComponent<InputField>().text, out int guess) || ResultButton.activeSelf)
        {
            return;
        }


        if(guess == answer)
        {
            guessNumber++;
            responseText.GetComponent<Text>().text = "Correct!!";
            ResultButton.SetActive(true);
            global.guessnumber = guessNumber.ToString();
        }
        else if (guess > min && guess < max)
        {
            if (guess > answer)
            {
                max = guess;
            }
            else
            {
                min = guess;
            }

            rangeText.GetComponent<Text>().text = min + " ~ " + max;
            responseText.GetComponent<Text>().text = "Try again!!";
            guessNumber++;
        }
    }

    // 清空輸入框
    public void click_clearButton()
    {
        inputField.GetComponent<InputField>().text = "";
    }

    // 回到遊戲主頁
    public void click_back2gamehome()
    {
        SceneManager.LoadScene("game_home");
    }
}
