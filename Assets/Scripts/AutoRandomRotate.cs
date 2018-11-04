using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRandomRotate : MonoBehaviour {
    public float rotSpeed = 50f;
    public float noiseSpeed = 0.1f;

    Vector3 noisePos;
    float angle = 0;

	// Use this for initialization
	void Start () {
        noisePos = Random.insideUnitSphere * 10000f;
    }
	
	// Update is called once per frame
	void Update () {
        float ns = noiseSpeed * Time.deltaTime;

        noisePos.x += ns;
        noisePos.y += ns;
        noisePos.z += ns;

        float x = Mathf.PerlinNoise(noisePos.x, noisePos.y);
        float y = Mathf.PerlinNoise(noisePos.y, noisePos.z);
        float z = Mathf.PerlinNoise(noisePos.z, noisePos.x);
        Vector3 axis = new Vector3(x, y, z);
        axis.Normalize();

        angle += rotSpeed * Time.deltaTime;

        transform.rotation = Quaternion.AngleAxis(angle, axis);
    }
}
