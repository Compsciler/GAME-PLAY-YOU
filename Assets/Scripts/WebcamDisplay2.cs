using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebcamDisplay2 : MonoBehaviour
{
    RawImage image;
    [SerializeField] WebcamMaterialDisplay webcamMaterialDisplay;

    void Awake()
    {
       image = GetComponent<RawImage>(); 
    }

    void Start()
    {
        image.texture = webcamMaterialDisplay.Webcam;
    }
}
