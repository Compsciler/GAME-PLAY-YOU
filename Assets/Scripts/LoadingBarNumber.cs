using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBarNumber : MonoBehaviour
{
    [SerializeField] float num;

    [SerializeField] LoadingBar loadingBar;

    Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (!image.enabled && loadingBar.ProgresNum >= num && num > 0f)
        {
            image.enabled = true;
        }
        else if (image.enabled && loadingBar.ProgresNum < num && num > 0f)
        {
            image.enabled = false;
        }
        else if (!image.enabled && loadingBar.ProgresNum < num && num <= 0f)
        {
            image.enabled = true;
        }
    }
}
