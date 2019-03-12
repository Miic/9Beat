using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetect : MonoBehaviour {

    public KeyCode buttonToHit;
    public SongManager sm;
    public GameObject approach;
    public Sprite spriteA;
    public Sprite spriteB;
    public Color col;

	// Use this for initialization
	void Start () {
        sm = FindObjectOfType<SongManager>();
        col = gameObject.GetComponent<SpriteRenderer>().color;

		if (gameObject.tag == "1Button")
        {
            buttonToHit = KeyCode.Keypad1;
        }
        else if (gameObject.tag == "2Button")
        {
            buttonToHit = KeyCode.Keypad2;
        }
        else if (gameObject.tag == "3Button")
        {
            buttonToHit = KeyCode.Keypad3;
        }
        else if (gameObject.tag == "4Button")
        {
            buttonToHit = KeyCode.Keypad4;
        }
        else if (gameObject.tag == "5Button")
        {
            buttonToHit = KeyCode.Keypad5;
        }
        else if (gameObject.tag == "6Button")
        {
            buttonToHit = KeyCode.Keypad6;
        }
        else if (gameObject.tag == "7Button")
        {
            buttonToHit = KeyCode.Keypad7;
        }
        else if (gameObject.tag == "8Button")
        {
            buttonToHit = KeyCode.Keypad8;
        }
        else if (gameObject.tag == "9Button")
        {
            buttonToHit = KeyCode.Keypad9;
        }
        else
        {
            Debug.Log("No button assigned");
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(buttonToHit))
        {
            //Debug.Log(buttonToHit + "pressed at " + Time.time);
            gameObject.transform.localScale = new Vector3(.95f, .95f, .95f);
            sm.buttonPressed(buttonToHit, Time.time, this);
            
        }

        if (Input.GetKeyUp(buttonToHit))
        {
            gameObject.transform.localScale = new Vector3(1f,1f,1f);
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteB;
            gameObject.GetComponent<SpriteRenderer>().color = col;
        }

    }

    public KeyCode getButton()
    {
        return buttonToHit;
    }

    public void invokeApproach(float f)
    {
        
        Invoke("playApproach", f);
    }

    public void playApproach()
    {
        //Debug.Log("setsetststsetset");
        Instantiate(approach, transform.position, Quaternion.identity);
    }
    
}
