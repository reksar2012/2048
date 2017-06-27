using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Playing,GameOVer,WaitingToMoveToEnd
}
public class GameManager : MonoBehaviour {
    public GameState State;
    [Range(0f, 2f)]
    public float delay;
    private bool isMoveMade;
    private bool[] LineMoveComplete=new bool[4] { true, true , true, true };
    private Tile[,] AllTiles = new Tile[4, 4];
    private List<Tile[]> columns = new List<Tile[]>();
    private List<Tile[]> rows = new List<Tile[]>();
    private List<Tile> EmptyTiles = new List<Tile>();
    
    // Use this for initialization
    void Start () {
        Tile[] Tiles = GameObject.FindObjectsOfType<Tile>();
        foreach (Tile item in Tiles)
        {
            item.Number = 0;
            AllTiles[item.indRow, item.indCol] = item;
            EmptyTiles.Add(item);
        }
        columns.Add(new Tile[] { AllTiles[0, 0], AllTiles[1, 0], AllTiles[2, 0], AllTiles[3, 0] });
        columns.Add(new Tile[] { AllTiles[0, 1], AllTiles[1, 1], AllTiles[2, 1], AllTiles[3, 1] });
        columns.Add(new Tile[] { AllTiles[0, 2], AllTiles[1, 2], AllTiles[2, 2], AllTiles[3, 2] });
        columns.Add(new Tile[] { AllTiles[0, 3], AllTiles[1, 3], AllTiles[2, 3], AllTiles[3, 3] });

        rows.Add(new Tile[] {AllTiles[0,0], AllTiles[0,1], AllTiles[0,2], AllTiles[0,3] });
        rows.Add(new Tile[] {AllTiles[1,0], AllTiles[1,1], AllTiles[1,2], AllTiles[1,3] });
        rows.Add(new Tile[] {AllTiles[2,0], AllTiles[2,1], AllTiles[2,2], AllTiles[2,3] });
        rows.Add(new Tile[] {AllTiles[3,0], AllTiles[3,1], AllTiles[3,2], AllTiles[3,3] });

        Generate();
        Generate();
    }
    bool MakeOneMoveDownIndex(Tile[] LineTiles)
    {
        for (int i = 0; i < LineTiles.Length-1; i++)
        {
            if(LineTiles[i].Number==0&& LineTiles[i+1].Number != 0)
            {
                LineTiles[i].Number = LineTiles[i+1].Number;
                LineTiles[i + 1].Number=0;
                return true;
            }
            if (LineTiles[i].Number != 0&&LineTiles[i+1].Number== LineTiles[i].Number&&
                LineTiles[i].isMergedThisTurn==false&& LineTiles[i+1].isMergedThisTurn == false)
            {
                LineTiles[i].Number *= 2;
                LineTiles[i].isMergedThisTurn = true;
                LineTiles[i + 1].Number = 0;
                LineTiles[i + 1].animator.SetTrigger("Appear");
                LineTiles[i].animator.SetTrigger("Merge");
                ScoreTracker.Instance.Score += LineTiles[i].Number;
                return true;
            }

        }
        return false;
    }
    bool MakeOneMoveUpIndex(Tile[] LineTiles)
    {
        for (int i = LineTiles.Length - 1; i >0 ; i--)
        {
            if (LineTiles[i].Number == 0 && LineTiles[i - 1].Number != 0)
            {
                LineTiles[i].Number = LineTiles[i - 1].Number;
                LineTiles[i - 1].Number = 0;
                return true;
            }
            if (LineTiles[i].Number != 0 && LineTiles[i - 1].Number == LineTiles[i].Number &&
                LineTiles[i].isMergedThisTurn == false && LineTiles[i - 1].isMergedThisTurn == false)
            {
                LineTiles[i].Number *= 2;
                LineTiles[i].isMergedThisTurn = true;
                LineTiles[i-1].Number = 0;

                LineTiles[i - 1].animator.SetTrigger("Appear");
                LineTiles[i].animator.SetTrigger("Merge");

                ScoreTracker.Instance.Score += LineTiles[i].Number;
                return true;
            }

        }
        return false;
    }
    void GameOver()
    {
        ScrollObject[] ScrollObjects= FindObjectsOfType<ScrollObject>();
        foreach (ScrollObject item in ScrollObjects)
        {
            item.start = true;
        }
    }
    bool CanMove()
    {
        if(EmptyTiles.Count>0)
        { return true; }
        else
        {
            for (int i = 0; i <columns.Count; i++)
            {
                for (int j = 0; j < rows.Count-1; j++)
                {
                    if (AllTiles[j, i].Number == AllTiles[j + 1, i].Number)
                    {
                        return true;
                    }
                }
            }
            for (int i = 0; i < rows.Count; i++)
            {
                for (int j = 0; j < columns.Count - 1; j++)
                {
                    if (AllTiles[i, j].Number == AllTiles[i,j + 1].Number)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    private void ResetMergeFlag()
    {
        foreach (Tile item in AllTiles)
        {
            item.isMergedThisTurn = false;
        }
    }
    private void Generate()
    {
        if (EmptyTiles.Count != 0)
        {
            int RandomNumber = UnityEngine.Random.Range(0, EmptyTiles.Count);
            int persent= UnityEngine.Random.Range(0, 10);
            if (persent == 0)
            {
                EmptyTiles[RandomNumber].Number = 4;
            }
            else
            {
                EmptyTiles[RandomNumber].Number = 2;
            }
            EmptyTiles.RemoveAt(RandomNumber);
        }
    }
    private void UpdateEmptyTile() {
        EmptyTiles.Clear();
        foreach (Tile item in AllTiles)
        {
            if (item.Number==0)
            {
                EmptyTiles.Add(item);
            }
        }
     }
    public void Move(MoveDirection md)
    {
        Debug.Log(md.ToString()+" move.");
        bool isMoveMade = false;
        ResetMergeFlag();

        if (delay > 0)
        {
            StartCoroutine(MoveCorutine(md));
        }
        else
        {
            for (int i = 0; i < rows.Count; i++)
            {
                switch (md)
                {
                    case MoveDirection.Right:
                        while (MakeOneMoveUpIndex(rows[i]))
                            
                        {
                            isMoveMade = true;
                        };
                        break;
                    case MoveDirection.Down:
                        while (MakeOneMoveUpIndex(columns[i]))
                        {
                            isMoveMade = true;
                        };
                        break;
                    case MoveDirection.Left:
                        while (MakeOneMoveDownIndex(rows[i]))
                        {
                            isMoveMade = true;
                        };
                        break;
                    case MoveDirection.Up:
                        while (MakeOneMoveDownIndex(columns[i]))
                        {
                            isMoveMade = true;
                        };

                        break;
                }
            }
            if (isMoveMade == true)
            {
                UpdateEmptyTile();
                Generate();
                if (!CanMove())
                {
                    GameOver();
                }
                State = GameState.Playing;
            }
        }
    }


    public void NewGameButtonHandler()
    {
        SceneManager.LoadScene(0);
    }
   IEnumerator MoveCorutine(MoveDirection md)
    {
        State = GameState.WaitingToMoveToEnd;
        switch (md)
        {
            case MoveDirection.Left:
                for (int i = 0; i < columns.Count; i++)
                {
                    StartCoroutine(MoveOneLineMoveDownIndexCorutine(rows[i],i));
                } break;
            case MoveDirection.Right:
                for (int i = 0; i < columns.Count; i++)
                {
                    StartCoroutine(MoveOneLineMoveUpIndexCorutine(rows[i], i));
                }
                break;
            case MoveDirection.Up:
                for (int i = 0; i < rows.Count; i++)
                {
                    StartCoroutine(MoveOneLineMoveDownIndexCorutine(columns[i], i));
                }
                break;
            case MoveDirection.Down:
                for (int i = 0; i < rows.Count; i++)
                {
                    StartCoroutine(MoveOneLineMoveUpIndexCorutine(columns[i], i));
                }
                break;
        }
        while(!(LineMoveComplete[0] && LineMoveComplete[1] && LineMoveComplete[2] && LineMoveComplete[3]))
        {
            yield return null;
        }
        if (isMoveMade == true)
        {
            UpdateEmptyTile();
            Generate();
            if (!CanMove())
            {
                GameOver();
            }
            State = GameState.Playing;
        }
    }
    IEnumerator MoveOneLineMoveUpIndexCorutine(Tile[] line,int index)
    {
        LineMoveComplete[index] = false;
        while (MakeOneMoveUpIndex(line))
        {
            isMoveMade = true;
            yield return new WaitForSeconds(delay);
        }
        LineMoveComplete[index] = true;
    }
    IEnumerator MoveOneLineMoveDownIndexCorutine(Tile[] line, int index)
    {
        LineMoveComplete[index] = false;
        while (MakeOneMoveDownIndex(line))
        {
            isMoveMade = true;
            yield return new WaitForSeconds(delay);
        }
        LineMoveComplete[index] = true;
    }
}
