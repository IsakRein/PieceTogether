using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateShapes : MonoBehaviour {

    public int shapeCount;
    public int minSize;
    public int maxSize;

    public int width;
    public int height;
    private int amount;

    private int sum;
    private int sumLeft;

    public List<int> sizes = new List<int>();

    public List<SpriteRenderer> squares = new List<SpriteRenderer>();

    public List<int> grid = new List<int>();

    public List<Color32> colors = new List<Color32>();

    private void Start()
    {
        foreach (Transform child in transform)
        {
            squares.Add(child.GetComponent<SpriteRenderer>());
        }
    }

    public void Generate()
    {
        GenerateSizes();

        for (int i = 0; i < amount; i++)
        {
            grid.Add(0);
        }

        for (int i = 0; i < sizes.Count; i++)
        {
            int firstFreeSquare = 0;
            int num = 0;

            while (firstFreeSquare == 0)
            {
                if (grid[num] == 0)
                {
                    firstFreeSquare = num;
                }

                num = num + 1;
            }

            grid[firstFreeSquare] = i; 
        }


        ApplyColor();
    }


    void GenerateSizes()
    {
        sizes.Clear();

        amount = width * height;
        sumLeft = amount;

        for (int i = 0; i < shapeCount; i++)
        {
            if ((minSize * (shapeCount - sizes.Count) <= sumLeft) && (maxSize * (shapeCount - sizes.Count) >= sumLeft))
            {
                if (i == shapeCount - 1)
                {
                    sizes.Add(sumLeft);
                }
                else
                {
                    sizes.Add(Random.Range(minSize, maxSize + 1));
                }
                CalculateSum();
            }

            else
            {
                sizes.RemoveAt(i - 1);
                CalculateSum();
                i = i - 2;
            }
        }

        CalculateSum();
    }

    void CalculateSum()
    {
        sum = 0;
        for (int j = 0; j < sizes.Count; j++)
        {
            sum = sum + sizes[j];
        }
        sumLeft = amount - sum;
    }

    void ApplyColor()
    {
        for (int i = 0; i < amount; i++)
        {
            squares[i].color = colors[grid[i]];
        }
    }
}
