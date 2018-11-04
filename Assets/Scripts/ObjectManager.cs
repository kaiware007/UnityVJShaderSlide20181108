using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {

    [SerializeField]
    GameObject cube;

    [SerializeField]
    KeyCode objectVisibleChangeKey = KeyCode.Space;

	// Use this for initialization
	void Start () {
        cube.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(objectVisibleChangeKey))
        {
            cube.SetActive(!cube.activeSelf);
        }
	}
}
