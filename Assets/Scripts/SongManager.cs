using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using WebSocketSharp;
using System;
using System.Text;
//using TeamDev.Redis;


public class SongManager : MonoBehaviour
{

    public GameObject k1;
    public GameObject k2;
    public GameObject k3;
    public GameObject k4;
    public GameObject k5;
    public GameObject k6;
    public GameObject k7;
    public GameObject k8;
    public GameObject k9;

    public SongCondensed song;
    public Dictionary<KeyCode, int> lastCheck = new Dictionary<KeyCode, int>();
    private int score = 0;
    private float startTime;
    private ScoreObject scoreText;

    private WebSocket ws;

    public AudioSource aud;

    private float lastNoteTime = 0;

    private bool isFinished = false;

    public static byte[] GetBytes(string str)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(str);
        // System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
        Debug.Log("Base64 debug: " + Convert.ToBase64String(bytes));
        return bytes;
    }



    // Use this for initialization
    void Start()
    {
        /*
        using (ws = new WebSocket("wss://54.215.219.53:2222"))
        {

            ws.OnOpen += (sender, e) => ws.Send("Hi, there!");
            ws.OnMessage += (sender, e) => scoreRecieved(e); //  => function to call
            ws.OnError += (sender, e) => Debug.Log(e.Message);

        }
        ws.Connect();
        //ws.Send("{\"intent\":\"queryScores\",\"username\":\"testUser\",\"mapid\":" + 2 + "}");
        ws.Send("Yes");

        GetBytes("Yes");
        */

        
        LoadGameData();
        scoreText = FindObjectOfType<ScoreObject>();

        lastCheck[KeyCode.Keypad1] = 0;
        lastCheck[KeyCode.Keypad2] = 0;
        lastCheck[KeyCode.Keypad3] = 0;
        lastCheck[KeyCode.Keypad4] = 0;
        lastCheck[KeyCode.Keypad5] = 0;
        lastCheck[KeyCode.Keypad6] = 0;
        lastCheck[KeyCode.Keypad7] = 0;
        lastCheck[KeyCode.Keypad8] = 0;
        lastCheck[KeyCode.Keypad9] = 0;
        startSong();
    }


    public void startSong()
    {
        aud.Play();
        aud.Play(44100);

        startTime = Time.time;

        /*
        foreach (float i in song.hitMap[KeyCode.Keypad1])
        {
            Debug.Log(i - 0.5f);
            k1.GetComponent<HitDetect>().invokeApproach(i - 1f);
            if (i > lastNoteTime)
            {
                lastNoteTime = i;
            }
        }
        */

        prepareApproach(KeyCode.Keypad1, k1);
        prepareApproach(KeyCode.Keypad2, k2);
        prepareApproach(KeyCode.Keypad3, k3);
        prepareApproach(KeyCode.Keypad4, k4);
        prepareApproach(KeyCode.Keypad5, k5);
        prepareApproach(KeyCode.Keypad6, k6);
        prepareApproach(KeyCode.Keypad7, k7);
        prepareApproach(KeyCode.Keypad8, k8);
        prepareApproach(KeyCode.Keypad9, k9);

        Debug.Log("last note at " + lastNoteTime);

        //TODO PLAY MP3
    }

    private void prepareApproach(KeyCode keyC, GameObject hitK)
    {
        foreach (float i in song.hitMap[keyC])
        {
            //Debug.Log(i - f);
            hitK.GetComponent<HitDetect>().invokeApproach(i - 1f);
            if (i > lastNoteTime)
            {
                lastNoteTime = i;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > (lastNoteTime + 5f))
        {
            onFinishedMap(scoreText.score);
        }
    }

    public void setSong(SongCondensed s)
    {
        song = s;
    }

    public void buttonPressed(KeyCode k, float time, HitDetect hd)
    {
        if (song != null && song.hitMap.ContainsKey(k) && lastCheck[k] < song.hitMap[k].Length)
        {
            float offTiming = (time - startTime) - song.hitMap[k][lastCheck[k]];
            Debug.Log(offTiming + " " + song.hitMap[k][lastCheck[k]]);
            if (offTiming >= 0)
            {
                //get closest on the left
                for (int i = lastCheck[k]; i < song.hitMap[k].Length; i++)
                {
                    float lastTime = song.hitMap[k][i];
                    //if shorter, use that one
                    if ((time - startTime) - lastTime < offTiming && (time - startTime) - lastTime >= 0)
                    {
                        offTiming = (time - startTime) - lastTime;

                    }
                    //found shortest, make next one as start to check
                    else
                    {
                        break;
                    }
                }
            }

            //if in range
            if (Mathf.Abs(offTiming) <= 1f)
            {
                hd.gameObject.GetComponent<SpriteRenderer>().sprite = hd.spriteA;

                lastCheck[k] = lastCheck[k] + 1;

                if (Mathf.Abs(offTiming) <= 0.33)
                {
                    score += 500;
                    scoreText.gameObject.GetComponent<Text>().text = score.ToString();
                    Debug.Log("500 note at " + song.hitMap[k][lastCheck[k]]);
                    //hd.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                }
                else if (Mathf.Abs(offTiming) <= 0.66)
                {
                    score += 250;
                    scoreText.gameObject.GetComponent<Text>().text = score.ToString();
                    Debug.Log("250 note at " + song.hitMap[k][lastCheck[k]]);
                    hd.gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
                }
                else
                {
                    Debug.Log("MISSED note at " + song.hitMap[k][lastCheck[k]]);
                    hd.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                }

            }
            //if miss late, move to next one
            else if (offTiming > 0)
            {
                lastCheck[k] = lastCheck[k] + 1;
            }
        }
    }


    private void LoadGameData()
    {

        var stream = File.OpenText("./Assets/Maps/map3.json");

        string json = stream.ReadToEnd();
        //Debug.Log(json);
        SongMap s = new SongMap();
        JsonUtility.FromJsonOverwrite(json, s);
        this.song = new SongCondensed(s);

        foreach (float i in song.hitMap[KeyCode.Keypad1])
        {
            Debug.Log(i);
        }

        //SongCondensed = new 

        //setSong(new SongCondensed(SongMap.CreateFromJSON(json)));

        /*
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = Path.Combine(Application.streamingAssetsPath, "Maps/map1.json");

        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            setSong(new SongCondensed(SongMap.CreateFromJSON(filePath)));
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
        */
    }


    public void onFinishedMap(int score)
    {
        if (!isFinished)
        {
            isFinished = true;
            Debug.Log("Finished songId=" + song.id + " with score=" + score);
            //TODO SEND SCORE
            //ws.Send("{intent:\"submitscore\",username:\"" + username +"\",mapid:" + counter + "}");

            SceneManager.LoadScene(1);

            
        }

    }

    public void scoreRecieved(MessageEventArgs e)
    {
        Debug.Log(e.Data);
    }


}
