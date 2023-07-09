using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    RectTransform rect;
    [SerializeField] private float speed = 10f;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate rect about z
        rect.Rotate(0, 0, Time.deltaTime * speed);
    }
}
