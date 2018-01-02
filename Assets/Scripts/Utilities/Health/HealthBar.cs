using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Transform parent;
    [SerializeField] Vector3 offset;

    RectTransform rectTransform;

    [SerializeField] Image healthImage;

    [SerializeField] float transitionDuration = 2f;

    Coroutine healthBarCoroutine;
    

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Assign()
    {

    }

    public void SetHealth(float newValue)
    {
        if (healthBarCoroutine != null)
        {
            StopCoroutine(healthBarCoroutine);
        }

        healthBarCoroutine = StartCoroutine(HealthAnimation(newValue));
    }

    IEnumerator HealthAnimation(float newValue)
    {
        float oldValue = healthImage.fillAmount;

        float currentTime = 0;

        float duration = Mathf.Abs(oldValue - newValue) * transitionDuration;
        while (currentTime < duration)
        {
            healthImage.fillAmount = Mathf.Lerp(oldValue, newValue, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}
