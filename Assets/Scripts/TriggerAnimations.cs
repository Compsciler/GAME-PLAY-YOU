using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimations : MonoBehaviour
{
    [SerializeField] List<GameObject> objectsToTrigger;
    [SerializeField] string triggerString;

    List<Animator> animators = new List<Animator>();

    void Start()
    {
        foreach (GameObject obj in objectsToTrigger)
        {
            animators.Add(obj.GetComponent<Animator>());
        }
    }

    public void TriggerAllAnimations()
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger(triggerString);
        }
    }
}
