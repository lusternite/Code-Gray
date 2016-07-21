using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{

    public bool LeftMovementFlag;
    public bool RightMovementFlag;
    bool Jumping;
    public bool CanJump;
    public bool Crouching;
    Vector2 HorizontalVelocity;
    Vector2 VerticalVelocity;
    Vector2 StartPosition;
    float JumpingDuration = 1.0f;
    public float JumpingTimer;
    public float HorizontalMovementSpeed = 6.0f;
    public float VerticalMovementSpeed = 1000.0f;
    public GameObject MemoryStanding;
    public GameObject MemoryCrouch;
    GameObject[] Hazards;
    public Sprite[] CharacterSprites;

    void OnLevelWasLoaded(int level)
    {
        Hazards = GameObject.FindGameObjectsWithTag("Hazard");
    }

    void HazardCollision()
    {
        if (Hazards.Length > 0)
        {
            foreach (GameObject hazard in Hazards)
            {
                if (/*Collision with hazard == */ true)
                {
                    GameObject Camera = GameObject.Find("GameManager");
                    if (Camera != null)
                    {
                        //Camera.GetComponent<GameManager>.RestartLevel();
                    }
                }
            }
        }
    }

    void EndLevelFlagCollision()
    {
        if (/*Collision == */ true)
        {
            GameObject Camera = GameObject.Find("GameManager");
            if (Camera != null)
            {
                //Camera.GetComponent<GameManager>.GoToNextLevel();
            }
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
        HandleKeyInputs();
        //HandleJumping();
        transform.rotation = Quaternion.identity;
        Debug.Log("Velocity.y = " + GetComponent<Rigidbody2D>().velocity.y);
        if (!LeftMovementFlag && !RightMovementFlag && !Jumping && GetComponent<Rigidbody2D>().velocity.y == 0.0f)
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
        //HazardCollision();
        //EndLevelFlagCollision();
    }

    //void OnCollisionEnter2D(Collision2D Col)
    //{
    //    if ()
    //}

    void OnTriggerStay2D(Collider2D Col)
    {
        if (Col.gameObject.tag == "Object")
        {
            CanJump = true;
        }
    }

    void OnTriggerExit2D(Collider2D Col)
    {
        if (Col.gameObject.tag == "Object")
        {
            CanJump = false;
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
                    Debug.Log("Yes");
                }

            }

            //Right movement up
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                if (RightMovementFlag)
                {
                    GetComponent<Rigidbody2D>().velocity += -HorizontalVelocity;
                    RightMovementFlag = false;
                    Debug.Log("Yes");
                }
            }

            //Jump
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) <= 0.001f)
                {
                    //Jumping = true;
                    if (CanJump)
                    {
                        GetComponent<Rigidbody2D>().velocity = VerticalVelocity;
                    }

                }
            }
        }

        //Freeze Mechanic
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject myRoadInstance =
             Instantiate(MemoryStanding,
             transform.position,
             Quaternion.identity) as GameObject;

            transform.position = StartPosition;
        }
    }


}
