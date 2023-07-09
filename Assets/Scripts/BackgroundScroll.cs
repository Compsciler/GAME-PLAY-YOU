using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.5f;
    [SerializeField] float startX = 307f;
    [SerializeField] float startY = 270f;
    [SerializeField] float endY = 4096f / 24f;

    RectTransform rectTransform;

    float xScroll;
    float yScroll;

    private void Start()
    {
        xScroll = Mathf.Sin(15f * Mathf.Deg2Rad);
        yScroll = Mathf.Cos(15f * Mathf.Deg2Rad);

        rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (rectTransform.anchoredPosition.y > endY)
        {
            transform.localPosition = new Vector3(transform.localPosition.x - xScroll * scrollSpeed * Time.deltaTime, transform.localPosition.y - yScroll * scrollSpeed * Time.deltaTime, transform.localPosition.z);
        }
        else
        {
            transform.localPosition = new Vector3(startX, startY, transform.localPosition.z);
        }
    }
}
