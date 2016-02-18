using UnityEngine;
using System.Collections;

public class Driver : MonoBehaviour
{
    public UnityEngine.UI.Text heightCounter;
    public UnityEngine.UI.Text recentHeightDisplay;
    public UnityEngine.UI.Text messageBoard;
    public GameObject escMenu;
    public GameObject fire;
    public GameObject deathMenu;
    public Transform Building;
    public Transform RedGate;
    public Transform heightDisplay;
    public float spawnSpeed;
    public Transform player;
    public Material RedGateMaterial;
    int difficulty;
    public int recentHeight;
    public float topHeight = 0;
    public int highScore;
    float spawnHeight;
    float autoSpawnHeightChange;
    public bool settingUp;
    bool killerFloorBegin = false;
    bool waitForSpawn = false;
    bool waitForSpawnGate = false;
    // Use this for initialization
    void Start()
    {
        StartCoroutine("startSpawning");
        StartCoroutine("floorStart");
        spawnHeight = 50f;
        difficulty = 1;
        autoSpawnHeightChange = .7f;
        GetComponent<DataHandling>().Load();
        recentHeightDisplay.text = "Last Score: " + recentHeight;
        //Cursor Stuff
        Cursor.lockState = UnityEngine.CursorLockMode.Locked;
        Cursor.visible = false;
    }

    IEnumerator startSpawning()
    {
        settingUp = true;
        spawnSpeed = .1f;
        messageBoard.text = "Setting up game";
        yield return new WaitForSeconds(5f);
        messageBoard.text = "";
        spawnSpeed = .5f;
        settingUp = false;
    }

    IEnumerator floorStart()
    {
        yield return new WaitForSeconds(15f);
        GetComponent<Renderer>().material = RedGateMaterial;
        fire.SetActive(true);
        killerFloorBegin = true;
    }
    // Update is called once per frame
    void Update()
    {
        //Height Counter
        if (player.transform.position.y > topHeight)
        {
            topHeight = player.transform.position.y;
            heightCounter.text = "Height: " + (int)topHeight;
        }
        //Open the menu with escape
        //Use to Unlock the cursor if you want to quit etc.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = UnityEngine.CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            Cursor.visible = !Cursor.visible;
            escMenu.SetActive(!escMenu.activeSelf);
        }
        //Spawn speed change based on difficulty
        if ((int)topHeight / 10 + 1 > difficulty)
        {
            increaseDifficulty();
        }

        if (!player.gameObject.activeInHierarchy)
        {
            endMenu();
        }

        heightDisplay.transform.position = new Vector3(0, topHeight, 0);

        //Fix the weird spawn lag
        

        if (spawnHeight < topHeight + 100)
        {
            spawnHeight += autoSpawnHeightChange;
            Instantiate(Building, new Vector3(Random.Range(-20f, 20f), Random.Range(spawnHeight - 3f, spawnHeight + 3f), Random.Range(-20f, 20f)), new Quaternion());
        }
    }

    void FixedUpdate()
    {
        if (!waitForSpawn)
        {
            StartCoroutine("spawnBuilding");
        }
        if (!waitForSpawnGate)
        {
            StartCoroutine("spawnRedGate");
        }
        if (killerFloorBegin)
        {
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + .01f + .002f * difficulty, transform.position.z);
            GetComponent<BoxCollider>().isTrigger = true;
        }
    }

    void OnTriggerEnter(Collider colid)
    {
        if (killerFloorBegin)
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

    void increaseDifficulty()
    {
        difficulty++;
        spawnSpeed = .5f + .1f * difficulty;
        autoSpawnHeightChange = .7f + .1f * difficulty;
    }

    IEnumerator spawnRedGate()
    {
        waitForSpawnGate = true;
        int redGateSpawn = 10;
        if (difficulty > 40)
        {
            redGateSpawn = 0;
        }
        else
        {
            redGateSpawn = 10 - difficulty / 4;
        }
        yield return new WaitForSeconds(1f);

        if ((int)Random.Range(0, redGateSpawn) == 0)
        {
            Instantiate(RedGate, new Vector3(Random.Range(-20f, 20f), spawnHeight + 30f, Random.Range(-20f,
                20f)), new Quaternion());
        }
        waitForSpawnGate = false;
    }

    IEnumerator spawnBuilding()
    {
        waitForSpawn = true;
        Instantiate(Building, new Vector3(Random.Range(-20f, 20f), Random.Range(spawnHeight - 3f, spawnHeight + 3f), Random.Range(-20f, 20f)), new Quaternion());

        yield return new WaitForSeconds(spawnSpeed);
        waitForSpawn = false;
    }

    void endMenu()
    {
        //GetComponent<AudioSource>().Play();
        if (topHeight > highScore)
        {
            highScore = (int)topHeight;
        }
        GetComponent<AudioListener>().enabled = true;
        GetComponent<LeaderBoard>().totalScore = (int)topHeight;
        GetComponent<LeaderBoard>().saveScore();
        recentHeight = (int)topHeight;
        deathMenu.gameObject.SetActive(true);
        messageBoard.text = "High Score: " + highScore;
        GetComponent<DataHandling>().Save();
    }

    public void LeaderBoardLoad()
    {
        GetComponent<DataHandling>().Save();
        Application.LoadLevel(3);
    }

    public void Quit()
    {
        GetComponent<DataHandling>().Save();
        Application.Quit();
    }

    public void Retry()
    {
        GetComponent<DataHandling>().Save();
        Application.LoadLevel(2);
    }

    public void NameSelect()
    {
        GetComponent<DataHandling>().Save();
        Application.LoadLevel(1);
    }
}
