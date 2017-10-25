using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_detonator : MonoBehaviour {

    float lifespan = 3;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;

        if (lifespan <= 0)
        {
            Explode();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        string collision = col.gameObject.tag;
        Debug.Log(collision);
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy");
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }

    void Explode()
    {
        Destroy(gameObject);
    }
}
