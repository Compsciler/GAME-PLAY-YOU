using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonShaderChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Material newMaterial;
    private Material originalMaterial;
    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        originalMaterial = buttonImage.material;
    }

    public void ResetMatieral()
    {
        buttonImage.material = originalMaterial;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.material = newMaterial;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.material = originalMaterial;
    }

    public void SetActiveAfterDelay(GameObject go)
    {
        StartCoroutine(SetActiveAfterDelayCoroutine(go, 1.5f));
    }

    public IEnumerator SetActiveAfterDelayCoroutine(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        go.SetActive(true);
    }
}
