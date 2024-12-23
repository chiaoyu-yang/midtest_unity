using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 

public class SendToGoogle : MonoBehaviour
{
    private string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfhSs9a-aHuLUGHMDT9q9fbbbvrMlFDIYT2u0iSshNh_iIhXg/formResponse";
    public IEnumerator Post(string username, string level, string step, string time)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.155245204", username);
        form.AddField("entry.179439861", level);
        form.AddField("entry.1044593132", step);
        form.AddField("entry.112895372", time);

        var www = UnityWebRequest.Post(BASE_URL, form);
        yield return www.SendWebRequest();
    }
}
