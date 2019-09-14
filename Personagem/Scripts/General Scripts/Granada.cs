using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace capitulo1
{
    public class Granada : MonoBehaviour
    {
        public GameObject granadaPrefab;
        private Transform meuTransform;
        public float forcaPropulcao;

        void Start()
        {
            setarReferenciasIniciais();
        }

        void setarReferenciasIniciais()
        {
            meuTransform = transform;
        }

        void Update()
        {
            if(Input.GetButton("Fire1"))
            {
                gerarGranada();
            }
        }

        void gerarGranada()
        {
            GameObject go = Instantiate(granadaPrefab, meuTransform.TransformPoint(0, 0, 0.5f), meuTransform.rotation);
            go.GetComponent<Rigidbody>().AddForce(meuTransform.forward * forcaPropulcao, ForceMode.Impulse);
            Destroy(go, 10);
        }
    }
}
