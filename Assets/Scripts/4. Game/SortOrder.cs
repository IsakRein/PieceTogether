using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Positions
{
    public List<Vector2> squaresPos = new List<Vector2>();
}

[System.Serializable]
public class PositionsList
{
    public List<Positions> shapesPos = new List<Positions>();
}

public class SortOrder : MonoBehaviour {

    public List<int> sortOrder = new List<int>();
    public List<ShapeScript> shapes = new List<ShapeScript>();
    public GenerateShapes generateShapes;

    public PositionsList positions = new PositionsList();
    public List<Vector2> positionsInOne = new List<Vector2>();

    public List<Vector2> finishedLevel = new List<Vector2>();
    
    public PopUp popUp;

    public void CustomStart()
    {
        for (int i = 0; i <= generateShapes.shapeCount; i++)
        {
            positions.shapesPos.Add(new Positions());
        }

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

    public void UpdatePositions()
    {
        positionsInOne.Clear();

        for (int i = 0; i < positions.shapesPos.Count; i++)
        {
            for (int j = 0; j < positions.shapesPos[i].squaresPos.Count; j++)
            {
                positionsInOne.Add(positions.shapesPos[i].squaresPos[j]);
            }
        }

        CheckIfWon();
    }

    private void CheckIfWon()
    {
        bool notInBoth = false;

        int amountOfSim = 0;

        for (int i = 0; i < positionsInOne.Count; i++)
        {
            if (finishedLevel.Contains(positionsInOne[i])) {
                amountOfSim += 1;
            }
        }

        //level won
        if (amountOfSim == finishedLevel.Count) {
            //trigger animations etc

            popUp.InitPopUp("Level Won");
        }
    }
}
