using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehaviour : MonoBehaviour
{

    public bool LeftMovementFlag;
    public bool RightMovementFlag;
    public bool JumpFlag;
    bool timer = true;
    bool move = true;
    public bool CanJump;
    public bool Crouching;
    Vector2 HorizontalVelocity;
    Vector2 VerticalVelocity;
    Vector3 StartPosition;
    float JumpingDuration = 1.0f;
    public float JumpingTimer;
    public float HorizontalMovementSpeed = 65.0f;
    public float VerticalMovementSpeed = 69.0f;
    public GameObject MemoryStanding;
    public GameObject MemoryCrouch;
    GameObject[] Hazards;
    public Sprite[] CharacterSprites;
    public List<GameObject> Clones;
    public int MaxClones = 5;

    public AudioSource JumpSound;
    public AudioSource CloneSound;
    public AudioSource EndOfLevelSound;
    public AudioSource DieSound;
    public AudioClip HazardDeathSound;

    void OnLevelWasLoaded(int level)
    {
        Hazards = GameObject.FindGameObjectsWithTag("Hazard");
        GameObject UICanvas = GameObject.Find("Canvas");
        if (UICanvas != null)
        {
            UICanvas.GetComponent<UIManager>().SetMemoriesText("Memories: " + MaxClones.ToString());
        }
    }

    IEnumerator EndLevelFlagCollision()
    {
        EndOfLevelSound.Play();
        GameObject gameManager = GameObject.Find("GameManager");
        timer = false;
        move = false;
        LeftMovementFlag = false;
        RightMovementFlag = false;
        JumpFlag = false;
        float time = Time.timeSinceLevelLoad;
        gameManager.GetComponent<GameManager>().UpdateTimes(time);
        yield return new WaitForSeconds(1);
        gameManager.GetComponent<GameManager>().GoToNextLevel();
    }

    IEnumerator HazardCollision()
    {
        DieSound.Play();
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {
            Debug.Log("Death by hazard");
            AudioSource.PlayClipAtPoint(HazardDeathSound, transform.position);
            timer = false;
            float time = Time.timeSinceLevelLoad;
            gameManager.GetComponent<GameManager>().UpdateTimes(time);
            yield return new WaitForSeconds(1);
            gameManager.GetComponent<GameManager>().RestartLevel();
        }
    }

    // Use this for initialization
    void Start()
    {
        HorizontalVelocity = new Vector2(HorizontalMovementSpeed, 0.0f);
        VerticalVelocity = new Vector2(0.0f, VerticalMovementSpeed);
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (move) HandleKeyInputs();
        //HandleJumping();
        transform.rotation = Quaternion.identity;
        //Debug.Log("Velocity.y = " + GetComponent<Rigidbody2D>().velocity.y);
        GameObject UICanvas = GameObject.Find("Canvas");
        if (UICanvas != null)
        {
            if (timer) UICanvas.GetComponent<UIManager>().SetTimerText(Time.timeSinceLevelLoad.ToString("F2"));
        }
        
    }

    void FixedUpdate()
    {
        if (JumpFlag)
        {
            GetComponent<Rigidbody2D>().velocity += VerticalVelocity;
            JumpFlag = false;
        }
        if (!LeftMovementFlag && !RightMovementFlag && !JumpFlag && GetComponent<Rigidbody2D>().velocity.y == 0.0f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        }
        if (LeftMovementFlag)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-HorizontalMovementSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else if (RightMovementFlag)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(HorizontalMovementSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D Col)
    {

    }

    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.gameObject.tag == "EndFlag")
        {
            StartCoroutine(EndLevelFlagCollision());
        }
        if (Col.gameObject.tag == "Hazard")
        {
            StartCoroutine(HazardCollision());
        }
    }

    void HandleKeyInputs()
    {
        //Crouch down
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Crouched");
            Crouching = true;
            if (CanJump)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, GetComponent<Rigidbody2D>().velocity.y);
                LeftMovementFlag = false;
                RightMovementFlag = false;
            }
            GetComponent<SpriteRenderer>().sprite = CharacterSprites[1];
        }

        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) <= 0.001f && Crouching)
        {
            Debug.Log("This is happening");
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
            LeftMovementFlag = false;
            RightMovementFlag = false;
        }

        //Crouch up
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            Crouching = false;
            GetComponent<SpriteRenderer>().sprite = CharacterSprites[0];
        }

        if (!Crouching)
        {
            //Left movement down
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (!LeftMovementFlag)
                {
                    //GetComponent<Rigidbody2D>().velocity += -HorizontalVelocity;
                    LeftMovementFlag = true;
                }
            }

            //Right movement down
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (!RightMovementFlag)
                {
                    GetComponent<Rigidbody2D>().velocity += HorizontalVelocity;
                    RightMovementFlag = true;
                }
            }

            //Left movement up
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                if (LeftMovementFlag)
                {
                    GetComponent<Rigidbody2D>().velocity += HorizontalVelocity;
                    LeftMovementFlag = false;
                    Debug.Log("Left arrowkey up");
                }

            }

            //Right movement up
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                if (RightMovementFlag)
                {
                    GetComponent<Rigidbody2D>().velocity += -HorizontalVelocity;
                    RightMovementFlag = false;
                    Debug.Log("Right arrowkey up");
                }
            }

            //Jump
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) <= 0.2f)
                {
                    if (CanJump)
                    {
                        JumpFlag = true;
                        JumpSound.Play();
                    }

                }
            }
        }

        //Freeze Mechanic
        if (Input.GetKeyDown(KeyCode.Space) && move)
        {
            GameObject myRoadInstance =
             Instantiate(MemoryStanding,
             transform.position,
             Quaternion.identity) as GameObject;

            Clones.Add(myRoadInstance);
            if (Clones.Count > MaxClones)
            {
                GameObject.Destroy(Clones[0]);
                Clones.RemoveAt(0);
            }

            transform.position = StartPosition;
            CloneSound.Play();

            //Reset all buttons
            Button[] Buttons = FindObjectsOfType<Button>();
            foreach (Button button in Buttons)
            {
                button.ButtonPressed = false;
            }
        }
    }


}
