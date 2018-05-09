using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortOrder : MonoBehaviour {

    public List<int> sortOrder = new List<int>();
    public List<ShapeScript> shapes = new List<ShapeScript>();
    public GenerateShapes generateShapes;

    public static List<int> objects = new List<int>();

    public void CustomStart()
    {
        transform.localScale = new Vector2(generateShapes.scaleValue, generateShapes.scaleValue);
    }

    public void UpdateSort(int number)
    {
        sortOrder.Remove(number);
        sortOrder.Insert(0, number);

        foreach (ShapeScript shape in shapes)
        {
            int indexNumber = sortOrder.Count - sortOrder.IndexOf(shape.number);

            shape.SetSort(indexNumber);
        }
    }
}
