using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAvançada : MonoBehaviour
{
    public Transform Cabeca;
    public Transform[] posCam;
    public Vector3 cameraMoveVel = Vector3.zero;
    public GameObject segueOBJ;
    public float limiteAng = 65.0f;
    public float inputSensit = 155.0f;
    public float mouseX, mouseY;
    public float rotY = 0, rotX = 0;
    public Vector3 rot;
    private Quaternion localRot;
    public Transform Player;
   

    
    public int id;
    private float rotVel, rotacao;
    public Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
        transform.position = posCam[id].position;
        rotVel = 150;
    }

    // Update is called once per frame
    void Update()
    {
        Atualizacao();
        if (Mathf.Abs(PersonagemCod.InputZ) == 0)
        {
            RotacaoCam(Cabeca);
        }
        else
        {
            RotacaoCam(Player);
        }
    }

    void AjusteCamera()
    {
        if (Input.GetButtonDown("CameraAjust") && id < 2)
        {
            id++;
        }
        else if (Input.GetButtonDown("CameraAjust") && id > 1)
        {
            id = 0;
        }
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, posCam[id].position, ref cameraMoveVel, 0.1f);
    }

    void Init()
    {
        rot = transform.localRotation.eulerAngles;
        rotY = rot.x;
        rotX = rot.y;
    }

    void Atualizacao()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        rotY += mouseX * inputSensit * Time.deltaTime;
        rotX += mouseY * inputSensit * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -limiteAng, limiteAng);
        localRot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = localRot;
    }

    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, segueOBJ.transform.position, ref cameraMoveVel, 0.1f);
        AjusteCamera();
    }

    void RotacaoCam(Transform obj)
    {
        rotacao = Input.GetAxis("CameraRot") * rotVel;
        rotacao *= Time.deltaTime;
        obj.Rotate(0, rotacao, 0);
    }
}
