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
        if(content == null)
        {
            Gem[] gems = FindObjectsOfType<Gem>();
            foreach(Gem gem in gems)
            {
                if(gem.transform == transform)
                {
                    DestroyObject(gem);
                }
            }
        }
        Initialize();
    }
    
    public void Initialize()
    {
        Instantiate(content, transform);
    }
}
