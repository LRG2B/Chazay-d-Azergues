using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public int health;

    bool AlreadyUsed;

    void OnTriggerEnter2D()
    {
        if (AlreadyUsed == false)
        {
            FindObjectOfType<PlayerCombat>().GetHealth(health);
            FindObjectOfType<Chest>().HideObject();
            AlreadyUsed = true;
        }
    }
}
