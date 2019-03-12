using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Approach : MonoBehaviour {

    private float createTime;
    private bool isGrowing = true;

	// Use this for initialization
	void Start () {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1); 
        createTime = Time.time;
        Invoke("setGrowingFalse", 1.0f);
        Invoke("des", 1.6f);
	}
	
	// Update is called once per frame
	void Update () {
        if (isGrowing)
        {
            
            gameObject.transform.localScale = new Vector3(1 * (Time.time - createTime), 1 * (Time.time - createTime), 1 * (Time.time - createTime));
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1 * (Time.time - createTime));
        }
        else if (!isGrowing)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1 - 2 * (Time.time - createTime));
        }
        
	}

    public void setGrowingFalse()
    {
        isGrowing = false;
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        createTime = Time.time;
    }


    public void des()
    {
        //Destroy(gameObject);
    }
}
