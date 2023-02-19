using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage;

    void OnTriggerEnter2D()
    {
        FindObjectOfType<PlayerCombat>().TakeDamage(damage);
    }
}
