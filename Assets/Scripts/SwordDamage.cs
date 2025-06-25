using UnityEngine;
using UnityEngine.U2D;

public class SwordDamage : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<HealthController>().TakeDamage();
        }
    }
}