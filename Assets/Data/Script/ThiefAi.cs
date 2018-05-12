using UnityEngine;
using System.Collections;

public class ThiefAi : MonoBehaviour {
    public float maxHP;
    float HP;
    public float power;
    public float maxSpeed;
    Animator anm;
    Rigidbody2D rb2d;
    public float atk=9999;
	// Use this for initialization
	void Start () {
        HP = maxHP;

        anm = transform.FindChild("Body").GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    Vector2 front;
    void FixedUpdate() 
    {
        anm.SetFloat("MoveSpeed", Mathf.Abs(rb2d.velocity.x));
        if (Mathf.Sign(rb2d.transform.localScale.x) + Mathf.Sign(rb2d.velocity.x) == 0f && rb2d.velocity.x != 0)
        {
            Turnaround();
        }

        //寻找玩家
        if (GameObject.FindGameObjectsWithTag("Player").Length>0)
        {
            
            float playerPosition = GameObject.FindGameObjectsWithTag("Player")[0].transform.position.x;
            if (transform.position.x - playerPosition > 0.5)
            {
                front = rb2d.velocity;
                rb2d.AddForce(Vector2.right * -power);
            }
            else if (transform.position.x - playerPosition < -0.5)
            {
                front = rb2d.velocity;
                rb2d.AddForce(Vector2.right * power);
            }
            else
            {
                rb2d.AddForce(front * power);
            }

            if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            {
                rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
            }
        }
        
    }

    void Turnaround()
    {
        rb2d.transform.localScale = new Vector2(rb2d.transform.localScale.x * -1, rb2d.transform.localScale.y);
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("碰到玩家");
        if (coll.gameObject.tag == "Player")
        {
            //Destroy(GameObject.FindGameObjectsWithTag("Player")[0]);
            //GameObject.FindGameObjectsWithTag("Player")[0].SetActive(false);
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>().HP -= atk;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag=="PlayerBullet")
        {
            HP -= coll.attachedRigidbody.GetComponent<Bullet>().damage;
            if (HP<=0)
            {
                Destroy(gameObject);
                GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>().socer++;
                if (GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>().socer % 10 == 0)
                {
                    GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>().life++;
                }
            }

            Debug.Log("碰到子弹");
            
            transform.position = new Vector2(transform.position.x + 0.2f*Mathf.Sign(coll.attachedRigidbody.velocity.x), transform.position.y);
        }
    }
}
