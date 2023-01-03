using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterControlScript : MonoBehaviour
{
    [SerializeField]
    private Image _noteImage_;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _noteImage_.enabled = true; 
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _noteImage_.enabled = false;
        }
    }
}


