using UnityEngine;
using System.Collections;

public class TopHeight : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Color C = GetComponent<Renderer>().material.color;
        C.a = 0.2f;
        GetComponent<Renderer>().material.color = C;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
