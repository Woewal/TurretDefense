using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class EnergyBar : MonoBehaviour
{
    public Transform parent;
    [SerializeField] Vector3 offset;

    RectTransform rectTransform;

    [SerializeField] Image energyImage;
    
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetHealth(float newValue)
    {
        energyImage.fillAmount = newValue;
    }
}
