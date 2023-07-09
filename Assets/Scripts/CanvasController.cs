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
    }
}
