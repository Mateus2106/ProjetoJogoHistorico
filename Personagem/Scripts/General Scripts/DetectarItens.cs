using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace capitulo1
{
    public class DetectarItens : MonoBehaviour
    {
        private RaycastHit hit;
        [SerializeField] private LayerMask camadaDeteccao;
        private float range = 5;
        private float checarPorcentagem = 0.5f;
        private float proximaChecagem;
        private Transform meuTransform;

        void Start()
        {
            setarReferenciasIniciais();
        }
 
        void Update()
        {
            detectarItens();
            camadaDeteccao = 1 << 9 | 1 << 8;
        }

        void setarReferenciasIniciais()
        {
            meuTransform = transform;
        }

        void detectarItens()
        {
            if(Time.time > proximaChecagem)
            {
                proximaChecagem = Time.time + checarPorcentagem;
                if(Physics.Raycast(meuTransform.position, meuTransform.forward, out hit, range, camadaDeteccao))
                {
                    Debug.Log(transform.name + "é um item!");
                }
            }
        }
    }
}
