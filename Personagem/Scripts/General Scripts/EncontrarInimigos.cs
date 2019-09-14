using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace capitulo1
{
    public class EncontrarInimigos : MonoBehaviour
    {
        GameObject[] inimigos;
        void Start()
        {

        }

        void Update()
        {

        }

        void procurarInimigos()
        {
            inimigos = GameObject.FindGameObjectsWithTag("Inimigo");

            if(inimigos.Length > 0)
            {
                foreach(GameObject alvo in inimigos)
                {
                    Debug.Log(alvo.name);
                }
            }
        }
    }
}
