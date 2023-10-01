using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLose : MonoBehaviour
{
    [SerializeField]private float thingsToSteal;
    public float thingsStolen;

    public bool hasCollectedAll = false;

    // Start is called before the first frame update
    void Start()
    {
        thingsStolen = 0f;

        GameObject[] interactableObjects = GameObject.FindGameObjectsWithTag("Interactable");
        thingsToSteal = interactableObjects.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(thingsStolen == thingsToSteal)
        {
            hasCollectedAll = true;
            thingsStolen = 0f;
        }
    }

    public void AddStolen()
    {
        thingsStolen ++;
    }
}
