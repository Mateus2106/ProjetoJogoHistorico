using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager inst;
    public InputManager inputM;

    private void Awake()
    {
        if(inst == null)
        {
            inst = this;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        inputM = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
