using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public bool firstmove = false;
    public GameObject otherPortal;
    public GameObject player;
    public List<GameObject> playerParts;
    public GameObject leftSide;
    public GameObject rightSide;
    public GameObject back;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerParts.Contains(collision.gameObject))
        {
            if (player.GetComponent<PortalThrower>().ableToMove && (firstmove ==true && otherPortal.GetComponent<Portal>().firstmove == true))
            {
                player.GetComponent<PortalThrower>().PlacePlayerCorrect(otherPortal.transform);
                StartCoroutine(player.GetComponent<PortalThrower>().WaitForPortalTravel());
                
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(playerParts.Contains(other.gameObject))
        {
            if (player.GetComponent<PortalThrower>().ableToMove && (firstmove ==true && otherPortal.GetComponent<Portal>().firstmove == true))
            {
                player.transform.position = otherPortal.transform.position;
                StartCoroutine(player.GetComponent<PortalThrower>().WaitForPortalTravel());
                
            }
        }
    }

    public void CheckSides(int side)
    {

        //Make sure its not overlapping on the sides
        if(side == 1)
        {
            //if the leftside is touching a wall - scooch right
            transform.position += transform.right*2;
        }
        else if(side == 2)
        {
            //if the rightside is touching a wall - scooch left
            transform.position += -transform.right*2;
        }
        else
        {
            //backside
        }
    }
}