using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapesGameManager : MonoBehaviour
{
    [Header("Set up")]
    public int numberShapesToSpawn;             //likely be set to 15
    public List<GameObject> shapesPrefabs;
    public RectTransform shapesSpawnParent;
    public Transform circleDropLocation;
    public Transform squareDropLocation;
    public Transform triangleDropLocation;
    public GameObject rocket;
    public Image burn;

    [Header("Win Condition")]
    public List<GameObject> shapesInLevel;
    public GameObject winPanel;
    public bool isRocketOffScreen;



    private void Start()
    {
        SetupGame();
    }

    public void SetupGame()
    {
        for (int i = 0; i < numberShapesToSpawn; i++)
        {
            SpawnShapes();
        }

        
        rocket.GetComponent<RectTransform>().position = Vector3.zero;
        isRocketOffScreen = false;
        burn.enabled = false;
        winPanel.SetActive(false);
    }

    private void Update()
    {
        if(shapesInLevel.Count == 0)
        {
            //The player has won the game!
            WinState();
        }
    }

    void SpawnShapes()
    {
        int randomShape = Random.Range(0, shapesPrefabs.Count);

        //instantiate the shapes into the world
        GameObject shape = Instantiate(shapesPrefabs[randomShape]);

        //parent the shapes to the container
        shape.transform.parent = shapesSpawnParent;

        //rescale the shape to its orginal scale
        shape.transform.localScale = new Vector3(1f, 1f, 1f);

        shapesInLevel.Add(shape);

        Shape shapeScript = shape.GetComponent<Shape>();

        shape.GetComponent<Image>().sprite = shapeScript.sprites[Random.Range(0, shapeScript.sprites.Count)];

        switch (randomShape)
        {
            case 0:
                shapeScript.dropLocation = triangleDropLocation;
                break;
            case 1:
                shapeScript.dropLocation = squareDropLocation;
                break;
            case 2:
                shapeScript.dropLocation = circleDropLocation;
                break;
        }
    }

    void WinState()
    {
        if (isRocketOffScreen)
        {
            winPanel.SetActive(true); 
        }
        else
        {
            Animator rocketAnim = rocket.GetComponent<Animator>();
            burn.enabled = true;
            rocketAnim.SetBool("hasWon", true);
        }
    }

    public void RocketOffScreen()
    {
        isRocketOffScreen = true;
        rocket.GetComponent<Animator>().SetBool("hasWon", false);
    }
}
