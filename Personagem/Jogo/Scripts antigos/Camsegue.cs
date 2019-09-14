using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camsegue : MonoBehaviour
{
    public Transform Cabeca;
    public Transform[] pos;
    public int id;
    public Vector3 vel = Vector3.zero;
    private RaycastHit hit;
    private float rotVel, rotacao;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        rotVel = 150;
        id = 0;
    }

    // Update is called once per frame
    void Update()
    {
     
        RotacaoCam(Player);

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
    }

    void LateUpdate()
    {
        transform.LookAt(Cabeca);
        if(!Physics.Linecast(Cabeca.position, pos[id].position))
        {
            transform.position = Vector3.SmoothDamp(transform.position, pos[id].position, ref vel, 0.4f);
            Debug.DrawLine(Cabeca.position, pos[id].position);
        }
        else if(Physics.Linecast(Cabeca.position, pos[id].position, out hit))
        {
            transform.position = Vector3.SmoothDamp(transform.position, hit.point, ref vel, 0.4f);
        }

    }

    void RotacaoCam(Transform obj)
    {
        rotacao = Input.GetAxis("CameraRot") * rotVel;
        rotacao *= Time.deltaTime;
        obj.Rotate(0, rotacao, 0);
    }
}
