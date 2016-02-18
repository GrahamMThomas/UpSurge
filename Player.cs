using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float jumpPower;
    public float glidePower;
    public AudioClip impact;
    public AudioClip Jump;
    public AudioClip bImpact;
    public AudioClip bJump;
    public AudioClip blImpact;
    public AudioClip blJump;
    public Material bSkinDef;
    public Material bSkin1;
    public Material bSkin2;
    public Material bSkin3;
    public Material bSkin4;
    public Material bSkin5;
    public GameObject tester;
    Combo comboMaker;
    float xAxis = 0;
    public GameObject deathMenu;
    float yAxis = 0;
    bool jumpResetStopper = true;
    public bool canJump;
    bool touchingFloor;
    Rigidbody rBody;

    // Use this for initialization
    void Start()
    {
        touchingFloor = false;
        glidePower = 20f;
        comboMaker = GetComponent<Combo>();
        jumpPower = 1500f;
        canJump = true;
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Boosting your character.
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        if (Input.GetMouseButtonDown(0) && canJump && !deathMenu.activeSelf)
        {
            //Particle trail burst
            comboMaker.trail.startSpeed = 2.5f;
            comboMaker.trail.Play();
            StartCoroutine("trailReset");
            //Audio
            //settings sounds based on combo
            if (comboMaker.combo < 16)
            {
                GetComponent<AudioSource>().clip = Jump;
            }
            else if (comboMaker.combo < 30)
            {
                GetComponent<AudioSource>().clip = bJump;
            }
            else
            {
                GetComponent<AudioSource>().clip = blJump;
            }
            GetComponent<AudioSource>().Play();

            //Jump Physics
            Vector3 jumpDirection = new Vector3(Camera.main.transform.forward.x * jumpPower, Camera.main.transform.forward.y * jumpPower, Camera.main.transform.forward.z * jumpPower);
            rBody.AddForce(jumpDirection);
            rBody.useGravity = true;
            canJump = false;
        }
        //Combo correlation to jump speed
        jumpPower = 1500f + comboMaker.combo * 100f;
    }

    IEnumerator trailReset()
    {
        yield return new WaitForSeconds(.5f);
        comboMaker.trail.startSpeed = 0f;
    }
    void FixedUpdate()
    {

        if (!canJump || touchingFloor || rBody.useGravity)
        {
            //Have some movement capabilities without a jump. Forward/Backward
            Vector3 jumpDirection = new Vector3(Camera.main.transform.forward.x * glidePower * yAxis, 0, Camera.main.transform.forward.z * glidePower * yAxis);
            rBody.AddForce(jumpDirection);
            //Have some movement capabilities without a jump. Left/Right
            jumpDirection = new Vector3(Camera.main.transform.right.x * glidePower * xAxis, 0, Camera.main.transform.right.z * glidePower * xAxis);
            rBody.AddForce(jumpDirection);
        }
        if (!rBody.useGravity)
        {
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - Building.dropSpeed, transform.position.z);
        }
        else
        {
            //Disable drag effects on gravity.
            rBody.AddForce(1 * Physics.gravity);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.activeSelf && other.gameObject.name != "Floor")
        {
            StartCoroutine(gameObject.GetComponent<Combo>().CCCOMBO(other));
        }

        rBody.velocity = Vector3.zero;
        //settings sounds based on combo
        if (comboMaker.combo < 16)
        {
            GetComponent<AudioSource>().clip = impact;
        }
        else if (comboMaker.combo < 30)
        {
            GetComponent<AudioSource>().clip = bImpact;
        }
        else
        {
            GetComponent<AudioSource>().clip = blImpact;
        }

        if (gameObject.activeSelf)
        {
            GetComponent<AudioSource>().Play();
        }

        rBody.useGravity = false;
        canJump = true;
    }

    void OnTriggerStay(Collider colid)
    {
        if (colid.gameObject.name == "Floor")
        {
            touchingFloor = true;
        }
        if (jumpResetStopper)
        {
            StartCoroutine("jumpReset");
        }
    }

    //If two blocks spawn ontop of each other, the colliders broke and made it so you couldn't jump. This is the fix.
    IEnumerator jumpReset()
    {
        jumpResetStopper = false;
        canJump = true;
        yield return new WaitForSeconds(.5f);
        jumpResetStopper = true;
    }
    /*
    void OnCollisionEnter(Collision coll)
    {
        gameObject.transform.position = coll.contacts[0].point;
    }
    */

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Floor")
        {
            touchingFloor = false;
        }
    }

    public Material selectSkin(string ballSkin)
    {
        if (ballSkin == "Happy Face")
            return bSkin1;
        if (ballSkin == "Soccer Ball")
            return bSkin2;
        if (ballSkin == "Patriot")
            return bSkin3;
        if (ballSkin == "Pokeball")
            return bSkin4;
        if (ballSkin == "Gold")
            return bSkin5;
        return bSkinDef;
    }
}
