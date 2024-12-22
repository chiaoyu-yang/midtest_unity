using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Reslut : MonoBehaviour
{
    public GameObject username, level, step, time;
    void Start()
    {
        if (global.username != null)
        {
            username.GetComponent<Text>().text = global.username;
            level.GetComponent<Text>().text = global.level;
            step.GetComponent<Text>().text = global.step;
            time.GetComponent<Text>().text = global.time;
        }
    }
}
