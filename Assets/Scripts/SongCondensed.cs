using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO od
public class SongCondensed {
    public Dictionary<KeyCode, float[]> hitMap = new Dictionary<KeyCode, float[]>();
    public int id;

    public SongCondensed(SongMap sm)
    {
        id = sm.id;

        hitMap[KeyCode.Keypad1] = sm.Presses1;
        hitMap[KeyCode.Keypad2] = sm.Presses2;
        hitMap[KeyCode.Keypad3] = sm.Presses3;
        hitMap[KeyCode.Keypad4] = sm.Presses4;
        hitMap[KeyCode.Keypad5] = sm.Presses5;
        hitMap[KeyCode.Keypad6] = sm.Presses6;
        hitMap[KeyCode.Keypad7] = sm.Presses7;
        hitMap[KeyCode.Keypad8] = sm.Presses8;
        hitMap[KeyCode.Keypad9] = sm.Presses9;
        
    }
}
