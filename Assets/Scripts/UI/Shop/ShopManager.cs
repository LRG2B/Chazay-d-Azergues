using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public Animator animator;

    private float speed;
    private float jump;

    void Start()
    {
        speed = GameObject.Find("Player").GetComponent<PlayerController>().maxSpeed;
        jump = GameObject.Find("Player").GetComponent<PlayerController>().jumpPower;
    }

    void Update()
    {
    }

    public void StartShop()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().maxSpeed = 0f;
        GameObject.Find("Player").GetComponent<PlayerController>().jumpPower = 0f;
        GameObject.Find("Player").GetComponent<Animator>().SetBool("CanMove", false);
        animator.SetBool("IsOpen", true);
    }
}
