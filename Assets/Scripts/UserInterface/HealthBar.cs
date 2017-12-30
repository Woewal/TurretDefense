using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] Vector3 offset;

    RectTransform rectTransform;

    [SerializeField] Image healthImage;

    [SerializeField] float duration = 2f;

    Coroutine healthBarCoroutine;
    

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Assign()
    {

    }

    private void LateUpdate()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(parent.position + offset);

        rectTransform.position = new Vector3(screenPoint.x, screenPoint.y);
    }

    public void SetHealth(float oldValue, float newValue, float maxValue)
    {
        if (healthBarCoroutine != null)
        {
            StopCoroutine(healthBarCoroutine);
        }

        healthBarCoroutine = StartCoroutine(HealthAnimation(oldValue, newValue, maxValue));
    }

    IEnumerator HealthAnimation(float oldValue, float newValue, float maxValue)
    {
        float currentTime = 0;

        duration = Mathf.Abs(Mathf.Lerp(0, 1, oldValue / maxValue) - Mathf.Lerp(0, 1, newValue / maxValue)) * duration;

        while (currentTime < duration)
        {
            healthImage.fillAmount = Mathf.Lerp(oldValue / maxValue, newValue / maxValue, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}
