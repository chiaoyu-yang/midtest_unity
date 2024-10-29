using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class guessNumber : MonoBehaviour
{
    public InputField inputField;
    public Button inputButton;
    public Button resultButton;
    

    public Text[] resultTexts = new Text[8];

    // 靜態變數保存密碼，只生成一次
    private static int[] password;
    private int attemptCount = 0;
    private bool gameOver = false;

    void Start()
    {
        // 只生成一次密碼
        if (password == null)
        {
            GeneratePassword();
        }

        // 確保只綁定一次點擊事件
        inputButton.onClick.RemoveAllListeners();  
        inputButton.onClick.AddListener(OnGuess);

        resultButton.gameObject.SetActive(false);
    }

    void GeneratePassword()
    {
        password = new int[4];  // 初始化密碼
        System.Random rand = new System.Random();
        int ans;
        bool renew;

        do
        {
            ans = rand.Next(1234, 9877);
            password[0] = ans % 10;
            password[1] = (ans / 10) % 10;
            password[2] = (ans / 100) % 10;
            password[3] = ans / 1000;

            // 檢查數字是否重複
            renew = false;
            for (int i = 0; i < 3; i++)
                for (int j = i + 1; j < 4; j++)
                    if (password[i] == password[j])
                        renew = true;

        } while (renew);

        Debug.Log($"Generated Password: {password[3]}{password[2]}{password[1]}{password[0]}");
    }

    public void OnGuess()
    {
        Debug.Log($"OnGuess called - attemptCount: {attemptCount}");
        
        if (gameOver || attemptCount >= 8) return;

        string input = inputField.text;

        inputButton.interactable = false;  // 禁用按鈕以避免重複觸發

        if (input.Length != 4 || !int.TryParse(input, out int guessNumber))
        {
            inputField.text = "Input again";
            inputButton.interactable = true;  // 若輸入錯誤，重新啟用按鈕
            return;
        }

        int[] guess = new int[4]
        {
            guessNumber % 10,
            (guessNumber / 10) % 10,
            (guessNumber / 100) % 10,
            guessNumber / 1000
        };

        for (int i = 0; i < 3; i++)
            for (int j = i + 1; j < 4; j++)
                if (guess[i] == guess[j])
                {
                    inputField.text = "Input again";
                    inputButton.interactable = true;  // 若輸入有誤，重新啟用按鈕
                    return;
                }

        int A = 0, B = 0;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (password[i] == guess[j])
                {
                    if (i == j) A++;
                    else B++;
                }
            }
        }

        if (attemptCount < resultTexts.Length)
        {
            Debug.Log($"Updating Text {attemptCount}: {resultTexts[attemptCount].name}");
            resultTexts[attemptCount].text = $"{input} → {A}A{B}B";
        }

        attemptCount++;
        inputButton.interactable = true;  // 成功後重新啟用按鈕

        if (A == 4 || attemptCount >= 8)
        {
            gameOver = true;
            resultButton.gameObject.SetActive(true);
        }
    }

    public void ClearInputField()
    {
        inputField.text = "";
    }
}