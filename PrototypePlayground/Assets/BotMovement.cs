using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BotMovement : MonoBehaviour
{
    Animator anim;
    [SerializeField]float acceleration = 1.5f;
    [SerializeField]float deceleration = 2.0f;
    float maxWalkSpeed = 0.5f;
    float maxRunSpeed = 2.0f;
    [SerializeField] float horzAxis;
    [SerializeField] float vertAxis;
    float velocityX;
    float velocityZ;

    Rigidbody rb;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

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
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        /*
        bool isWalking = Input.GetKey("w");
        bool isRight = Input.GetKey("d");
        bool isLeft = Input.GetKey("a");
        */
        horzAxis = Input.GetAxis("Horizontal");
        vertAxis = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey("left shift");
        
        if(horzAxis != 0 && !Input.GetKey("a") && !Input.GetKey("d")){horzAxis = 0;}
        

        float maxVelocity = (isRunning) ? maxRunSpeed : maxWalkSpeed;
        
        //player is holding key, continue movement
        
        if(/*isWalking*/vertAxis != 0){
            velocityZ += (Time.deltaTime * acceleration) * vertAxis;
            if(!isLegalVelocity(velocityZ, maxVelocity)){
                if(isRunning){velocityZ = maxVelocity;}
                else{velocityZ -= Time.deltaTime * deceleration;}}
        } else { //vertAxis == 0 ; no vertical input
            velocityZ = resetVelocity(velocityZ);
        }


        if(horzAxis != 0.0f){
            velocityX += (Time.deltaTime * acceleration) * horzAxis;
            if(!isLegalVelocity(velocityX, maxVelocity)){
                if(isRunning){velocityX = (velocityX > 0) ? maxVelocity : -maxVelocity;}
                else{
                    velocityX = (velocityX > 0) ? velocityX - Time.deltaTime * deceleration : velocityX + Time.deltaTime * deceleration;
                    //velocityX -= Time.deltaTime * deceleration;}
                    //velocityX = resetVelocity(velocityX);
                }   
            }
        } else { //horzAxis == 0 ; no horizontal input 
            velocityX = resetVelocity(velocityX);
        }
        /*
        if(horzAxis > 0.0f){
            velocityX += Time.deltaTime * acceleration;
            if(!isLegalVelocity(velocityX, maxVelocity)){
                if(isRunning){velocityX = maxVelocity;}
                else{velocityX -= Time.deltaTime * deceleration;}
            }
        }
        if(horzAxis < 0.0f){
            velocityX -= Time.deltaTime * acceleration;
            if(!isLegalVelocity(velocityX, maxVelocity)){
                if(isRunning){velocityX = -maxVelocity;}
                else{velocityX += Time.deltaTime * deceleration;}
            }
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
        Vector3 temp = new Vector3(velocityX, 0, velocityZ);
        rb.MovePosition(rb.transform.position + temp);
        //rb.velocity = new Vector3(velocityX, 0, velocityZ);
        
        Debug.Log("Horz: " + horzAxis + "\nVert: " + vertAxis);
    }

    bool isLegalVelocity(float velocity, float maxVelocity){
        return (Math.Abs(velocity) < maxVelocity);
    }

    float resetVelocity(float velocity){
        float dummyVal = 0;
        bool isPositive = (velocity > 0);
        if(isPositive) {dummyVal = (Time.deltaTime * deceleration > velocity) ? 0 : velocity - Time.deltaTime * deceleration;}
        else {dummyVal = (Time.deltaTime * deceleration > -velocity) ? 0 : velocity + Time.deltaTime * deceleration;}
        return dummyVal;
    }

}
