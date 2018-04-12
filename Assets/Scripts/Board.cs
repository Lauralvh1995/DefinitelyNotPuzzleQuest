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

    bool cascading = false;
    public void Awake()
    {
        random = new System.Random();
        matched = new HashSet<Cell>();
        StartCoroutine(InitializeBoard());
    }
    public IEnumerator InitializeBoard()
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

                cells[x, y] = newTile;
                cells[x, y].x = x;
                cells[x, y].y = y;

                yOffset += cellPrefab.size;
            }
            xOffset += cellPrefab.size;
            yield return null;
        }
        yield return StartCoroutine("ResetBoard");
        manager.Initialize();
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
            yield return null;
        }
        yield return StartCoroutine(FixBoard());
    }

    public IEnumerator FixBoard()
    {
        while (cascading)
        {
            cascading = false;
            yield return StartCoroutine(Clear());
            yield return StartCoroutine(Repopulate());
        }
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
        foreach (Cell d in cells)
        {
            Match(d);
            yield return null;
        }
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
            yield return StartCoroutine(FixBoard());
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
        bool check1 = Match(cell1);
        bool check2 = Match(cell2);
        if(check1 || check2)
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
        GemType gem = c.GetContent().GetComponent<Gem>().type;
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
                if (i.y < 7 && cells[i.x, i.y + 1].GetContent().GetComponent<Gem>().type == gem)
                {
                    if (match.Add(cells[i.x, i.y + 1]))
                    {
                        newlyAdded = true;
                    }
                }
                if (i.y > 0 && cells[i.x, i.y - 1].GetContent().GetComponent<Gem>().type == gem)
                {
                    if (match.Add(cells[i.x, i.y - 1]))
                    {
                        newlyAdded = true;
                    }
                }
                if (i.x < 7 && cells[i.x + 1, i.y].GetContent().GetComponent<Gem>().type == gem)
                {
                    if (match.Add(cells[i.x + 1, i.y]))
                    {
                        newlyAdded = true;
                    }
                }
                if (i.x > 0 && cells[i.x - 1, i.y].GetContent().GetComponent<Gem>().type == gem)
                {
                    if (match.Add(cells[i.x - 1, i.y]))
                    {
                        newlyAdded = true;
                    }
                }
            }
        }
        if(match.Count > 2)
        {
            correct = true;
            cascading = true;

            if (match.Count > 0)
            {
                foreach (Cell d in match)
                {
                    matched.Add(d);
                }
            }
        }
        manager.ResetPass();
        return correct;
    }
    public IEnumerator Clear()
    {
        foreach(Cell c in matched)
        {
            Gem g = c.GetContent().GetComponent<Gem>();
            if (manager.GetActivePlayer() != null)
            {
                switch (g.type)
                {
                    case GemType.Red:
                        {
                            manager.GetActivePlayer().AddScore(g.score);
                            manager.GetActivePlayer().AddRed(g.red);
                            break;
                        }
                    case GemType.Blue:
                        {
                            manager.GetActivePlayer().AddScore(g.score);
                            manager.GetActivePlayer().AddBlue(g.blue);
                            break;
                        }
                    case GemType.Green:
                        {
                            manager.GetActivePlayer().AddScore(g.score);
                            manager.GetActivePlayer().AddGreen(g.green);
                            break;
                        }
                    case GemType.Yellow:
                        {
                            manager.GetActivePlayer().AddScore(g.score);
                            manager.GetActivePlayer().AddYellow(g.yellow);
                            break;
                        }
                    case GemType.Purple:
                        {
                            manager.GetActivePlayer().AddScore(g.score);
                            manager.GetActivePlayer().MatchAttack(g.damage);
                            break;
                        }
                    case GemType.Orange:
                        {
                            manager.GetActivePlayer().AddScore(g.score);
                            manager.GetActivePlayer().AddHP(g.damage);
                            break;
                        }
                    case GemType.White:
                        {
                            manager.GetActivePlayer().AddScore(g.score);
                            break;
                        }
                }
            }
            EmptyCell(c);
            yield return null;
        }
        matched.Clear();
    }
}
