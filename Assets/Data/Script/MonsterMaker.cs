using UnityEngine;
using System.Collections;

public class MonsterMaker : MonoBehaviour {
    public Transform monster;
    public float cd;
    Random ran = new Random();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    float time;
	void Update () {
        time += Time.deltaTime;
        if (time > cd)
        {
            if (GameObject.FindGameObjectsWithTag("Player").Length>0)
            {
                Vector2 playerloc = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
                Instantiate<Transform>(monster);
                if (Mathf.Sin(Time.time) > 0)
                {
                    monster.position = new Vector2(Random.Range(playerloc.x + 5f, playerloc.x + 7f), Random.Range(playerloc.y - 3f, playerloc.y + 3f));
                }
                else
                {
                    monster.position = new Vector2(Random.Range(playerloc.x - 7f, playerloc.x - 5f), Random.Range(playerloc.y - 3f, playerloc.y + 3f));
                }
                time = 0;
            }
        }
	}
}
