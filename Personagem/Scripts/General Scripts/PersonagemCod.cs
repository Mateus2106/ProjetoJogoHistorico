using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PersonagemCod : MonoBehaviour
{
    public static float InputX, InputZ;
    public Vector3 dirMoveDesejada;
    public float velRotDesejada = 0.1f;
    public Animator anim;
    public float Speed;
    public float permiteRotPlayer = 0.3f;
    public Camera cam;
    public float verticalVel;
    public Vector3 moveVector;

    public CinemachineVirtualCamera vcam;
    public float[] posCam;
    public int id;
    public CinemachineFramingTransposer composer;
    private Vector3 cameraMoveVel;

    public InputManager inp;
    private CharacterController heroiCh;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;

        inp = Gamemanager.inst.inputM;
        heroiCh = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputMagnitude();
        if (Input.GetButtonDown("CameraAjust"))
        {
            composer.m_CameraDistance = posCam[id];
        }

        Vector3 movimento = new Vector3(inp.horizontal, 0, inp.vertical);
        heroiCh.SimpleMove(movimento * Time.deltaTime * 150);

        anim.SetFloat("Z", inp.horizontal, 0.1f, Time.deltaTime);
        anim.SetFloat("X", inp.vertical, 0.1f, Time.deltaTime);

        if (movimento.magnitude > 0)
        {
            Quaternion novaDir = Quaternion.LookRotation(movimento);
            transform.rotation = Quaternion.Slerp(transform.rotation, novaDir, 0.08f);
        }

        float rotacaoH = Input.GetAxis("CameraRot");
        transform.Rotate(Vector3.up, rotacaoH * Time.deltaTime * 150);

        if(inp.vertical != 0)
        {
            heroiCh.SimpleMove(transform.forward * 150 * Time.deltaTime * inp.vertical);
        }
        if(inp.horizontal != 0)
        {
            heroiCh.SimpleMove(transform.right * 150 * Time.deltaTime * inp.horizontal);
        }
    }

    void PlayerMoveRot()
    {
        Quaternion rot = new Quaternion(0, 0, 0, 0);

        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        Vector3 frente = cam.transform.forward;
        Vector3 direita = cam.transform.right;

        frente.Normalize();
        direita.Normalize();

        dirMoveDesejada = frente * InputZ + direita * InputX;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirMoveDesejada), velRotDesejada);
        transform.rotation = new Quaternion(0, rot.y, 0, rot.w);
    }

    void InputMagnitude()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        anim.SetFloat("Z", InputZ, 0.0f, Time.deltaTime * 2);
        anim.SetFloat("X", InputX, 0.0f, Time.deltaTime * 2);

        Speed = new Vector2(InputX, InputZ).sqrMagnitude;
        if(Speed > permiteRotPlayer)
        {
            anim.SetFloat("InputMagnitude", Speed, 0.1f, Time.deltaTime);
            PlayerMoveRot();
        }
        else if(Speed < permiteRotPlayer)
        {
            anim.SetFloat("InputMagnitude", Speed, 0.1f, Time.deltaTime);
        }
    }
}
