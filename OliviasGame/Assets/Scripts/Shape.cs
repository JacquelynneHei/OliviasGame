﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shape : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ShapesGameManager shapesGameManager;
    public Transform dragParent;
    public Transform dropParent;
    public float sizeDecreaseSpeed;     //the speed in which to decrease the scale on a correct answer
    public Transform dropLocation;      //the drop location that is correct
    private Vector3 startLocation;     //the location of the shape when it spawns into the level

    public List<Sprite> sprites;

    public AudioSource mySource;
    public AudioClip correct;
    public AudioClip wrong;

    bool isCorrect;     //boolean to tell us if we got the correct answer

    private void Start()
    {
        mySource = GetComponent<AudioSource>();
        shapesGameManager = GameObject.FindObjectOfType<ShapesGameManager>();
        startLocation = transform.position;
        dragParent = GameObject.Find("Canvas").transform;
        dropParent = GameObject.Find("Container").transform;

    }

    private void Update()
    {
        if (isCorrect)
        {
            mySource.Play();
            Vector3 myScale = transform.localScale;
            myScale -= new Vector3(sizeDecreaseSpeed, sizeDecreaseSpeed, 0f) * Time.deltaTime;
            transform.localScale = myScale;
        }

        if (transform.localScale.x < 0 && transform.localScale.y < 0)
        {
            shapesGameManager.shapesInLevel.Remove(this.gameObject);
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
            transform.SetAsFirstSibling();
        }
    }
}
