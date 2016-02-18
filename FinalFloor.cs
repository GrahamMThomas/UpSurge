using UnityEngine;
using System.Collections;

public class FinalFloor : MonoBehaviour
{
    GameObject floor;
    bool killer;
    // Use this for initialization
    void Start()
    {
        floor = GameObject.Find("Floor");
    }

    // Update is called once per frame
    void Update()
    {
        if (floor.GetComponent<Driver>().topHeight > 1025)
        {
            killer = true;
        }
    }

    void OnTriggerEnter(Collider colid)
    {
        if (killer && colid.gameObject.name == "Player")
        {
            Camera.main.transform.parent = null;
            colid.gameObject.SetActive(false);
            Cursor.lockState = UnityEngine.CursorLockMode.None;
            Cursor.visible = true;
        }

    }
}
