using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DoorOpen : MonoBehaviour
{
    public float theDistance;
    public GameObject actionDisplay;
    public GameObject actionText;
    public GameObject theDoor;
    public AudioSource creekSound;
    public Animator doorAnim;


    private void Start()
    {
        doorAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        theDistance = PlayerCasting.distanceFromTarget;

    }
void OnMouseOver()
    {
        if (theDistance < 3)
        {
            actionDisplay.SetActive(true);
            actionText.SetActive(true);
        }
        if (Input.GetButtonDown("Action"))
        {
            if (theDistance <= 3)
            {
                this.GetComponent<BoxCollider>().enabled = false;
                actionDisplay.SetActive(false);
                actionText.SetActive(false);
                doorAnim.SetBool("open", true);
                creekSound.Play();
            }
        }
    }

    private void OnMouseExit()
    {
        actionDisplay.SetActive(false);
        actionText.SetActive(false);
    }
}
