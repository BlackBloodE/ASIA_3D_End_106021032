using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject ui;
    public float timer = 0;
    public int CD = 3;
    // Start is called before the first frame update
    void Start()
    {
        CD = Random.Range(0, CD);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer < CD)
        {
            ui.active = true;
            //timer = 0;
        }
        else if (timer > CD*2)
        {
            ui.active = true;
            timer = 0;
        } 
        else
        {
            ui.active = false;
        }
    }
}
