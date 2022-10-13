using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController Pico;
    Animator PicoAnimator;

    Vector2 movementVector;
    Vector3 characterMovement;


    float targetAngle;

    public Transform cam;
    public float turnSmoothVelocity;
    public float turnSmoothTime;
    public bool isMovermentPressed = false;
    public bool isAim = false;


    // Start is called before the first frame update
    private void Awake()
    {
        Pico = GetComponent<CharacterController>();
        PicoAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        //isAim = false;
    }

    private void FixedUpdate()
    {
        moveByKeyBoard();
        checkAnimator();
    }

    //Move
    void moveByKeyBoard()
    {
        //check When has input
        if (isMovermentPressed)
        {
            Pico.Move(characterMovement * Time.deltaTime);
            characterRotation();
        }
        //Add Gravity 
        else if (!isMovermentPressed)
        {
            characterMovement = new Vector3(characterMovement.x, -9.8f, characterMovement.z);
            Pico.Move(characterMovement * Time.deltaTime);
        }
    }
    //Check Rotation 
    void characterRotation()
    {
        targetAngle = Mathf.Atan2(movementVector.x, movementVector.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
    //Get Input From New InputSystem
    void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
        characterMovement.x = movementVector.x;
        characterMovement.z = movementVector.y;
        characterMovement.y = Physics.gravity.y;
        isMovermentPressed = movementVector.x != 0 || movementVector.y != 0;
    }

    //Check Animator
    void checkAnimator()
    {
        if (isMovermentPressed)
        {
            PicoAnimator.SetBool("IsMoving", true);
        }
        else if (!isMovermentPressed)
        {
            PicoAnimator.SetBool("IsMoving", false);
        }
        if (isAim)
        {
            PicoAnimator.SetBool("Aim", true);
        }
        else if (!isAim)
        {
            PicoAnimator.SetBool("Aim", false);
        }
    }
    void OnAim()
    {
        if (isAim)
        {
            isAim = false;
        }
        else
        {
            isAim = true;
        }
    }
}
