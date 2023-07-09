using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] AnimationCurve progressCurve;

    [SerializeField] float duration = 5f;
    [SerializeField] float multiplier = 100f;

    [SerializeField] float progressNum = 0f;
    public float ProgresNum => progressNum;

    bool hasPregameStarted = false;
    [SerializeField] TriggerAnimations triggerAnimations;

    public IEnumerator LoadingBarCoroutine()
    {
        float time = 0f;

        while (time < duration)
        {
            progressNum = progressCurve.Evaluate(time / duration) * multiplier;
            if (time > duration * 0.88f && !hasPregameStarted)
            {
                triggerAnimations.TriggerAllAnimations();
                hasPregameStarted = true;
            }
            time += Time.deltaTime;
            yield return null;
        }
    }
}
