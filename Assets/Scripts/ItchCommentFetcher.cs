using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using HtmlAgilityPack;

public class ItchCommentFetcher : MonoBehaviour
{
    string url = "https://rogerw.itch.io/webcam-test";

    string html;

    public event Action<Comment> OnNewComment;

    IEnumerator Start()
    {
        yield return StartCoroutine(GetHtml());
        Comment mostRecentComment = GetMostRecentTopLevelCommentDiv();
        OnNewComment?.Invoke(mostRecentComment);
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

    private Comment GetMostRecentTopLevelCommentDiv()
    {
        HtmlDocument htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);

        HtmlNodeCollection commentNodes = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'community_post')]");
        HtmlNode mostRecentCommentNode = commentNodes.First();

        HtmlNode authorNode = mostRecentCommentNode.SelectSingleNode(".//span[@class='post_author']/a");
        HtmlNode contentNode = mostRecentCommentNode.SelectSingleNode(".//div[contains(@class, 'post_body')]/p");

        Comment mostRecentComment;
        mostRecentComment.author = authorNode?.InnerText;
        mostRecentComment.content = contentNode?.InnerText;

        return mostRecentComment;
    }

    public struct Comment
    {
        public string author;
        public string content;
    }
}
