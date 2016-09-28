
﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehaviour : MonoBehaviour
{
    public bool LeftMovementFlag;
    public bool RightMovementFlag;
    public bool JumpFlag;
    public bool FacingRight = true;
    bool timer = true;
    bool move = true;
    public bool CanJump;
    public bool Crouching;
    public bool IsActive = true;
    Vector2 HorizontalVelocity;
    Vector2 VerticalVelocity;
    Vector3 StartPosition;
    float JumpingDuration = 1.0f;
    float NextLevelTimer = 0.0f;
    float ResetLevelTimer = 0.0f;
    public float JumpingTimer;
    public float HorizontalMovementSpeed = 65.0f;
    public float VerticalMovementSpeed = 69.0f;
    public GameObject MemoryStanding;
    public GameObject MemoryCrouch;
    GameObject[] Hazards;
    public Sprite[] CharacterSprites;
    public List<GameObject> Clones;
    public int MaxClones = 5;
    public Quaternion PlayerRotation;
    public Vector3 PreviousPosition;
    public Sprite FrozenStandingManSprite;
    public Sprite WallGrabbingSprite;

    public AudioSource JumpSound;
    public AudioSource CloneSound;
    public AudioSource EndOfLevelSound;
    public AudioSource DieSound;
    public AudioClip HazardDeathSound;

    float currenttimesincelevelload;

    void OnLevelWasLoaded(int level)
    {
        Hazards = GameObject.FindGameObjectsWithTag("Hazard");
        GameObject UICanvas = GameObject.Find("Canvas");
        if (UICanvas != null)
        {
            UICanvas.GetComponent<UIManager>().SetMemoriesText("Memories: " + MaxClones.ToString());
        }
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
            if (move) HandleKeyInputs();
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
            GameObject UICanvas = GameObject.Find("Canvas");
            if (UICanvas != null)
            {
                if (timer) UICanvas.GetComponent<UIManager>().SetTimerText(Time.timeSinceLevelLoad.ToString("F2"));
            }
        }

        LevelTransitions();
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
                    GetComponent<Animator>().Play("PlayerWallGrabbingAnim");
                }
                else if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) <= 0.02f)
                {
                    //GetComponent<Animator>().Play();
                    GetComponent<Animator>().Play("PlayerRunningAnim");
                }
            }
            else if (RightMovementFlag && !LeftMovementFlag)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(HorizontalMovementSpeed, GetComponent<Rigidbody2D>().velocity.y);
                //transform.rotation.Set(0.0f, 0.0f, 0.0f, 1.0f);
                FacingRight = true;
                if (Vector3.Magnitude(PreviousPosition - transform.position) < (HorizontalMovementSpeed * Time.fixedDeltaTime / 2))
                {
                    GetComponent<Animator>().Play("PlayerWallGrabbingAnim");
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
            PreviousPosition = transform.position;
        }
    }

    void OnCollisionEnter2D(Collision2D Col)
    {
        GetComponent<Animator>().Play("PlayerStandingAnim");
        if (Col.gameObject.tag == "EndFlag")
        {
            currenttimesincelevelload = Time.timeSinceLevelLoad;
            GameObject gameManager = GameObject.Find("GameManager");
            EndOfLevelSound.Play();
            timer = false;
            move = false;
            LeftMovementFlag = false;
            RightMovementFlag = false;
            JumpFlag = false;
            NextLevelTimer = 1.0f;
        }
        if (Col.gameObject.tag == "Hazard")
        {
            IsActive = false;
            GetComponent<Rigidbody2D>().velocity *= 0.0f;
            GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            GetComponent<SpriteRenderer>().enabled = false;
            LeftMovementFlag = false;
            RightMovementFlag = false;
            AudioSource.PlayClipAtPoint(HazardDeathSound, transform.position);
            GameObject gameManager = GameObject.Find("GameManager");
            ResetLevelTimer = 1.0f;
            //StartCoroutine(HazardCollision());
        }
    }

    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.gameObject.tag == "EndFlag")
        {
            currenttimesincelevelload = Time.timeSinceLevelLoad;
            GameObject gameManager = GameObject.Find("GameManager");
            EndOfLevelSound.Play();
            timer = false;
            move = false;
            LeftMovementFlag = false;
            RightMovementFlag = false;
            JumpFlag = false;
            NextLevelTimer = 1.0f;
        }
        if (Col.gameObject.tag == "Hazard")
        {
            IsActive = false;
            GetComponent<Rigidbody2D>().velocity *= 0.0f;
            GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            GetComponent<SpriteRenderer>().enabled = false;
            LeftMovementFlag = false;
            RightMovementFlag = false;
            AudioSource.PlayClipAtPoint(HazardDeathSound, transform.position);
            GameObject gameManager = GameObject.Find("GameManager");
            ResetLevelTimer = 1.0f;
            //StartCoroutine(HazardCollision());
        }
    }

    void HandleKeyInputs()
    {
        //Crouch down
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    Debug.Log("Crouched");
        //    Crouching = true;
        //    if (CanJump)
        //    {
        //        GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, GetComponent<Rigidbody2D>().velocity.y);
        //        LeftMovementFlag = false;
        //        RightMovementFlag = false;
        //    }
        //    GetComponent<SpriteRenderer>().sprite = CharacterSprites[1];
        //}

        //if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) <= 0.001f && Crouching)
        //{
        //    Debug.Log("This is happening");
        //    GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        //    LeftMovementFlag = false;
        //    RightMovementFlag = false;
        //}

        ////Crouch up
        //if (Input.GetKeyUp(KeyCode.DownArrow))
        //{
        //    Crouching = false;
        //    GetComponent<SpriteRenderer>().sprite = CharacterSprites[0];
        //}

        if (!Crouching)
        {
            //Left movement down
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
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
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (!RightMovementFlag)
                {
                    GetComponent<Rigidbody2D>().velocity += HorizontalVelocity;
                    //transform.rotation.Set(0.0f, 0.0f, 0.0f, 0.0f);
                    //FacingRight = true;
                    RightMovementFlag = true;
                }
            }

            //Left movement up
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
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
            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
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
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) <= 0.2f)
                {
                    if (CanJump)
                    {
                        JumpFlag = true;
                        GetComponent<Animator>().Play("PlayerJumpAnim");
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
             transform.rotation) as GameObject;

            if (GetComponent<Rigidbody2D>().velocity.x == 0.0f && GetComponent<Rigidbody2D>().velocity.y == 0.0f)
            {
                myRoadInstance.GetComponent<SpriteRenderer>().sprite = FrozenStandingManSprite;
            }


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

    void LevelTransitions()
    {
        if (NextLevelTimer > 0.0f)
        {
            NextLevelTimer -= Time.deltaTime;
            if (NextLevelTimer <= 0.0f)
            {
                GameObject gameManager = GameObject.Find("GameManager");
                gameManager.GetComponent<GameManager>().UpdateTimes(currenttimesincelevelload);
                gameManager.GetComponent<GameManager>().GoToNextLevel();
            }
        }
        if (ResetLevelTimer > 0.0f)
        {
            ResetLevelTimer -= Time.deltaTime;
            if (ResetLevelTimer <= 0.0f)
            {
                GameObject gameManager = GameObject.Find("GameManager");
                gameManager.GetComponent<GameManager>().RestartLevel();
            }
        }
    }
}