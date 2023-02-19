using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public Animator anim_player;

    private Animator anim_enemy;

    // Start is called before the first frame update
    void Start()
    {
        anim_enemy = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            anim_enemy.SetBool("IsHit", false);
            anim_enemy.SetBool("IsDead", true);
        }
    }
}
