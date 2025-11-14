using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using System.Collections;
public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
public class PortalThrower : MonoBehaviour
{
    public bool ableToMove = true;
    
    private Direction portalDirection;
    public GameObject bullet;
    public Transform shootPoint;
    public Transform shootRotation;
    public GameObject portal1;
    public GameObject portal2;
    public Tilemap targetTilemap;
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        ThrowBullet(portal1);
    }
    public void OnAttack2(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        ThrowBullet(portal2);
    }
    void ThrowBullet(GameObject portal)
    {
        GameObject g = Instantiate(bullet, shootPoint.position, Quaternion.identity);
        g.GetComponent<PortalBullet>().shootRotation = shootRotation;
        g.GetComponent<PortalBullet>().pt = this;
        g.GetComponent<PortalBullet>().portal = portal;
        g.GetComponent<PortalBullet>().tilemap = targetTilemap;
        g.GetComponent<PortalBullet>().GO = true;
    }
    public void PlacePortal(Transform pos, GameObject port, Vector2 contact)
    {
        //---Get the direction the portal should be in
        float zRotation = shootRotation.transform.rotation.eulerAngles.z;
        
        //Normalize to -180 to 180 range
        if (zRotation > 180f)
        {
            zRotation -= 360f;
        }
        if (zRotation >= -40f && zRotation <= 45f)
        {
            portalDirection = Direction.Right;
        }
        if (zRotation >= 46f && zRotation <= 135f)
        {
            portalDirection = Direction.Up;
        }
        if (zRotation >= 136f || zRotation <= -136f)
        {
            portalDirection = Direction.Left;
        }
        if (zRotation >= -137f && zRotation < -40f)
        {
            portalDirection = Direction.Down;
        }
        
        //Set the rotation
        Vector3 spawnPos = new Vector3(contact.x, contact.y, 0f);
        switch (portalDirection)
        {
            case Direction.Up:
                spawnPos = new Vector3(spawnPos.x, spawnPos.y - .3f, spawnPos.z);//for ceiling portals
                port.transform.rotation = Quaternion.Euler(port.transform.rotation.eulerAngles.x, port.transform.rotation.eulerAngles.y, 180);
                break;
            case Direction.Down:
                spawnPos = new Vector3(spawnPos.x, spawnPos.y + .3f, spawnPos.z);//for floor portals
                port.transform.rotation = Quaternion.Euler(port.transform.rotation.eulerAngles.x, port.transform.rotation.eulerAngles.y, 0);
                //no rotation needed
                break;
            case Direction.Left:
                spawnPos = new Vector3(spawnPos.x + .3f, spawnPos.y, spawnPos.z);//for left side portals
                port.transform.rotation = Quaternion.Euler(port.transform.rotation.eulerAngles.x, port.transform.rotation.eulerAngles.y, 270);
                break;
            case Direction.Right:
                spawnPos = new Vector3(spawnPos.x - .3f, spawnPos.y, spawnPos.z);//for right side portals
                port.transform.rotation = Quaternion.Euler(port.transform.rotation.eulerAngles.x, port.transform.rotation.eulerAngles.y, 90);
                break;
            default:
                Debug.LogError("Uhm. So. No? Error in which direction the portal is supposed to be...");
                break;
        }
        
        
        port.transform.position = spawnPos;
    }

    public int PlacePlayerCorrect(Transform portalTransform)
    {
        transform.position = portalTransform.position;
        
        if (portalTransform.eulerAngles.z == 0)
        {
            transform.position = portalTransform.position + (Vector3.up*2);
            return 0;
        }
        if (portalTransform.eulerAngles.z == 90)
        {
            transform.position = portalTransform.position + Vector3.left+Vector3.up;
            return 90;
        }
        if (portalTransform.eulerAngles.z == 180)
        {
            transform.position = portalTransform.position + Vector3.down;
            return 180;
        }
        if (portalTransform.eulerAngles.z == -90)
        {
            transform.position = portalTransform.position + Vector3.right+Vector3.up;
            return -90;
        }
        else
        {
            return -1;
        }
        
    }

    //timer for moving portals
    public IEnumerator WaitForPortalTravel()
    {
        ableToMove = false;
        yield return new WaitForSeconds(.3f);
        ableToMove = true;
    }

}
