using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactable : MonoBehaviour
{

    [SerializeField]
    private Text interactableText;

    private bool interactionAllowed;

    private void Start()
    {
        interactableText.gameObject.SetActive(false);
    }

    
    private void Update()
    {
       if(interactionAllowed && Input.GetKeyDown(KeyCode.E))
       {
            Interaction();
       }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("TestPlayer"))
        {
            interactableText.gameObject.SetActive(true);

            interactionAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("TestPlayer"))
        {
            interactableText.gameObject.SetActive(false);

            interactionAllowed = false;
        }
    }

    private void Interaction()
    {
        Destroy(gameObject);
    }

}
