using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    // Score
    [Header("Score Management")]
    public int updateScore;
    private int score = 0;
    private int toAchieveScore;

    [Space(1)]
    [Header("Input Control")]
    public float minSwipeDistY;
    public float minSwipeDistX;

    private Vector2 startPos;
    private float finalPosition;
    private string direction;

    [Space(1)]
    [Header("Player Control")]
    public float updateSpeed;
    public float speed;
    public float rollingSpeed;
    public float jumping;

    public bool jumpAble = true;

    private bool dead = false;
    private bool moving = false;
    private Transform playerTransform;
    private Rigidbody playerRb;
    private Level1UI controller;
    private MapGenerator manager;

    private void Awake()
    {
        manager = GameObject.Find("_GameSetting").GetComponent<MapGenerator>();
        toAchieveScore = updateScore;
        playerTransform = this.gameObject.GetComponent<Transform>();
        finalPosition = playerTransform.position.x;
        playerRb = this.gameObject.GetComponent<Rigidbody>();
        controller = GameObject.Find("PauseButton").GetComponent<Level1UI>();
    }

    void Update()
    {
        if (playerTransform.position.y <= -10f && dead == false)
        {

            Death();
        }
        if (dead == false)
        {
            playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z + speed * Time.deltaTime);
            PlayerMovement();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "DeletePlayer" && this.gameObject.tag == "Player" && dead == false)
        {
            Death(); 
        }

        if (collision.gameObject.tag == "Ground")
        {
            jumpAble = true;
            if (score >= toAchieveScore)
            {
                toAchieveScore += updateScore;
                speed += updateSpeed;
                rollingSpeed += updateSpeed;
                if(speed%1 == 0 ) // If "speed" is whole number increment "Not Spawn after me"
                    manager.NotSpawnAfterMeIncrement++;
            }
        }
    }

    public void GetCoin()
    {
        score++;
        controller.ScoreUp(score);
    }
    void Death()
    {
        dead = true;
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<Collider>().enabled = false;
        this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        GameObject.Find("Explosion").GetComponent<Explosion>().enabled = true;

        controller.AnimatingUI(controller.ScoreUI, "NextScene");
        controller.AnimatingUI(controller.PlayUI, "NextScene");

        StartCoroutine(Decay());
    }

    IEnumerator Decay()
    {
        yield return new WaitForSeconds(2);
        controller.GameOver(score);
    }
    void PlayerMovement()
    {
        if(jumpAble == false) transform.rotation = Quaternion.Euler(0, 0, 0);
        if (moving == true) HorizontalMove();
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];
                switch (touch.phase)
                {
                    case TouchPhase.Began:

                        startPos = touch.position;
                        break;

                    case TouchPhase.Ended:

                        float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
                        float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
                        int decision = 0;

                        if (swipeDistHorizontal < minSwipeDistX && jumpAble == true && (touch.position.y - startPos.y) >= -5f) // tf
                        {
                            playerRb.AddForce(new Vector3(0f ,1f ,0f) * jumping);
                            jumpAble = false;
                        Debug.Log("  ,   " + jumping);
                            // Jumping Animation
                        }
                        else if (swipeDistHorizontal < minSwipeDistX && jumpAble == false && swipeDistVertical >= minSwipeDistY && (touch.position.y - startPos.y) <= -5f)
                        {
                            playerRb.AddForce(new Vector3(0f, -1f, 0f) * jumping);
                            // Shrink
                        }
                        if (swipeDistHorizontal >= minSwipeDistX && moving == false)
                        {
                            decision = MoveDirection(touch.position.x, startPos.x);
                            
                            float x = (playerTransform.position.x < 0f) ?(float)((int) playerTransform.position.x - 0.5f) : (float)((int)playerTransform.position.x + 0.5f);
                            finalPosition = (decision == 1) ? finalPosition = x + 1f : finalPosition = x - 1f;
                            direction = (decision == 1) ? direction = "Right" : direction = "Left";
                            moving = true;
                        }
                        break;
                }
            }
    }

    void HorizontalMove()
    {
        if (direction == "Right")
            MoveRight();
        if (direction == "Left")
            MoveLeft();
    }
    void MoveRight()
    {
        if (finalPosition > playerTransform.position.x)
        {
            playerTransform.position = new Vector3(playerTransform.position.x + rollingSpeed * Time.deltaTime, playerTransform.position.y, playerTransform.position.z);
        }
        if(finalPosition < playerTransform.position.x)
        {
            playerTransform.position = new Vector3(finalPosition, playerTransform.position.y, playerTransform.position.z);
            finalPosition = 0;
            moving = false;
        }
    }
    void MoveLeft()
    {
        if (finalPosition < playerTransform.position.x)
        {
            playerTransform.position = new Vector3(playerTransform.position.x - rollingSpeed * Time.deltaTime, playerTransform.position.y, playerTransform.position.z);
        }
        if (finalPosition > playerTransform.position.x)
        {
            playerTransform.position = new Vector3(finalPosition, playerTransform.position.y, playerTransform.position.z);
            finalPosition = 0;
            moving = false;
        }
    }

    int MoveDirection( float endSwipePosition, float startSwipePosition)
    {
            float swipeValue = Mathf.Sign(endSwipePosition - startSwipePosition);
            if(swipeValue > 0)
            {
                return 1;
            //Right
            //Jump
            
            }
            else
            {
                return 2;
            //Left
            //Shrink
            
            }
    }
}
