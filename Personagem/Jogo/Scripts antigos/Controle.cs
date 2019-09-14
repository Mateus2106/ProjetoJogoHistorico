using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controle : MonoBehaviour
{
    private Animator persAnim;
    static public float moveX, moveZ;
    public Vector3 dirMoveDesejada;
    public float velRotDesejada = 0.1f;
    public float Speed;
    public float permiteRotPlayer = 0.3f;
    public Camera cam;
    public float verticalVel;
    public Vector3 moveVector;

    void Start()
    {
        persAnim = GetComponent<Animator>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        InputMagnitude();
    }

    void PlayerMoveRot()
    { 
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        Vector3 frente = cam.transform.forward;
        Vector3 direita = cam.transform.right;

        frente.Normalize();
        direita.Normalize();
    }

    void InputMagnitude()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        persAnim.SetFloat("Z", moveZ, 0.0f, Time.deltaTime * 2);
        persAnim.SetFloat("X", moveX, 0.0f, Time.deltaTime * 2);

        Speed = new Vector2(moveX, moveZ).sqrMagnitude;
        if (Speed > permiteRotPlayer)
        {
            persAnim.SetFloat("InputMagnitude", Speed, 0.1f, Time.deltaTime);
            PlayerMoveRot();
        }
        else if (Speed < permiteRotPlayer)
        {
            persAnim.SetFloat("InputMagnitude", Speed, 0.1f, Time.deltaTime);
        }
    }
}
