using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace capitulo1
{
    public class Atirar : MonoBehaviour
    {
        private float fireRate = 0.3f;
        private float nextFire;
        private RaycastHit hit;
        private float range = 300;
        private Transform myTransform;

        void Start()
        {
            setInitialReferences();
        }

        void setInitialReferences()
        {
            myTransform = transform;
        }

        void Update()
        {
            checkInput();
        }

        void checkInput()
        {
            if (Input.GetButton("Fire1") && Time.time > nextFire){
                Debug.DrawRay(myTransform.TransformPoint(0, 0, 1), myTransform.forward, Color.green, 3);
                if(Physics.Raycast(myTransform.TransformPoint(0,0,1), myTransform.forward, out hit, range))
                {
                    if (hit.transform.CompareTag("Enemy"))
                    {
                        Debug.Log("Enemy" + hit.transform.name);
                    }
                    else
                    {
                        Debug.Log("Não é um inimigo");
                    }
                    Debug.Log(hit.transform.name);
                }
                nextFire = Time.time + fireRate;
                
            }
        }
    }
}
