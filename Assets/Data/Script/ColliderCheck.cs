using UnityEngine;
using System.Collections;

public class ColliderCheck : MonoBehaviour
{
    public Collider2D Ignore;
    public Collider2D Active;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void FixedUpdate() 
    {
        Collider2D cc = transform.GetComponent<Collider2D>();
        if (Ignore)
        {
            Physics2D.IgnoreCollision(cc, Ignore, true);
            Ignore = null;
        }
        if (Active)
        {
            Physics2D.IgnoreCollision(cc, Active, false);
            Active = null;
        }
    } 
    
    void OnTriggerExit2D(Collider2D collision) 
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision) 
    {
        //Transform player = collision.transform.root;
        //Collider2D cc = transform.GetComponent<Collider2D>();
        //if (player.GetComponent<Player>().isOnFloor)
        //{
        //    cc.isTrigger = false;
        //}
        
    }
    void OnTriggerStay2D(Collider2D collision) 
    {
    }
    void OnCollisionExit2D(Collision2D coll) 
    {
        //Collider2D cc = transform.GetComponent<Collider2D>();
        //cc.isTrigger = true;
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        //Collider2D cc = transform.GetComponent<Collider2D>();
        ////Transform player = coll.transform.root;
        ////if (Active&& coll.collider.name != Active.name)
        ////{
        ////    Physics2D.IgnoreCollision(cc, coll.collider, true);
        ////}
        //Ignore = coll.collider;
        //Physics2D.IgnoreCollision(cc, Ignore, true);
    }
}
