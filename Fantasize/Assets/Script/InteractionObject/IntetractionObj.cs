using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntetractionObj : MonoBehaviour
{
    [SerializeField] GameObject interactionImg;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Equals("Aj"))
            interactionImg.SetActive(true);
    }


    private void OnTriggerExit(Collider other)
    {
        interactionImg.SetActive(false);
    }

}
