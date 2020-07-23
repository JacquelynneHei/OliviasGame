using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Shapes")]
    public List<GameObject> shapesPrefabs;
    public List<GameObject> shapesInLevel;
    public RectTransform shapesSpawnParent;
    public Transform circleDropLocation;
    public Transform squareDropLocation;
    public Transform triangleDropLocation;

    private void Start()
    {
        //Test SpawnShapes logic
        SpawnShapes();
        SpawnShapes();
        SpawnShapes();
    }

    void StartShapes()
    {

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
}
