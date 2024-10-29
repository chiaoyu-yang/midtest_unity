using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class profile : MonoBehaviour
{
    public GameObject inputName, inputNumber, hinttext;
    void Start()
    {
        hinttext.SetActive(false);
    }

    public void click_clearName_button()
    {
        inputName.GetComponent<InputField>().text = "";
    }

    public void click_clearNumber_button()
    {
        inputNumber.GetComponent<InputField>().text = "";
    }
}
