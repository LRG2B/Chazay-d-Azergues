using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWeapon : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D()
    {
        animator.SetBool("HaveSword", true);
    }

    void OnTriggerExit2D()
    {
        animator.SetBool("HaveSword", true);
    }
}
