using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] ItchCommentFetcher itchCommentFetcher;

    [SerializeField] TMP_Text commentText;
    [SerializeField] TMP_Text authorText;
    [SerializeField] TMP_Text combinedText;

    void OnEnable()
    {
        itchCommentFetcher.OnNewComment += HandleNewComment;
    }

    void OnDisable()
    {
        itchCommentFetcher.OnNewComment -= HandleNewComment;
    }

    private void HandleNewComment(ItchCommentFetcher.Comment comment)
    {
        commentText.text = comment.content;
        authorText.text = comment.author;

        string truncatedContent = comment.content.Substring(0, Math.Min(64, comment.content.Length));
        if (truncatedContent.Length < comment.content.Length)
        {
            truncatedContent += "...";
        }

        combinedText.text = $"Comment by <color=#00ffff>{comment.author}</color>:\n <i>{truncatedContent}</i>\n\nLeave a comment on the itch.io page to literally decide what Mark should say next!";
    }
}
