using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallIdle : MonoBehaviour {

    public float startAmplitude;
    public float startSpeed;

    public float amplitude;
    public float speed;
    
    private float startPosX;
    private float startPosY;
    private float startPosZ;

    private void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        startPosZ = transform.position.z;

        amplitude = startAmplitude + Random.Range(-0.2f, 0.2f);
        speed = startSpeed + Random.Range(-0.2f, 0.2f);
    }

    void Update () {
        float rand = Random.Range(1,2);
        float rand2 = Random.Range(1, 2);

        transform.position = new Vector3(
            startPosX + amplitude * (Mathf.Cos(Time.time * speed * rand)),
            startPosY,
            startPosZ + amplitude * (Mathf.Sin(Time.time * speed * rand2 )));
        
	}
}
