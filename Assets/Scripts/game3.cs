using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class game3 : MonoBehaviour
{
    public GameObject panel1, panel2, panel3, panel4, panel5, inputField, enterButton, hintButton, hintPanel;

    public string answer, hintName;

    public int attemptCount=0;


    void Start()
    {
        int random_number = Random.Range(0, 3);
        switch (random_number)
        {
            case 0:
                answer = "sleep";
                hintName = "睡覺";
                break;
            case 1:
                answer = "happy";
                hintName = "開心";
                break;
            case 2:
                answer = "angry";
                hintName = "生氣";
                break;
        }

        // 預設只顯示panel1
        panel1.SetActive(true);
        panel2.SetActive(false);
        panel3.SetActive(false);
        panel4.SetActive(false);
        panel5.SetActive(false);  
        hintButton.SetActive(false); 
    }

    // 當按下enterButton時，先檢查inputField是不是五個英文字母，再檢查是否正確
    public void click_enterButton()
    {
        string guess = inputField.GetComponent<InputField>().text;

        // 檢查輸入是否為五個英文字母
        if (guess.Length != 5 || !System.Text.RegularExpressions.Regex.IsMatch(guess, @"^[a-zA-Z]+$") || attemptCount >= 5)
        {
            return;
        }

        // 更新當前panel的顯示文字
        GameObject currentPanel = GetPanelByAttempt(attemptCount);
        for (int i = 0; i < 5; i++)
        {
            currentPanel.transform.GetChild(i).GetComponentInChildren<Text>().text = guess[i].ToString().ToUpper();

            // 如果猜測字母正確，則將該字母顏色改為綠色
            if (guess[i].ToString().ToLower() == answer[i].ToString().ToLower())
            {
                currentPanel.transform.GetChild(i).GetComponentInChildren<Text>().color = Color.green;
            }
            // 如果猜測字母正確，但是位置錯誤，則將該字母顏色改為黃色
            else if (answer.Contains(guess[i].ToString().ToLower()))
            {
                currentPanel.transform.GetChild(i).GetComponentInChildren<Text>().color = Color.yellow;
            }
        }

        if (guess.ToLower() == answer)
        {
            Debug.Log("恭喜你猜對了！");
            attemptCount = 5;
        }
        else
        {
            attemptCount++;
            ShowNextPanel(attemptCount);
        }

        if (attemptCount == 3)
        {
            hintButton.SetActive(true);
        }
    }

    // 清空輸入框
    public void click_clearButton()
    {
        inputField.GetComponent<InputField>().text = "";
    }

    // 根據嘗試次數顯示對應的panel，保留之前顯示的panel
    private void ShowNextPanel(int attempt)
    {
        switch (attempt)
        {
            case 1:
                panel2.SetActive(true);
                break;
            case 2:
                panel3.SetActive(true);
                break;
            case 3:
                panel4.SetActive(true);
                break;
            case 4:
                panel5.SetActive(true);
                break;
        }
    }

    // 根據嘗試次數獲取當前應顯示的panel
    private GameObject GetPanelByAttempt(int attempt)
    {
        switch (attempt)
        {
            case 0: return panel1;
            case 1: return panel2;
            case 2: return panel3;
            case 3: return panel4;
            case 4: return panel5;
            default: return null;
        }
    }

    // hint
    public void click_hintButton()
    {
        hintPanel.SetActive(true);
        
        // hintPanel 裡面的 panel 裡面的 text
        hintPanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = hintName;
    }

    public void click_closeHintButton()
    {
        hintPanel.SetActive(false);
    }

}
