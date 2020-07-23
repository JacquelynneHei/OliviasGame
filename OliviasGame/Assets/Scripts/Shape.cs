using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shape : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public Transform dragParent;
    public Transform dropParent;
    public float sizeDecreaseSpeed;     //the speed in which to decrease the scale on a correct answer
    public Transform dropLocation;      //the drop location that is correct
    private Vector3 startLocation;     //the location of the shape when it spawns into the level

    bool isCorrect;     //boolean to tell us if we got the correct answer

    private void Start()
    {
        startLocation = transform.position;
        dragParent = GameObject.Find("Canvas").transform;
        dropParent = GameObject.Find("Container").transform;

    }

    private void Update()
    {
        if (isCorrect)
        {
            Debug.Log("Correct");
            Vector3 myScale = transform.localScale;
            myScale -= new Vector3(sizeDecreaseSpeed, sizeDecreaseSpeed, 0f) * Time.deltaTime;
            transform.localScale = myScale;
        }

        if (transform.localScale.x < 0 && transform.localScale.y < 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent = dragParent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0;
        transform.position = newPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (Vector2.Distance(transform.position, dropLocation.position) < 1f)
        {
            transform.position = dropLocation.position;
            isCorrect = true;
        }
        else
        {
            transform.position = startLocation;
            transform.parent = dropParent;
        }
    }
}
