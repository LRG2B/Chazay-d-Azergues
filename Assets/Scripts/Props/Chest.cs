using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Animator anim_chest;
    public GameObject potion;

    bool AlreadyOpened;

    private void Start()
    {
        potion.GetComponent<Collider2D>().enabled = false;
    }

    void OnTriggerEnter2D()
    {
        if (AlreadyOpened == false)
            anim_chest.SetBool("IsOpen", true);
    }

    void OnTriggerExit2D()
    {
        anim_chest.SetBool("IsOpen", false);
    }

    void MoveObject() { 
        if (AlreadyOpened == false)
        {
            potion.transform.localPosition = new Vector3(potion.transform.localPosition.x + 0.9f, potion.transform.localPosition.y, potion.transform.localPosition.z);
            AlreadyOpened = true;
            potion.GetComponent<Collider2D>().enabled = true;
        }
    }

    public void HideObject()
    {
        potion.transform.localPosition = new Vector3(potion.transform.localPosition.x-1, potion.transform.localPosition.y, potion.transform.localPosition.z);
    }
}
