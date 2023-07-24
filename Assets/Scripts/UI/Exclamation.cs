using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class Exclamation : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(true);
    }
    public void Show(bool on)
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(on);
        }
    }
}
