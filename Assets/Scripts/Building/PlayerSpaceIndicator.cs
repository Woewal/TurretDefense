using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpaceIndicator : MonoBehaviour
{
    private List<GameObject> Collissions;
    private SpriteRenderer Sprite;
    public bool CanPlace;

    private void Start()
    {
        Collissions = new List<GameObject>();
        Sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Collissions = new List<GameObject>();
        Sprite = GetComponent<SpriteRenderer>();

        if (Collissions.Count == 0)
        {
            CanPlace = true;
            Sprite.color = Color.green;
        }
        else
        {
            CanPlace = false;
            Sprite.color = Color.red;
        }
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (!other.isTrigger)
        {
            CanPlace = false;
            Sprite.color = Color.red;
            Collissions.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger)
        {
            Collissions.Remove(other.gameObject);

            if (Collissions.Count == 0)
            {
                CanPlace = true;
                Sprite.color = Color.green;
            }
        }
    }
}
