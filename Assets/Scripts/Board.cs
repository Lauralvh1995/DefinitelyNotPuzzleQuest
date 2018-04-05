using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Board : MonoBehaviour {
    public int height;
    public int width;

    public TurnManager manager;

    public List<GameObject> possibleGems;

    [SerializeField]
    private Cell[,] cells;
    public Cell cellPrefab;

    System.Random random;

    [SerializeField] GameObject backup1;
    [SerializeField] GameObject backup2;

    [SerializeField] HashSet<Cell> matched;
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
                cells[x, y].x = x;
                cells[x, y].y = y;

                yOffset += cellPrefab.size;
            }
            xOffset += cellPrefab.size;
        }
        StartCoroutine("ResetBoard");
    }

    public IEnumerator ResetBoard()
    {
        foreach (Cell c in cells)
        {
            StartCoroutine(c.SetContent(possibleGems[random.Next(possibleGems.Count)]));
            yield return null;
            
        }
        foreach (Cell c in cells)
        {
            Match(c);
        }
        Clear();
    }

    private void Update()
    {
        
    }

    public IEnumerator Repopulate()
    {
        foreach (Cell c in cells)
        {
            if (c.GetContent() == null)
            {
                StartCoroutine(c.SetContent(possibleGems[random.Next(possibleGems.Count)]));
            }
            yield return null;
        }
        foreach (Cell c in cells)
        {
            Match(c);
        }
        Clear();
    }

    public void EmptyCell(Cell c)
    {
        StartCoroutine(c.SetContent(null));
    }

    public IEnumerator Move(Cell cell1, Cell cell2)
    {
        backup1 = cell1.GetContent();
        backup2 = cell2.GetContent();

        yield return StartCoroutine(cell2.SetContent(backup1));
        yield return StartCoroutine(cell1.SetContent(backup2));

        if(Match(cell1, cell2))
        {
            Clear();
            yield return StartCoroutine(Repopulate());
            manager.NextTurn();
        }
        else
        {
            yield return StartCoroutine(cell1.SetContent(backup1));
            yield return StartCoroutine(cell2.SetContent(backup2));
        }
    }
    public bool Match(Cell cell1, Cell cell2)
    {
        bool correct = false;
        if(Match(cell1)||Match(cell2))
        {
            correct = true;
        }
        return correct;
    }
    public bool Match(Cell c)
    {
        bool correct = false;
        bool newlyAdded = true;
        HashSet<Cell> match = new HashSet<Cell>();

        //add origin to set
        //check up, left, down and right from origin
        //if same gem type, add to set
        //repeat for new elements in set
        //if no new elements are added, check length
        //if set.size > 2, return true
        match.Add(c);
        while (newlyAdded)
        {
            foreach (Cell i in match.ToList())
            {
                newlyAdded = false;
                if (cells[c.x, c.y + 1].GetContent().GetComponent<Gem>().type == i.GetContent().GetComponent<Gem>().type)
                {
                    if (match.Add(cells[c.x, c.y + 1]))
                    {
                        newlyAdded = true;
                    }
                }
                if (cells[c.x, c.y - 1].GetContent().GetComponent<Gem>().type == i.GetContent().GetComponent<Gem>().type)
                {
                    if (match.Add(cells[c.x, c.y - 1]))
                    {
                        newlyAdded = true;
                    }
                }
                if (cells[c.x + 1, c.y].GetContent().GetComponent<Gem>().type == i.GetContent().GetComponent<Gem>().type)
                {
                    if (match.Add(cells[c.x + 1, c.y]))
                    {
                        newlyAdded = true;
                    }
                }
                if (cells[c.x - 1, c.y].GetContent().GetComponent<Gem>().type == i.GetContent().GetComponent<Gem>().type)
                {
                    if (match.Add(cells[c.x - 1, c.y]))
                    {
                        newlyAdded = true;
                    }
                }
            }
        }
        
        if(match.Count > 2)
        {
            correct = true;

            foreach(Cell d in match)
            {
                matched.Add(d);
            }
        }
        manager.ResetPass();
        return correct;
    }
    public void Clear()
    {
        foreach(Cell c in matched)
        {

        }
        Repopulate();
    }
}
