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

    public void click_back2main()
    {
        SceneManager.LoadScene("Main");
    }
}
