﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeScript : MonoBehaviour {

    public List<Transform> squares = new List<Transform>();
    public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    public float HighestX;
    public float LowestX;
    public float HighestY;
    public float LowestY;

    public float AverageX;
    public float AverageY;

    private BoxCollider2D boxCollider2D;
    BoxCollider2D[] BoxColliders;

    Vector3 targetPos;

    private SortOrder sortOrder;

    public int number;

    public void CustomStart()
    {
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        sortOrder = transform.parent.GetComponent<SortOrder>();

        sortOrder.sortOrder.Add(number);
        sortOrder.shapes.Add(gameObject.GetComponent<ShapeScript>());

        foreach (Transform child in transform)
        {
            squares.Add(child);
            spriteRenderers.Add(child.GetComponent<SpriteRenderer>());
        }

        HighestX = squares[0].position.x;
        LowestX = squares[0].position.x;
        HighestY = squares[0].position.y;
        LowestY = squares[0].position.y;

        foreach (Transform square in squares)
        {
            if (square.position.x > HighestX)
            {
                HighestX = square.position.x;
            }
            else if (square.position.x < LowestX)
            {
                LowestX = square.position.x;
            }

            if (square.position.y > HighestY)
            {
                HighestY = square.position.y;
            }
            else if (square.position.y < LowestY)
            {
                LowestY = square.position.y;
            }
        }

        AverageX = (HighestX + LowestX) / 2;
        AverageY = (HighestY + LowestY) / 2;

        transform.position = new Vector2(AverageX, AverageY);

        foreach (Transform square in squares)
        {
            square.position = new Vector2(square.position.x-transform.position.x, square.position.y-transform.position.y);

            UnityEditorInternal.ComponentUtility.CopyComponent(square.GetComponent<BoxCollider2D>());
            Destroy(square.GetComponent<BoxCollider2D>());
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(gameObject);

        }

        BoxColliders = GetComponents<BoxCollider2D>();

        for (int i = 0; i < transform.childCount; i++)
        {
            BoxColliders[i].offset = transform.GetChild(i).transform.localPosition;
        }

        targetPos = transform.position;
    }

    public void SetSort(int sort)
    {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.sortingOrder = sort;
        }
    }

    public void DraggingItem()
    {
        sortOrder.UpdateSort(number);
    }

    public void DropItem()
    {
        if (Mathf.Abs(transform.position.x - targetPos.x) < 0.5f && Mathf.Abs(transform.position.y - targetPos.y) < 0.5f)
        {
            transform.position = targetPos;
        }
    }
}