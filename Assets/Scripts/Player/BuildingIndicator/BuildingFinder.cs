using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingFinder : MonoBehaviour
{

    BuildingIndicator indicator;
    public GameObject lineRendererPrefab;

    [SerializeField] 

    private void Start()
    {
        indicator = GetComponentInParent<BuildingIndicator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Building")
        {
            indicator.lineRenderers.Add(other.gameObject, Instantiate(lineRendererPrefab, indicator.transform).GetComponent<LineRenderer>());
            indicator.UpdateDisplay();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Building")
        {
            Destroy(indicator.lineRenderers[other.gameObject].gameObject);
            indicator.lineRenderers.Remove(other.gameObject);
            indicator.UpdateDisplay();
        }
    }
}
