using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {
    public Transform hover;
    [SerializeField]
    public Gem selected;
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            hover = hitInfo.transform;
            Debug.Log("Hit");
            if(Input.GetMouseButtonDown(0))
            {
                selected = hitInfo.transform.gameObject.GetComponent<Gem>();
            }
        }
    }
}
