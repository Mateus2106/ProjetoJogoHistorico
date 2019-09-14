using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DropItens : MonoBehaviour
{
    private Enemy_Master enemyMaster;
    public GameObject[] itensToDrop;

    void OnEnable()
    {
        SetInitialReferences();
        enemyMaster.EventEnemyDie += DropItens;
    }

    void OnDisable()
    {
        enemyMaster.EventEnemyDie -= DropItens;
    }

    void SetInitialReferences()
    {
        enemyMaster = GetComponent<Enemy_Master>();
    }

    void DropItens()
    {
        if(itensToDrop.Length > 0)
        {
            foreach(GameObject item in itensToDrop)
            {
                StartCoroutine(PauseBeforeDrop(item));
            }
        }
    }

    IEnumerator PauseBeforeDrop(GameObject itemToDrop)
    {
        yield return new WaitForSeconds(0.05f);
        itemToDrop.SetActive(true);
        itemToDrop.transform.parent = null;
        yield return new WaitForSeconds(0.05f);
        if(itemToDrop.GetComponent<Item_Master>() != null)
        {
            Debug.Log("teste");
            itemToDrop.GetComponent<Item_Master>().CallEventObjectThrow();
        }
    }
}
