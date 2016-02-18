using UnityEngine;
using System.Collections;

public class RedGate : MonoBehaviour {
    public static float dropSpeed;
    //GameObject floor;

    // Use this for initialization
    void Start () {
        //Make it see through
        Color C = GetComponent<Renderer>().material.color;
        C.a = .7f;
        GetComponent<Renderer>().material.color = C;     
        //Initiallizations   
        //floor = GameObject.Find("Floor");
        dropSpeed = .15f;
    }
    // Update is called once per frame
    void Update () {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - dropSpeed, transform.position.z);
        if (gameObject.transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider colid)
    {
        if (colid.gameObject.name == "Player")
        {
            Camera.main.transform.parent = null;
            colid.gameObject.SetActive(false);
            Cursor.lockState = UnityEngine.CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
