using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSystem : MonoBehaviour {

    public Camera camera;
    public float range;
    public LayerMask myLayerMask;
    public int damage = 50;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

       if (Input.GetButtonDown("Fire1"))
        {

            Attack();
        }
        //	if (TheMace.animation.isPlaying == false)
        //	{
        //		TheMace.animation.CrossFade("Idle");
        //	}
        //	
        //	if (Input.GetKey (KeyCode.LeftShift))
        //	{
        //		TheMace.animation.CrossFade("Sprint");
        //	}
        //	
        //	if (Input.GetKeyUp(KeyCode.LeftShift))
        //	{
        //		TheMace.animation.CrossFade("Idle");
        //	}
    }

    void Attack()
    {
        //Attack function
        RaycastHit hit;
        if(Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, range, myLayerMask))
        {
            EnemyHealth enemy = hit.transform.gameObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDammage(damage);
            }
            Debug.Log(hit.transform.name);
        }
        
    }
}
