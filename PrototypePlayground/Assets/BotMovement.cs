using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BotMovement : MonoBehaviour
{
    Animator anim;
    float acceleration = 1.0f;
    float deceleration = 1.0f;
    float maxWalkSpeed = 0.5f;
    float maxRunSpeed = 2.0f;
    float velocityX;
    float velocityZ;

    int velocityXHash, velocityZHash;
    //string isWalkingString = "isWalking";
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        velocityXHash = Animator.StringToHash("velocityX");
        velocityZHash = Animator.StringToHash("velocityZ");
        velocityX = 0.0f;
        velocityZ = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        bool isWalking = Input.GetKey("w");
        bool isRight = Input.GetKey("d");
        bool isLeft = Input.GetKey("a");
        */
        float horzAxis = Input.GetAxis("Horizontal");
        float vertAxis = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey("left shift");
        
        
        float maxVelocity = (isRunning) ? maxRunSpeed : maxWalkSpeed;
        
        //player is holding key, continue movement
        
        if(/*isWalking*/vertAxis > 0){
            velocityZ += Time.deltaTime * acceleration;
            if(!isLegalVelocity(velocityZ, maxVelocity)){velocityZ = maxVelocity;}
        }
        if(horzAxis > 0.0f){
            velocityX += Time.deltaTime * acceleration;
            if(!isLegalVelocity(velocityX, maxVelocity)){velocityX = maxVelocity;}
        }
        if(horzAxis < 0.0f){
            velocityX -= Time.deltaTime * acceleration;
            if(!isLegalVelocity(velocityX, maxVelocity)){velocityX = -maxVelocity;}
        }

        //player has let go of key, ajdust speeds and check for valid values
        if(vertAxis == 0 && velocityZ > 0.0f){
            velocityZ = (Time.deltaTime * deceleration > velocityZ) ? 0 : velocityZ - Time.deltaTime * deceleration;
        }
        if(horzAxis == 0){
            if(velocityX > 0.0f){
                velocityX = (Time.deltaTime * deceleration > velocityX) ? 0 : velocityX - Time.deltaTime * deceleration;
            } else{
                velocityX = (Time.deltaTime * deceleration > -velocityX) ? 0 : velocityX + Time.deltaTime * deceleration;
            }
        }
        /*
        if(!isLeft && velocityX < 0.0f){
            velocityX = (Time.deltaTime * deceleration > -velocityX) ? 0 : velocityX + Time.deltaTime * deceleration;
        } */
        anim.SetFloat(velocityXHash, velocityX);
        anim.SetFloat(velocityZHash, velocityZ);
    }

    bool isLegalVelocity(float velocity, float maxVelocity){
        return (Math.Abs(velocity) < maxVelocity);
    }

    
}
