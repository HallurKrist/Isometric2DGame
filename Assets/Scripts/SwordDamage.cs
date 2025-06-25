using UnityEngine;
using UnityEngine.U2D;

public class SwordDamage : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<HealthController>().TakeDamage();
        }
    }
}