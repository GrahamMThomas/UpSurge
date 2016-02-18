using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
    public static float dropSpeed;
    GameObject floor;
	// Use this for initialization
	void Start () {
        floor = GameObject.Find("Floor");
        dropSpeed = .03f;
	}
	
	// Update is called once per frame
	void Update () {

	}

    void FixedUpdate()
    {
        if (floor.GetComponent<Driver>().settingUp)
        {
            dropSpeed = .15f;
        }
        else
        {
            dropSpeed = .03f;
        }
        if (transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - dropSpeed, transform.position.z);
    }

}
