using UnityEngine;

public class SideToucher : MonoBehaviour
{
    public int side;//1=left,2=right,3=back
    void OnTriggerStay2D(Collider2D collision)
    {
        if(side == 3)
        {
            if(!collision.gameObject.CompareTag("ground"))
            {
                this.GetComponentInParent<Portal>().CheckSides(side);
            }
        }
        else
        {
            if(!collision.gameObject.CompareTag("ground"))
            {
                this.GetComponentInParent<Portal>().CheckSides(side);
            }
        }
    }
}