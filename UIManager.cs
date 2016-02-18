using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public UnityEngine.UI.Image comboBar;
    public GameObject player;

	// Use this for initialization
	void Start () {
        comboBar.fillAmount = 0;
	}
	
	// Update is called once per frame
	void Update () {
        comboBar.fillAmount = ((comboBar.fillAmount* player.GetComponent<Combo>().comboTime) - Time.deltaTime)/ player.GetComponent<Combo>().comboTime;
	}
}
