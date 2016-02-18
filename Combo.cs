using UnityEngine;
using System.Collections;

public class Combo : MonoBehaviour
{
    public int combo;
    public AudioClip comboBreaker;
    Collider lastColid;
    public float comboTime;
    float trailComboSize;
    int comboChecker;
    public ParticleSystem trail;
    public UnityEngine.UI.Text comboDisplay;
    public UIManager uiManager;
    //Player player;
    // Use this for initialization
    void Start()
    {
        comboTime = 2.25f;
        //comboTime = 10f;
        combo = 0;
        comboChecker = 0;
        trail = GameObject.Find("Trail").GetComponent<ParticleSystem>();
        //player = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        comboDisplay.text = "x" + combo;
        comboDisplay.color = new Color(.9f - combo / 20f, .9f - combo / 20f, 1f - combo/30f,1);
        trail.startColor = new Color(comboDisplay.color.r,comboDisplay.color.g,comboDisplay.color.b, .3f + Mathf.Pow(.3f, (float)combo));
        trail.emissionRate = 40 + 20 * combo;
        if (combo > 30)
        {
            trailComboSize = 0.06f * 30;
        }
        else
        {
            trailComboSize = 0.06f * combo;
        }
        trail.startSize = .40f + trailComboSize;
        
    }


    public IEnumerator CCCOMBO(Collider colid)
    {
        comboChecker++;
        print("Called" + combo);
        if (colid.GetType() == typeof(MeshCollider) && lastColid != colid)
        {
            lastColid = colid;
            combo++;
        }
        uiManager.comboBar.fillAmount = 1;
        yield return new WaitForSeconds(comboTime);
        if (comboChecker == 1)
        {
            if (combo > 20)
            {
                GetComponent<AudioSource>().clip = comboBreaker;
                GetComponent<AudioSource>().Play();
            }
            combo = combo/2;
            if (gameObject.GetComponent<Player>().canJump)
            {
                combo = 0;
            }
            else
            {
                StartCoroutine("CCCOMBO",colid);
            }
        }
        comboChecker--;
    }
}
