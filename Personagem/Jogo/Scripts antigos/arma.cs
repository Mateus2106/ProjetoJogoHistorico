using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class arma : MonoBehaviour
{
    public Image alvo;
    public float forca;
    public GameObject decalPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        VerificaAlvo();
    }

    void VerificaAlvo()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.GetComponent<Vilao>())
            {
                alvo.color = Color.red;
            }
            else
            {
                alvo.color = Color.white;
            }

            if (Input.GetButton("Fire1"))
            {
                if (hit.collider.GetComponent<Vilao>())
                {
                    hit.rigidbody.AddForce(-hit.normal * forca, ForceMode.Impulse);
                }
                else
                {
                    print("Alvo");
                    DecalInst(hit);
                }
            }
        }
        else
        {
            alvo.color = Color.white;
        }
   
    }

    private void DecalInst(RaycastHit hitInf)
    {
        var decalVar = Instantiate(decalPrefab);
        decalVar.transform.position = hitInf.point;
        decalVar.transform.forward = hitInf.normal * -1;
    }
}
