using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamMaterialDisplay : MonoBehaviour
{
    WebCamTexture webcam;
    public WebCamTexture Webcam => webcam;
    Renderer rend;

    void Awake()
    {
        if (webcam == null)
        {
            webcam = new WebCamTexture();
        }

        rend = GetComponent<Renderer>();
        rend.material.mainTexture = webcam;

        if (!webcam.isPlaying)
        {
            webcam.Play();
        }
    }
}
