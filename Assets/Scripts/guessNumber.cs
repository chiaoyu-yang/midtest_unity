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

    private int[] password = new int[4];
    private int attemptCount = 0;
    private bool gameOver = false;

    void Start()
    {
        GeneratePassword();
        resultButton.gameObject.SetActive(false);
    }

    void GeneratePassword()
    {
        System.Random rand = new System.Random();
        bool renew;

        do
        {
            int ans = rand.Next(0123, 9877);
            password[0] = ans % 10;
            password[1] = (ans / 10) % 10;
            password[2] = (ans / 100) % 10;
            password[3] = ans / 1000;

            // 檢查數字是否重複
            renew = false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = i + 1; j < 4; j++)
                {
                    if (password[i] == password[j])
                    {
                        renew = true;
                    }
                }
            }

        } while (renew);
    }

    public void OnGuess()
    {
        if (gameOver || attemptCount >= 8) return;

        string input = inputField.text;


        if (input.Length != 4 || !int.TryParse(input, out int guessNumber))
        {
            inputField.text = "Input again";
            return;
        }

        int[] guess = new int[4]
        {
            guessNumber % 10,
            (guessNumber / 10) % 10,
            (guessNumber / 100) % 10,
            guessNumber / 1000
        };

        // 檢查四位數是否不重複
        for (int i = 0; i < 3; i++)
        {
            for (int j = i + 1; j < 4; j++)
            {
                if (guess[i] == guess[j])
                {
                    inputField.text = "Input again";
                    return;
                }
            }
        }

        int A = 0, B = 0;

        for (int i = 0; i < 4; i++)
        {
            if (password[i] == guess[i])
            {
                A++;
            }
            else if (System.Array.Exists(password, element => element == guess[i]))
            {
                B++;
            }
        }

        if (attemptCount < resultTexts.Length)
        {
            resultTexts[attemptCount].text = $"{input} → {A}A{B}B";
        }

        attemptCount++;

        if (A == 4 || attemptCount >= 8)
        {
            gameOver = true;
            resultButton.gameObject.SetActive(true);
            global.guessnumber = attemptCount.ToString();
        }
    }

    public void ClearInputField()
    {
        inputField.text = "";
    }
}