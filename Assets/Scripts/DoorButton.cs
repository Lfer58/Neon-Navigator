using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField] private GameObject Button;
    [SerializeField] private GameObject door;
    private float timer;
    [SerializeField] private float buttonOffset;
    bool doorIsOpen = false;

    // Update is called once per frame

    void OnCollisionEnter(Collision collider) {
        Vector3 pos = transform.position;
        
        if(collider.gameObject.tag == "Player" && doorIsOpen == false) {
            pos.y -= buttonOffset;
            transform.position = pos;
            // door.transform.eulerAngles = new Vector3(0, 90, 0);
            door.SetActive(false);
            doorIsOpen = true;
        }

    }
}
