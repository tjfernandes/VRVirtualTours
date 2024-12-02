using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverManager : MonoBehaviour
{
    private Vector3 originalScale;
    public float scaleFactor = 1.1f; // Adjust this value to control the scale increase
    public float transitionDuration = 0.2f; // Duration of the scaling effect

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(BaseEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleTo(originalScale * scaleFactor));
    }

    public void OnPointerExit(BaseEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleTo(originalScale));
    }

    private IEnumerator ScaleTo(Vector3 targetScale)
    {
        Vector3 startScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / transitionDuration);
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
