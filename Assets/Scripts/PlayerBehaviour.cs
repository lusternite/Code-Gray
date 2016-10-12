
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehaviour : MonoBehaviour
{
    //Flags
    public bool LeftMovementFlag;
    public bool RightMovementFlag;
    public bool JumpFlag;
    public bool FacingRight = true;
    bool LevelTimerOn = true;
    bool CanMove = true;
    public bool CanJump;
    public bool IsActive = true;

    //Physics
    Vector2 HorizontalVelocity;
    Vector2 VerticalVelocity;
    Vector3 StartPosition;
    public float HorizontalMovementSpeed = 65.0f;
    public float VerticalMovementSpeed = 100.0f;
    public Quaternion PlayerRotation;
    public Vector3 PreviousPosition;

    //Timers
    float JumpingDuration = 1.0f;
    float NextLevelTimer = 0.0f;
    float RespawnTimer = 0.0f;
    public float JumpFlagTimer = 0.0f;
    public float JumpingTimer;
    float CloneTimer = 0.0f;

    //Prefabs
    public GameObject MemoryStanding;
    public GameObject MemoryCrouch;
    public int MemoryHealth = 1000;
    GameObject[] Hazards;
    public Sprite[] CharacterSprites;
    public List<GameObject> Clones;
    public int MaxClones = 5;
    public Sprite FrozenStandingManSprite;
    public Sprite WallGrabbingSprite;
    public AudioSource JumpSound;
    public AudioSource CloneSound;
    public AudioSource EndOfLevelSound;
    public AudioSource DieSound;
    public AudioClip HazardDeathSound;
    GameObject door;
    bool inDoor = false;

    float currenttimesincelevelload;

    void OnLevelWasLoaded(int level)
    {
        Hazards = GameObject.FindGameObjectsWithTag("Hazard");
    }

    public int GetMaxClones()
    {
        return MaxClones;
    }


    // Use this for initialization
    void Start()
    {
        HorizontalVelocity = new Vector2(HorizontalMovementSpeed, 0.0f);
        VerticalVelocity = new Vector2(0.0f, VerticalMovementSpeed);
        StartPosition = transform.position;
        IsActive = true;
        PlayerRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive)
        {
            for (int i = 0; i < Clones.Count; ++i)
            {
                Debug.Log(Clones[i].GetComponent<MemoryScript>().GetHealth());
                if (Clones[i].GetComponent<MemoryScript>().GetHealth() <= 0)
                {
                    Destroy(Clones[i].gameObject);
                    Clones.RemoveAt(i);

                    //Reset all buttons
                    Button[] Buttons = FindObjectsOfType<Button>();
                    foreach (Button button in Buttons)
                    {
                        button.ButtonPressed = false;
                    }
                }
            }

            if (CanMove) HandleKeyInputs();
            //HandleJumping();
            if (FacingRight)
            {
                transform.rotation = Quaternion.identity;
            }
            else
            {
                transform.rotation = new Quaternion(0.0f, 1.0f, 0.0f, 0.0f);
            }
            //Debug.Log("Velocity.y = " + GetComponent<Rigidbody2D>().velocity.y);
        }

        HandleTimers();
    }

    void FixedUpdate()
    {
        if (IsActive)
        {

            if (JumpFlag)
            {
                GetComponent<Rigidbody2D>().velocity += VerticalVelocity;
                JumpFlag = false;
            }
            if (!LeftMovementFlag && !RightMovementFlag && !JumpFlag && GetComponent<Rigidbody2D>().velocity.y == 0.0f)
            {
                //GetComponent<Animator>().StopPlayback();
                GetComponent<Animator>().Play("PlayerStandingAnim");
                GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
            }
            else if (LeftMovementFlag && !RightMovementFlag)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-HorizontalMovementSpeed, GetComponent<Rigidbody2D>().velocity.y);
                //transform.rotation.Set(0.0f, 1.0f, 0.0f, 1.0f);
                FacingRight = false;
                if (Vector3.Magnitude(PreviousPosition - transform.position) < (HorizontalMovementSpeed * Time.fixedDeltaTime / 2))
                {
                    //GetComponent<Animator>().Play("PlayerWallGrabbingAnim");
                }
                else if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) <= 0.02f)
                {
                    //GetComponent<Animator>().Play();
                    GetComponent<Animator>().Play("PlayerRunningAnim");
                }
                //Debug.Log(GetComponent<Rigidbody2D>().velocity);
            }
            else if (RightMovementFlag && !LeftMovementFlag)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(HorizontalMovementSpeed, GetComponent<Rigidbody2D>().velocity.y);
                //transform.rotation.Set(0.0f, 0.0f, 0.0f, 1.0f);
                FacingRight = true;
                if (Vector3.Magnitude(PreviousPosition - transform.position) < (HorizontalMovementSpeed * Time.fixedDeltaTime / 2))
                {
                    //GetComponent<Animator>().Play("PlayerWallGrabbingAnim");
                }
                else if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) <= 0.02f)
                {
                    GetComponent<Animator>().Play("PlayerRunningAnim");
                }
            }
            else
            {
                if (!(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) > 0.02f))
                {
                    GetComponent<Animator>().Play("PlayerStandingAnim");
                }
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            }
            HandleWallDetection();
            PreviousPosition = transform.position;
        }
    }

    void OnCollisionEnter2D(Collision2D Col)
    {
        if (Col.gameObject.tag == "Hazard")
        {
            CanMove = false;
            IsActive = false;
            GetComponent<Rigidbody2D>().velocity *= 0.0f;
            GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            GetComponent<SpriteRenderer>().enabled = false;
            LeftMovementFlag = false;
            RightMovementFlag = false;
            AudioSource.PlayClipAtPoint(HazardDeathSound, transform.position);
            GameObject gameManager = GameObject.Find("GameManager");
            RespawnTimer = 1.0f;
            GetComponent<BoxCollider2D>().enabled = false;
            //StartCoroutine(HazardCollision());
        }
    }

    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.gameObject.tag == "EndFlag")
        {
            if (!Col.gameObject.GetComponent<LevelDoorScript>().GetIsLocked())
            {
                inDoor = true;
                GameObject ddoor = Col.gameObject;
                door = ddoor;
                door.GetComponent<LevelDoorScript>().SetIsOpen(true);
            }
        }
        if (Col.gameObject.tag == "Flag")
        {
            Debug.Log("NANIII");
            StartPosition = transform.position;
        }
        if (Col.gameObject.tag == "Hazard" && IsActive)
        {
            Kill();
            //StartCoroutine(HazardCollision());
        }
        if (Col.gameObject.tag == "Collectible")
        {
            switch (Col.gameObject.GetComponent<CollectibleScript>().GetCollectibleType())
            {
                case Type.MEMORY:
                    {
                        MaxClones += 1;
                        Destroy(Col.gameObject);
                        GameObject.Find("Canvas").GetComponent<UIManager>().SetMemories(MaxClones);
                        break;
                    }
            }
        }
    }

    public void Kill()
    {
        CanMove = false;
        IsActive = false;
        GetComponent<Rigidbody2D>().velocity *= 0.0f;
        GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        LeftMovementFlag = false;
        RightMovementFlag = false;
        AudioSource.PlayClipAtPoint(HazardDeathSound, transform.position);
        //GameObject gameManager = GameObject.Find("GameManager");
        RespawnTimer = 1.0f;
    }

    void OnTriggerExit2D(Collider2D Col)
    {
        if (Col.gameObject.tag == "EndFlag")
        {
            inDoor = false;
            door.GetComponent<LevelDoorScript>().SetIsOpen(false);
        }
    }

    void HandleKeyInputs()
    {
        //Left movement down
        if (Input.GetAxis("Horizontal") < 0.0f)
        {
            if (!LeftMovementFlag)
            {
                //GetComponent<Rigidbody2D>().velocity += -HorizontalVelocity;
                LeftMovementFlag = true;
                //FacingRight = false;
                //transform.rotation.Set(0.0f, 1.0f, 0.0f, 0.0f);
            }
        }

        //Right movement down
        if (Input.GetAxis("Horizontal") > 0.0f)
        {
            if (!RightMovementFlag)
            {
                GetComponent<Rigidbody2D>().velocity += HorizontalVelocity;
                //transform.rotation.Set(0.0f, 0.0f, 0.0f, 0.0f);
                //FacingRight = true;
                RightMovementFlag = true;
            }
            Debug.Log("Right arrowkey down");
        }

        //Left movement up
        if (Input.GetAxis("Horizontal") == 0.0f || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            if (LeftMovementFlag)
            {
                GetComponent<Rigidbody2D>().velocity += HorizontalVelocity;
                if (!RightMovementFlag)
                {
                    //GetComponent<Animator>().Play("PlayerStandingAnim");
                }
                LeftMovementFlag = false;
                Debug.Log("Left arrowkey up");
            }

        }

        //Right movement up
        if (Input.GetAxis("Horizontal") == 0.0f || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            if (RightMovementFlag)
            {
                GetComponent<Rigidbody2D>().velocity += -HorizontalVelocity;
                if (!LeftMovementFlag)
                {
                    //GetComponent<Animator>().Play("PlayerStandingAnim");
                }
                RightMovementFlag = false;
                Debug.Log("Right arrowkey up");
            }
        }

        //Jump
        if (Input.GetAxis("Jump") > 0.0f /*|| Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)*/)
        {
            if (inDoor)
            {
                currenttimesincelevelload = Time.timeSinceLevelLoad;
                GameObject gameManager = GameObject.Find("GameManager");
                EndOfLevelSound.Play();
                LevelTimerOn = false;
                CanMove = false;
                LeftMovementFlag = false;
                RightMovementFlag = false;
                JumpFlag = false;
                NextLevelTimer = 1.0f;
            }
            else
            {
                if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) <= 2.6f)
                {
                    if (CanJump)
                    {
                        JumpFlag = true;
                        GetComponent<Animator>().Play("PlayerJumpAnim");
                        JumpSound.Play();
                        CanJump = false;
                    }
                }
            }
        }

        //Freeze Mechanic
        if (Input.GetAxis("Fire1") > 0.0f && CloneTimer < 0.0f /*Input.GetKeyDown(KeyCode.Space) && CanMove*/)
        {
            GameObject myRoadInstance =
             Instantiate(MemoryStanding,
             transform.position,
             transform.rotation) as GameObject;

            if (GetComponent<Rigidbody2D>().velocity.x == 0.0f && GetComponent<Rigidbody2D>().velocity.y == 0.0f)
            {
                myRoadInstance.GetComponent<SpriteRenderer>().sprite = FrozenStandingManSprite;
            }
            myRoadInstance.GetComponent<MemoryScript>().health = MemoryHealth;

            Clones.Add(myRoadInstance);
            if (Clones.Count > MaxClones)
            {
                GameObject.Destroy(Clones[0]);
                Clones.RemoveAt(0);
            }

            CloneTimer = 1.0f;
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

    void HandleTimers()
    {
        if (NextLevelTimer > 0.0f)
        {
            NextLevelTimer -= Time.deltaTime;
            if (NextLevelTimer <= 0.0f)
            {
                door.GetComponent<LevelDoorScript>().GoToLevel();
            }
        }
        if (RespawnTimer > 0.0f)
        {
            RespawnTimer -= Time.deltaTime;
            if (RespawnTimer <= 0.0f)
            {
                //GameObject gameManager = GameObject.Find("GameManager");
                //gameManager.GetComponent<GameManager>().RestartLevel();
                transform.position = StartPosition;
                IsActive = true;
                CanMove = true;
                GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<Rigidbody2D>().gravityScale = 1.0f;
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        if (JumpFlagTimer > 0.0f)
        {
            JumpFlagTimer -= Time.deltaTime;
            if (JumpFlagTimer <= 0.0f)
            {
                CanJump = false;
            }
        }
        CloneTimer -= Time.deltaTime;
    }

    //Uses raycasting to detect walls on either side
    //Will slide if walls are detected
    void HandleWallDetection()
    {
        if (GetComponent<Rigidbody2D>().velocity.y <= 0.0f)
        {
            RaycastHit2D leftHit = Physics2D.Raycast(new Vector2(transform.position.x - 0.4f, transform.position.y), Vector2.left, 0.05f);
            RaycastHit2D rightHit = Physics2D.Raycast(new Vector2(transform.position.x + 0.4f, transform.position.y), Vector2.right, 0.05f);
            if (leftHit)
            {
                if (leftHit.transform.tag == "Object")
                {
                    if (LeftMovementFlag)
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -4.4f);
                    }
                    Debug.Log("Wall detected - left");
                    Debug.Log(leftHit.transform.name);
                }
            }
            else if (rightHit)
            {
                if (rightHit.transform.tag == "Object")
                {
                    if (RightMovementFlag)
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -4.4f);
                    }
                    Debug.Log("Wall detected - right");
                    Debug.Log(rightHit.transform.name);
                }
            }
        }

    }
}