using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour {

    public GameObject myWeapon;
    public GameObject weaponOnGroud;

	// Use this for initialization
	void Start () {
        myWeapon.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            myWeapon.SetActive(true);
            weaponOnGroud.SetActive(false);
        }
    }
}
