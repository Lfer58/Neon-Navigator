using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDestruction : MonoBehaviour
{
    public float deadZone = -20;
    private PlayerController player;
    private PathCreation path;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        path = GameObject.FindGameObjectWithTag("Player").GetComponent<PathCreation>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!path.isPuzzleLevel) {
            if (transform.position.x <= player.transform.position.x + (-1 * transform.localScale.x + deadZone)) 
            {
                Destroy(gameObject);
            }
        }
    }
}
