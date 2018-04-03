using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {
    [SerializeField] GameObject content;
    int x, y;
    public int size;

    public void SetContent(GameObject type)
    {
        content = type;
    }
    
    public void Initialize()
    {
        Instantiate(content, transform);
    }
}
