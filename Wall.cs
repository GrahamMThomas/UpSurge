using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{

    void OnTriggerEnter(Collider colid)
    {
        if (colid.gameObject.name == "Player")
        {
            Camera.main.transform.parent = null;
            colid.gameObject.SetActive(false);
            Cursor.lockState = UnityEngine.CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Destroy(colid.gameObject);
        }
    }
}
