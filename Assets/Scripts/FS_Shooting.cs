using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FS_Shooting : MonoBehaviour
{

    public GameObject bullet_prefab;
    float bulletSpeed = 30f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Camera cam = Camera.main;
            GameObject thebullet = Instantiate(bullet_prefab, cam.transform.position + cam.transform.forward, cam.transform.rotation) as GameObject;
            
            thebullet.GetComponent<Rigidbody>().AddForce(cam.transform.forward * bulletSpeed, ForceMode.Impulse);
        }

        
    }
}
