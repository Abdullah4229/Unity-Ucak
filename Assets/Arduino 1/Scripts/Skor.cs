using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Skor : MonoBehaviour
{
    [SerializeField] Text skorT;
    int alan;
    void Start()
    {
        alan = 0;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Alan")
        {
            alan = alan + 1;
            skorT.text = "Skor: " + alan;
            if(alan >= 15)
            {
                SceneManager.LoadScene("Vurus");
            }
        }
    }
}