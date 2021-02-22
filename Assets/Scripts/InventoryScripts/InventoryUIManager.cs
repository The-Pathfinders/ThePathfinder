using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject inventory;
    private bool isInventoryOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isInventoryOpen)
            {
                Resume();
            }
            else
            {
                Open();
            }
        }
    }

    public void Resume()
    {
        inventory.SetActive(false);

        Time.timeScale = 1f;

        isInventoryOpen = false;
    }

    void Open()
    {
        inventory.SetActive(true);

        Time.timeScale = 0f;

        isInventoryOpen = true;
    }
}
