using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitLayout : MonoBehaviour {

    public GameObject prefab;
    public int num = 4;
    public float radius = 1.5f;
    public Vector3 axis = Vector3.up;

	// Use this for initialization
	void Start () {
        float angleDiff = 2 * Mathf.PI / num;

        for(int i = 0; i < num; i++)
        {
            GameObject obj = GameObject.Instantiate(prefab, this.transform);
            float angle = angleDiff * i;
            obj.transform.localPosition = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
        }
	}
	
}
