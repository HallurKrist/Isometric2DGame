using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private RectTransform healthPanel; 

    [SerializeField] private RectTransform firstHealth; 
    [SerializeField] private RectTransform secondHealth; 
    [SerializeField] private RectTransform thrirdHealth; 
    [SerializeField] private RectTransform fourthHealth;

    private int health = 4;

    private void Start()
    {
        health = 4;
    }

    void Update()
    {
        Vector3 screenposition = Camera.main.WorldToScreenPoint(transform.position);
        healthPanel.transform.position = screenposition;
    }

    public void TakeDamage()
    {
        health -= 1;
        UpdateHealth();
    }

    public void HealDamage()
    {
        health += 1;
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        if (health == 4)
        {
            firstHealth.gameObject.SetActive(true);
            secondHealth.gameObject.SetActive(true);
            thrirdHealth.gameObject.SetActive(true);
            fourthHealth.gameObject.SetActive(true);
        }
        else if (health == 3)
        {
            firstHealth.gameObject.SetActive(true);
            secondHealth.gameObject.SetActive(true);
            thrirdHealth.gameObject.SetActive(true);
            fourthHealth.gameObject.SetActive(false);
        }
        else if (health == 2)
        {
            firstHealth.gameObject.SetActive(true);
            secondHealth.gameObject.SetActive(true);
            thrirdHealth.gameObject.SetActive(false);
            fourthHealth.gameObject.SetActive(false);
        }
        else if (health == 1)
        {
            firstHealth.gameObject.SetActive(true);
            secondHealth.gameObject.SetActive(false);
            thrirdHealth.gameObject.SetActive(false);
            fourthHealth.gameObject.SetActive(false);
        }
        else
        {
            firstHealth.gameObject.SetActive(false);
            secondHealth.gameObject.SetActive(false);
            thrirdHealth.gameObject.SetActive(false);
            fourthHealth.gameObject.SetActive(false);

            //died
        }
    }
}
