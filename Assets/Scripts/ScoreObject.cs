using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObject : MonoBehaviour {

    public string name;
    public int score;

	// Use this for initialization
	void Start () {
		
	}

    public static ScoreObject CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<ScoreObject>(jsonString);
    }

    public void setScore(int s)
    {
        score = s;
    }

    public int getScore(int s)
    {
        return score;
    }
}
