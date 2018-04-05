using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {
    [SerializeField] GameObject content;
    int x, y;
    public int size;

    public IEnumerator SetContent(GameObject type)
    {
        content = type;
        if(content == null)
        {
            Gem[] gems = FindObjectsOfType<Gem>();
            foreach(Gem gem in gems)
            {
                if(gem.transform.position == transform.position)
                {
                    Destroy(gem.gameObject);
                    yield return null;
                    break;
                }
            }
        }
        if (content != null)
        {
            Initialize();
        }
    }

    public GameObject GetContent()
    {
        return content;
    }
    
    public void Initialize()
    {
        Gem[] gems = FindObjectsOfType<Gem>();
        foreach (Gem gem in gems)
        {
            if (gem.transform.position == transform.position)
            {
                Destroy(gem.gameObject);
            }
        }
        Instantiate(content, transform);
    }
}
