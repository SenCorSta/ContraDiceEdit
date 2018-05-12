using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float damage;
    public Vector2 direction;
    public float power;
    public float speed;
    public float maxtime;
    Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
        
        Destroy(gameObject, maxtime);
        rb2d = transform.GetComponent<Rigidbody2D>();
        rb2d.AddForce(direction * power);
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void FixedUpdate() 
    {
        if (Mathf.Abs(rb2d.velocity.x) > speed)
        {
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * speed, rb2d.velocity.y);
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.parent.tag=="Monster")
        {
            Destroy(gameObject);
        }
    }


}
