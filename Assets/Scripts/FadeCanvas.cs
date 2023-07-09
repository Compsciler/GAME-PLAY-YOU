using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeCanvas : MonoBehaviour
{
    [SerializeField] Image transitionPanel;
    [SerializeField] float fadeTime;
    [SerializeField] float fadeDelay;

    [SerializeField] AnimationCurve fadeInCurve;
    [SerializeField] AnimationCurve fadeOutCurve;

    [SerializeField] FadeType fadeType;

    [SerializeField] GameObject setInactiveGO;
    [SerializeField] GameObject setActiveGO;

    [SerializeField] TextToSpeech textToSpeech;

    void Awake()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GameStarted:
                StartCoroutine(FadeDelay());
                break;
            default:
                break;
        }
    }

    public void Fade(FadeType fadeType)
    {
        StartCoroutine(FadeCoroutine(fadeType));
    }
    private IEnumerator FadeDelay()
    {
        yield return new WaitForSeconds(fadeDelay);
        Fade(FadeType.FADE_IN);
    }
    private IEnumerator FadeCoroutine(FadeType fadeType)
    {
        AnimationCurve fadeCurve;
        switch (fadeType)
        {
            case FadeType.FADE_IN:
                fadeCurve = fadeInCurve;
                break;
            case FadeType.FADE_OUT:
                fadeCurve = fadeOutCurve;
                break;
            default:
                throw new System.Exception("Invalid fade type");
        }
        
        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            float alpha = fadeCurve.Evaluate(elapsedTime / fadeTime);
            SetTransitionPanelColorAlpha(alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SetTransitionPanelColorAlpha(fadeCurve.Evaluate(1f));
        Debug.Log("here");
        setInactiveGO.GetComponent<Canvas>().enabled = false;
        setActiveGO.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        textToSpeech.PlayAudio();
    }

    private void SetTransitionPanelColorAlpha(float alpha)
    {
        Color transitionPanelColor = transitionPanel.color;
        transitionPanelColor.a = alpha;
        transitionPanel.color = transitionPanelColor;
    }

    public enum FadeType {
        FADE_IN,
        FADE_OUT
    }
}
