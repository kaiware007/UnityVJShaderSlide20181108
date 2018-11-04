using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCursor : MonoBehaviour {

    [SerializeField]
    bool cursorVisible = false;

    public void CursorVisible(bool enable)
    {
        cursorVisible = enable;
        Cursor.visible = cursorVisible;
    }

	// Use this for initialization
	void Start () {
        Cursor.visible = cursorVisible;
    }
	
}
