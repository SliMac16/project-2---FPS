using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Gun gunScript;
    
    public Transform player, gunContainer, fpsCam, gun;

    public float pickUpRange;
    private void Start()
    {
        gunScript.enabled = false;
    }
    // Start is called before the first frame update
    private void Update()
    {
        Vector3 distance = player.position - transform.position;
        if(distance.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E))
        {
            PickUpWeapon();

        }
    }
    void PickUpWeapon()
    {
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
        
        
        
        
        

        
    }
}
