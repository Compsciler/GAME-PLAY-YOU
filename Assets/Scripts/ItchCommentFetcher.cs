using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ItchCommentFetcher : MonoBehaviour
{
    string url = "https://jackypark9852.itch.io/olympic-smashdown";
    
    string html;

    void Awake()
    {
        StartCoroutine(GetHtml());
    }

    IEnumerator GetHtml()
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        
        // www.SetRequestHeader("Access-Control-Allow-Credentials", "true");
        // www.SetRequestHeader("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
        // www.SetRequestHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
        // www.SetRequestHeader("Access-Control-Allow-Origin", "*");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            html = www.downloadHandler.text;
            Debug.Log(html);
            // byte[] results = www.downloadHandler.data;  // Binary data
        }
        else
        {
            Debug.LogError(www.error);
        }
    }
}
