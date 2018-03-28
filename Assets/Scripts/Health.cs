using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    private int maxHealth = 5;
    private int health = 3;
    private int damage;

    public Texture2D healthFull;
    public Texture2D healthEmpty;

    // Use this for initialization
    void Start ()
    {
       // health = maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Initialize GUI
    private void OnGUI()
    {
        for (int e = 0; e < maxHealth; e++)
        {
            GUI.DrawTexture(new Rect((70 * e) + 5, 10, 60, 50), healthEmpty);
        }
        for (int f = 0; f < health; f++)
        {
            GUI.DrawTexture(new Rect((70 * f) + 5, 10, 60, 50), healthFull);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
