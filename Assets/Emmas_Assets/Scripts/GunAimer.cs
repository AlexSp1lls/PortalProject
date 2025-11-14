using Unity.VisualScripting;
using UnityEngine;
public class GunAimer : MonoBehaviour
{
    void Update()
    {
        //Get the mouse position in world space
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Calculate the direction  of the mouse - berfs position
        Vector2 direction = mouseWorldPosition - transform.position;

        //Put it in angles
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //Apply it
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    
}
