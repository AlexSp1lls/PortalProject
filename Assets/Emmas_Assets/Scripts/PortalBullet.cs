using UnityEngine;
using UnityEngine.Tilemaps;

public class PortalBullet : MonoBehaviour
{
    public bool GO = false;
    public float lifetime = 2;
    public bool hasCollided = false;
    public Transform shootRotation;
    public PortalThrower pt;
    public GameObject portal;
    public Tilemap tilemap;
    void Awake()
    {
        Destroy(gameObject, lifetime);
    }
    void Update()
    {
        if(GO == true)
        {
            if (hasCollided == false && pt != null && shootRotation != null)
            {
                transform.Translate(shootRotation.right * 5 * Time.fixedDeltaTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            }
        }
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(GO == true)
        {
            if (collision.gameObject.CompareTag("ground") && hasCollided == false)
            {
                hasCollided = true;
                ContactPoint2D contact = collision.GetContact(0);
                Vector2 contactPoint = contact.point;


                pt.PlacePortal(transform, portal, contactPoint);
                Destroy(gameObject, 2);
                GetComponent<PolygonCollider2D>().enabled = false;
            }
        }
    }
}
