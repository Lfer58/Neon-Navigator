using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private int level;

        void OnTriggerEnter(Collider collider) {
            if(collider.gameObject.tag == "Player"){
                SceneManager.LoadScene(level);
            }
                
        }
    }

