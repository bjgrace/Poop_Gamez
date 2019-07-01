using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int jumpMag;
    public int moveSpeed;
    public int airMob;

    private Rigidbody2D thisRigidbody;

    private int baseJumps;
    private int curJumps;
    private bool isAirborne;
    private bool isTargeting;
    private int orientation;
    // private SpriteRenderer playerRender;

    /*
    private void Awake()
    {
    Need to get the player's current orientation
        playerRender = GetComponent<SpriteRenderer>();
    }
    */
    void Start()
    {
        baseJumps = PlayerStats.JUMP_NUM;
        curJumps = baseJumps;
        isAirborne = true;
        isTargeting = false;
        orientation = 1;
        thisRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Check to see if the player's # of jumps changed
        if (PlayerStats.JUMP_NUM != baseJumps)
        {
            baseJumps = PlayerStats.JUMP_NUM;
            if (curJumps > baseJumps) curJumps = baseJumps;
        }

        // Get horizontal movement input
        float horAxis = Input.GetAxis("Horizontal");
        int dirMult = 0;
        if (horAxis > .1f) dirMult = 1;
        if (horAxis < -.1f) dirMult = -1;

        Debug.Log("dirmult: " + dirMult);

        // On jump...
        if (Input.GetButtonDown("Jump") == true && curJumps > 0)
        {
            Debug.Log("Should jump");
            curJumps--;
            thisRigidbody.velocity = new Vector2(moveSpeed * dirMult, jumpMag);
            isAirborne = true;
        }

        // On a directional movement input...
        if (dirMult != 0)
        {
            if (isAirborne)
            {
                // Allow ineffective altering of horizontal velocity, limited between moveSpeed extremes
                Vector2 curVel = thisRigidbody.velocity;
                float newX = curVel.x + (airMob * dirMult);
                if (newX > moveSpeed) newX = moveSpeed;
                if (newX < -moveSpeed) newX = -moveSpeed;
                thisRigidbody.velocity = new Vector2(newX, curVel.y);
            }
            else
            {
                thisRigidbody.velocity = new Vector2(moveSpeed * dirMult, 0);
            }
        }
        // If there's no movement input and not airborne, come to a stop
        else if (!isAirborne) thisRigidbody.velocity = new Vector2(0, 0);

        // On a dodge input...
        if (Input.GetButtonDown("Dash") == true)
        {
            if (isAirborne)
            {

            }
        }

        // Check if sprite needs to be flipped
        /*
        bool flipSprite = (playerRender.flipX ? (horAxis > 0.01f) : (horAxis < 0.01f));
        if (flipSprite)
        {
            playerRender.flipX = !playerRender.flipX;
        }
        */
    }

    public void StartAction(Vector2 startVector, Vector2 endVector, int startupFrames, int iFrames, int endFrames)
    {

    }

    public void LeftPlatform()
    {
        Debug.Log("Left platform");
        if (curJumps == baseJumps) curJumps--;
        isAirborne = true;
    }

    public void HitPlatform()
    {
        Debug.Log("Hit platform");
        curJumps = baseJumps;
        isAirborne = false;
    }
}
