using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour
{
    public GameObject inputName;

    public void click_start_button()
    {
        SceneManager.LoadScene("Info");
    }

    public void click_help_button()
    {
        SceneManager.LoadScene("Help-1");
    }

    public void click_help_2_button()
    {
        SceneManager.LoadScene("Help-2");
    }

    public void click_help_3_button()
    {
        SceneManager.LoadScene("Help-3");
    }

    public void click_help_4_button()
    {
        SceneManager.LoadScene("Help-4");
    }

    public void click_help_5_button()
    {
        SceneManager.LoadScene("Help-5");
    }

    public void click_next_button()
    {
        if (inputName == null)
        {
            Debug.LogError("InputName GameObject is not assigned.");
            return;
        }

        InputField inputField = inputName.GetComponent<InputField>();

        if (inputField == null)
        {
            Debug.LogError("No InputField component found on InputName GameObject.");
            return;
        }

        if (inputField.text == "")
        {
            if (inputField.placeholder != null)
            {
                inputField.placeholder.GetComponent<Text>().text = "請輸入姓名";
            }
            else
            {
                Debug.LogError("Placeholder is not assigned in the InputField.");
            }
            return;
        }

        global.username = inputField.text;
        SceneManager.LoadScene("Level");
    }

    public void click_play_again_button()
    {
        SceneManager.LoadScene("Level");
    }

    public void click_easy_button()
    {
        SceneManager.LoadScene("game-easy");
        global.level = "easy";
    }

    public void click_normal_button()
    {
        SceneManager.LoadScene("game-normal");
        global.level = "normal";
    }

    public void click_hard_button()
    {
        SceneManager.LoadScene("game-hard");
        global.level = "hard";
    }

    public void click_back2main()
    {
        SceneManager.LoadScene("Main");
    }
}
