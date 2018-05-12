using UnityEngine;
using System.Collections;

public class BodyAnim : MonoBehaviour {

    public Transform bullet;
    public float speed;
    float front;
	// Use this for initialization
	void Start () {
	 
	}
	
	// Update is called once per frame
	void Update () {
        front = transform.parent.localScale.x;
	}

    public void Shoot() 
    {
        Transform tempBullet = Instantiate<Transform>(bullet);
        tempBullet.position = new Vector2(transform.position.x + (0.4f * front), transform.position.y + 0.1f);
        tempBullet.GetComponent<Bullet>().direction = new Vector2(front, 0);
    }

    public void ShootClimb()
    {
        Transform tempBullet = Instantiate<Transform>(bullet);
        tempBullet.position = new Vector2(transform.position.x + (0.6f * front), transform.position.y - 0.05f);
        tempBullet.GetComponent<Bullet>().direction = new Vector2(front, 0);
    }

    public void ShootUp()
    {
        Transform tempBullet = Instantiate<Transform>(bullet);
        tempBullet.position = new Vector2(transform.position.x + (0.1f * front), transform.position.y + 0.7f);
        tempBullet.GetComponent<Bullet>().direction = new Vector2(0, 1);
    }

    public void ShootUpMove()
    {
        Transform tempBullet = Instantiate<Transform>(bullet);
        tempBullet.position = new Vector2(transform.position.x + (0.35f * front), transform.position.y + 0.45f);
        tempBullet.GetComponent<Bullet>().direction = new Vector2(front, 1.3f);
    }

    public void ShootDownMove()
    {
        Transform tempBullet = Instantiate<Transform>(bullet);
        tempBullet.position = new Vector2(transform.position.x + (0.35f * front), transform.position.y - 0.15f);
        tempBullet.GetComponent<Bullet>().direction = new Vector2(front, -0.8f);
    }

    public void ShootJump()
    {
        if (Input.GetKey(KeyCode.J))
        {
            Vector2 jumpcheck = new Vector2();
            if (Input.GetKey(KeyCode.S))
            {
                jumpcheck.x = 0;
                jumpcheck.y = -1;
                if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))
                {
                    jumpcheck.x = front;
                    jumpcheck.y = -0.8f;
                }
            }
            else if (Input.GetKey(KeyCode.W))
            {
                jumpcheck.x = 0;
                jumpcheck.y = 1;
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    jumpcheck.x = front;
                    jumpcheck.y = 1.2f;
                }
            }
            else
            {
                jumpcheck.x = front;
                jumpcheck.y = 0; 
            }

            Transform tempBullet = Instantiate<Transform>(bullet);
            tempBullet.position = new Vector2(transform.position.x, transform.position.y);
            tempBullet.GetComponent<Bullet>().direction = jumpcheck;
        }
    }
}
