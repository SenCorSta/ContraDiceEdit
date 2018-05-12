using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float power;
    public float fly;
    public float maxSpeed;
    public float maxDropcd;
    float hight = 0.36f;
    public bool isJump=false;
    public bool isOnFloor;
    public float life;
    public float HP;
    public float MaxHP = 100;
    //Transform floorCheck;
    Transform body;
    public Transform footOn;

    Rigidbody2D rb2d;
    Animator anm;
	// Use this for initialization
	void Start () {
	    
	}
    void Awake() 
    {
        anm = transform.FindChild("Body").GetComponent<Animator>();
        //floorCheck = transform.Find("floorCheck");
        body = transform.Find("Body");
        rb2d = GetComponent<Rigidbody2D>();
        highttemp = hight;
        HP = MaxHP;
    }
	// Update is called once per frame
	void Update () {
        GameObject.FindGameObjectWithTag("Score").transform.GetComponent<Text>().text = "杀戮数:" + socer;
        GameObject.FindGameObjectWithTag("Life").transform.GetComponent<Text>().text = "剩余生命:" + life;
        if (HP<=0)
        {
            if (life>0)
            {
                HP = MaxHP;
                life--;
                gameObject.transform.position = new Vector2(-3.46f,-3f);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
	}
    float cd = 0;
    float dropcd = 0;
    float firecd = 0;
    float highttemp = 0;
    public float socer = 0;
    void FixedUpdate() 
    {
        if (anm.GetBool("isClimb"))
        {
            hight = hight/2;
        }
        else
        {
            hight = highttemp;
        }

        anm.SetFloat("MoveSpeed", Mathf.Abs(rb2d.velocity.x));
        if (Mathf.Sign(rb2d.transform.localScale.x) + Mathf.Sign(rb2d.velocity.x) == 0f && rb2d.velocity.x != 0)
        {
            Turnaround();
        }

        //isOnFloor = Physics2D.Linecast(body.position, floorCheck.position, 1 << LayerMask.NameToLayer("back"));
       
        Vector2 low_hight = new Vector2(transform.position.x, transform.position.y - hight);
        Vector2 top_hight = new Vector2(transform.position.x, transform.position.y + hight);
        Vector2 front_hight = new Vector2(transform.position.x, transform.position.y + (hight * Mathf.Sin(cd*10)));
        cd+=Time.deltaTime;
        //Debug.Log(Mathf.Sin(cd));
        //镭射检测下方
        RaycastHit2D hit = Physics2D.Raycast(low_hight, -Vector2.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("back"));
        if (hit.collider != null)
        {

            float distance = Mathf.Abs(hit.point.y - transform.position.y);
            if (hit.collider.GetComponent<ColliderCheck>())
            {
                hit.collider.GetComponent<ColliderCheck>().Active = transform.FindChild("Body").GetComponent<Collider2D>();
            }
            //float heightError = floatHeight - distance;
            //float force = liftForce * heightError - rb2D.velocity.y * damping;
            //rb2D.AddForce(Vector3.up * force);
            if (distance <= 0.4 && distance >= 0.2 && (rb2d.velocity.y<=0))
            {
                isOnFloor = true;
            }
            else if (distance >=0.5)
            {
                isOnFloor = false;
            }
            else
            {
                isOnFloor = false;
            }
        }

        //镭射检测上方
        RaycastHit2D hit_up = Physics2D.Raycast(top_hight, Vector2.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("back"));
        if (hit_up.collider != null)
        {

            //float distance = Mathf.Abs(hit_up.point.y - transform.position.y);
            if (hit_up.collider.GetComponent<ColliderCheck>())
            {
                hit_up.collider.GetComponent<ColliderCheck>().Ignore = transform.FindChild("Body").GetComponent<Collider2D>();
            }
        }
        //镭射检测右方
        RaycastHit2D hit_right = Physics2D.Raycast(front_hight, Vector2.right, Mathf.Infinity, 1 << LayerMask.NameToLayer("back"));
        if (hit_right.collider != null)
        {
            //float distance = Mathf.Abs(hit_right.point.y - transform.position.y);
            if (hit_right.collider.GetComponent<ColliderCheck>())
            {
                hit_right.collider.GetComponent<ColliderCheck>().Ignore = transform.FindChild("Body").GetComponent<Collider2D>();
            }
        }
        //镭射检测左方
        RaycastHit2D hit_left = Physics2D.Raycast(front_hight, -Vector2.right, Mathf.Infinity, 1 << LayerMask.NameToLayer("back"));
        if (hit_left.collider != null)
        {
            //float distance = Mathf.Abs(hit_left.point.y - transform.position.y);
            if (hit_left.collider.GetComponent<ColliderCheck>())
            {
                hit_left.collider.GetComponent<ColliderCheck>().Ignore = transform.FindChild("Body").GetComponent<Collider2D>();
            }
        }
        Debug.DrawLine(low_hight, hit.point, Color.green);
        Debug.DrawLine(top_hight, hit_up.point, Color.yellow);
        Debug.DrawLine(front_hight, hit_right.point, Color.red);
        Debug.DrawLine(front_hight, hit_left.point, Color.red);

        if (isOnFloor && isJump)//跳跃状态 在地上 切换跳跃动画
        {
            anm.SetTrigger("Landing");
            isJump = false;

        }

        dropcd += Time.deltaTime;
        if (dropcd > maxDropcd)
        {
            body.gameObject.layer = 9;
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            rb2d.AddForce(Vector2.right * -power);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            rb2d.AddForce(Vector2.right * power);
        }
        if (!isJump && Input.GetAxis("Jump") >0 && Input.GetAxis("Jump") < 0.5 && isOnFloor)//跳
        {
            if (Input.GetAxis("Vertical") < 0)//下跳
            {
                //body.gameObject.layer = 11;

                //anm.SetTrigger("Drop");
                //dropcd = 0;
                //isJump = true;
            }
            else
            {
                rb2d.AddForce(Vector2.up * fly);
                anm.SetTrigger("Jump");
                isJump = true;
            }

        }
        if (Input.GetAxis("Vertical") < 0)//爬
        {
            anm.SetBool("isClimb",true);
            if (Input.GetAxis("Jump") > 0 && Input.GetAxis("Jump") < 0.08)//下跳
            {
                body.gameObject.layer = 11;

                anm.SetTrigger("Drop");
                dropcd = 0;
                isJump = true;
            }
        }
        else
        {
            anm.SetBool("isClimb", false);
        }

        if (Input.GetAxis("Fire") > 0)//开枪
        {
            if (Input.GetAxis("Fire") > 0 && Input.GetAxis("Fire") < 0.5)
            {
                anm.SetTrigger("Fire");
                firecd = 0;
            }

            if (firecd>=0.4)
            {
                anm.SetTrigger("Fire");
                
                firecd = 0;
            }
        }
        else
        {
            anm.SetTrigger("StopFire");
        }
        firecd += Time.deltaTime;

       
        if (Input.GetAxis("Vertical") > 0)//看上
        {
            anm.SetBool("isUp", true);
        }
        else
        {
            anm.SetBool("isUp", false);
        }
        if (Mathf.Abs(rb2d.velocity.x)>maxSpeed)
        {
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
        }

    }



    void OnCollisionEnter2D(Collision2D coll)
    {
        //if (body.position.y < coll.transform.position.y)
        //{
        //    body.gameObject.layer = 11;
        //}
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        //transform.gameObject.layer = 9;
    }

    void Turnaround()
    {
        rb2d.transform.localScale = new Vector2(rb2d.transform.localScale.x * -1, rb2d.transform.localScale.y);
    }
}
