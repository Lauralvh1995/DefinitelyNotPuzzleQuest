using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {
    public Transform hover;
    [SerializeField]
    public Cell selected;
    public Cell target;
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            hover = hitInfo.transform;
            if(Input.GetMouseButtonDown(0) && !FindObjectOfType<TurnManager>().moving)
            {
                
                if (selected != null)
                {
                    target = hitInfo.transform.gameObject.GetComponentInParent<Cell>();
                }
                else
                {
                    selected = hitInfo.transform.gameObject.GetComponentInParent<Cell>();
                }

                if(selected != null && target != null)
                {
                    StartCoroutine(GetComponent<Board>().Move(selected, target));
                    selected = null;
                    target = null;
                }
            }
        }
    }
}
