using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;
    
    public List<Item> itemList;

    public PlayerData(CharacterController player, Inventory inventory)
    {
        position = new float[3];

        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        itemList = inventory.itemList;
    }
}
