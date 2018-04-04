using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Left,
    Down,
    Right
}
public class Board : MonoBehaviour {
    public int height;
    public int width;

    public List<GameObject> possibleGems;

    [SerializeField]
    private Cell[,] cells;
    public Cell cellPrefab;

    [SerializeField]
    private Direction currentDirection;

    System.Random random;
    public void Awake()
    {
        random = new System.Random();
        InitializeBoard();
    }
    public void InitializeBoard()
    {
        cells = new Cell[height, width];
        float xOffset = 0;
        for (int x = 0; x < width; x++)
        {
            float yOffset = 0;
            for (int y = 0; y < height; y++)
            {
                Cell newTile = Instantiate(cellPrefab);
                newTile.transform.SetParent(transform);
                newTile.transform.localPosition = new Vector3(xOffset, 0, yOffset); // Note that our XY-plane maps to Unity's XZ-plane since Y is the vertical axis.
                newTile.name = string.Format("Cell {0}x{1}", x, y);
                //newTile.name = "Cell " + x + "x" + y;

                cells[x, y] = newTile;

                yOffset += cellPrefab.size;
            }
            xOffset += cellPrefab.size;
        }
        foreach (Cell c in cells)
        {
            c.SetContent(possibleGems[random.Next(possibleGems.Count)]);
        }
    }

    private void Update()
    {
        
    }

    public void ChangeDirection(Direction direction)
    {
        currentDirection = direction;
    }

    public void Fall()
    {

    }

    public void EmptyCell(Cell c)
    {
        c.SetContent(null);
    }

    public void Move()
    {

    }
    public void Match()
    {

    }
}
