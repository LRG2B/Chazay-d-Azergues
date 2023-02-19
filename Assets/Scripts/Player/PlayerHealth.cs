using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Text healthText;
    public int health;
    public Animator anim_player;
    public Animator anim_goblin;
    public Animator anim_skeleton;
    public Transform goblin;
    public Transform skeleton;

    private Rigidbody2D rgbd;
    private bool LastCheck;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        LastCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distToGoblin = Vector2.Distance(transform.position, goblin.position);
        float distToSkeleton = Vector2.Distance(transform.position, skeleton.position);

        if (LastCheck != anim_goblin.GetBool("IsAttacking"))
        {
            if (distToGoblin < 1.5 && anim_goblin.GetBool("IsAttacking"))
            {
                health -= 10;
                anim_player.SetBool("IsHit", true);
                LastCheck = true;
                Debug.Log("Player Is Attacked");
            }
        }

        if (distToGoblin < 1.5 && anim_goblin.GetBool("IsAttacking") == false)
        {
            anim_player.SetBool("IsHit", false);
            LastCheck = false;
            Debug.Log("Player Is Not Attacked");
        }

        //if (LastCheck != anim_skeleton.GetBool("IsAttacking"))
        //{
        //    if (distToSkeleton < 1.5 && anim_goblin.GetBool("IsAttacking"))
        //    {
        //        health -= 10;
        //        anim_player.SetBool("IsHit", true);
        //        LastCheck = true;
        //    }
        //}

        //if (distToSkeleton < 1.5 && !anim_skeleton.GetBool("IsAttacking"))
        //{
        //    anim_player.SetBool("IsHit", false);
        //    LastCheck = false;
        //}


        //if (health == 0)
        //{
        //    anim_player.SetBool("IsHit", false);
        //    anim_player.SetBool("IsDead", true);
        //}

        healthText.text = health.ToString();
        //Debug.Log("DistToGoblin" + distToGoblin);
    }
}
