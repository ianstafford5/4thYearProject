using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int health = 100;
    public GameObject text;

    private void Start()
    {
        
    }

    void Update()
    {
        text.GetComponent<TextMesh>().text = health.ToString("00") + "%";
        
    }

    public void TakeDammage(int TheDammage)
    {
        health -= TheDammage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
