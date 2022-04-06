using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Renderer myRenderer;
    public bool button = false;
    Color red = Color.red;
    Color green = Color.green;
    public GameObject exit;
    public Transform player;
    public GameObject text;
    public GameObject gatlingGun;


    private void Start()
    {
        text.SetActive(false);
        gatlingGun.SetActive(false);
        myRenderer = gameObject.GetComponent<Renderer>();
        myRenderer.material.color = red;
        
    }

    private void Update()
    {
        //checking distance between player and the button;
        float distance = Vector3.Distance(player.position, transform.position);
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 10) && distance < 10)
            {
                myRenderer.material.color = green;
                button = true;
                if (text != null)
                {
                    text.SetActive(true);
                    exit.SetActive(false);

                }
            }
        }
        if (button == true)
        {
            exit.SetActive(false);
            if(gatlingGun != null)
                gatlingGun.SetActive(true);
        }
    }
    

}
