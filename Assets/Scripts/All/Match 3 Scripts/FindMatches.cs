using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FindMatches : MonoBehaviour
{
    private Board board;
    public List<GameObject> currentMatches = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
    }

    public void FindAllMatches()
    {
        StartCoroutine(FindAllMatchesCo());
    }
  
    private List<GameObject> isAdjacentBomb(Dot dot1, Dot dot2, Dot dot3)
    {
        List <GameObject> currentDots = new List<GameObject>();
        if (dot1.isAdjacentBomb)
        {
            currentMatches.Union(GetAdjacentPieces(dot1.column, dot1.row));
        }

        if (dot2.isAdjacentBomb)
        {
            currentMatches.Union(GetAdjacentPieces(dot2.column, dot2.row));
        }

        if (dot3.isAdjacentBomb)
        {
            currentMatches.Union(GetAdjacentPieces(dot3.column, dot3.row));
        }
        return currentDots;
    }
      
    private List<GameObject> isRowBomb(Dot dot1, Dot dot2, Dot dot3)
    {
        List <GameObject> currentDots = new List<GameObject>();
        if (dot1.isRowBomb)
        {
            currentMatches.Union(GetRowPieces(dot1.row));
            board.BombRow(dot1.row);
        }

        if (dot2.isRowBomb)
        {
            currentMatches.Union(GetRowPieces(dot2.row));
            board.BombRow(dot2.row);
        }

        if (dot3.isRowBomb)
        {
            currentMatches.Union(GetRowPieces(dot3.row));
            board.BombRow(dot2.row);
        }
        return currentDots;
    }

    private List<GameObject> isColumnBomb(Dot dot1, Dot dot2, Dot dot3)
    {
        List <GameObject> currentDots = new List<GameObject>();
        if (dot1.isColumnBomb)
        {
            currentMatches.Union(GetColumnPieces(dot1.column));
            board.BombColumn(dot1.column);
        }

        if (dot2.isColumnBomb)
        {
            currentMatches.Union(GetColumnPieces(dot2.column));
            board.BombColumn(dot2.column);
        }

        if (dot3.isColumnBomb)
        {
            currentMatches.Union(GetColumnPieces(dot3.column));
            board.BombColumn(dot3.column);
        }
        return currentDots;
    }
    
    private void AddToListAndMatch(GameObject dot)
    {
        if (!currentMatches.Contains(dot))
        {
            currentMatches.Add(dot);
        }
        dot.GetComponent<Dot>().isMatched = true;
    }
    private void GetNearbyPieces(GameObject dot1, GameObject dot2, GameObject dot3)
    {
        AddToListAndMatch(dot1);
        
        AddToListAndMatch(dot2);

        AddToListAndMatch(dot3);
    }


    private IEnumerator FindAllMatchesCo()
    {
        yield return new WaitForSeconds(.2f);
        //yield return null;

        for (int i = 0; i < board.width; i ++)
        {
            for (int j = 0; j < board.height; j ++)
            {
                GameObject currentDot = board.allDots[i, j];
                
                if (currentDot != null)
                {
                    Dot currentDotDot = currentDot.GetComponent<Dot>(); 
                    if (i > 0 && i < board.width - 1)
                    {
                        GameObject leftDot = board.allDots[i - 1, j];
                        GameObject rightDot = board.allDots[i + 1, j];

                        if (leftDot != null && rightDot != null)
                        {
                            Dot rightDotDot = rightDot.GetComponent<Dot>();
                            Dot leftDotDot = leftDot.GetComponent<Dot>();
                            if (leftDot.tag == currentDot.tag && rightDot.tag == currentDot.tag)
                            {
                                /*
                                if (currentDot.GetComponent<Dot>().isRowBomb
                                    || leftDot.GetComponent<Dot>().isRowBomb
                                    || rightDot.GetComponent<Dot>().isRowBomb)
                                {
                                    currentMatches.Union(GetRowPieces(j));
                                }
                                if(currentDot.GetComponent<Dot>().isColumnBomb)
                                {
                                    currentMatches.Union(GetColumnPieces(i));
                                }

                                if (leftDot.GetComponent<Dot>().isColumnBomb)
                                {
                                    currentMatches.Union (GetColumnPieces(i-1));
                                }

                                if (rightDot.GetComponent<Dot>().isColumnBomb)
                                {
                                    currentMatches.Union(GetColumnPieces(i + 1));
                                }


                                if (!currentMatches.Contains(leftDot)) {
                                    currentMatches.Add(leftDot);   
                                }
                                leftDot.GetComponent<Dot>().isMatched = true;

                                if (!currentMatches.Contains(rightDot))
                                {
                                    currentMatches.Add(rightDot);
                                }
                                rightDot.GetComponent<Dot>().isMatched = true;

                                if (!currentMatches.Contains(currentDot))
                                {
                                    currentMatches.Add(currentDot);
                                }
                                currentDot.GetComponent <Dot>().isMatched = true;
                                */

                                currentMatches.Union(isRowBomb(leftDotDot, currentDotDot, rightDotDot));

                                currentMatches.Union(isColumnBomb(leftDotDot, currentDotDot, rightDotDot));

                                currentMatches.Union(isAdjacentBomb(leftDotDot, currentDotDot, rightDotDot));
                                

                                GetNearbyPieces(leftDot, currentDot, rightDot);
                                
                            }
                        }                    
                    }

                    if (j > 0 && j < board.height - 1)
                    {
                        GameObject upDot = board.allDots[i, j + 1];
                        GameObject downDot = board.allDots[i, j - 1];

                        if (upDot != null && downDot != null)
                        {
                            Dot downDotDot = downDot.GetComponent<Dot>();
                            Dot upDotDot = upDot.GetComponent<Dot>();
                            if (upDot.tag == currentDot.tag && downDot.tag == currentDot.tag)
                            {
                                /*
                                if (currentDot.GetComponent<Dot>().isColumnBomb
                                    || upDot.GetComponent<Dot>().isColumnBomb
                                    || downDot.GetComponent<Dot>().isColumnBomb)
                                {
                                    currentMatches.Union(GetColumnPieces(i));
                                }

                                if (currentDot.GetComponent<Dot>().isRowBomb)
                                {
                                    currentMatches.Union(GetRowPieces(j));
                                }

                                if (upDot.GetComponent<Dot>().isRowBomb)
                                {
                                    currentMatches.Union(GetRowPieces(j + 1));
                                }

                                if (downDot.GetComponent<Dot>().isRowBomb)
                                {
                                    currentMatches.Union(GetRowPieces(j - 1));
                                }


                                if (!currentMatches.Contains(upDot))
                                {
                                    currentMatches.Add(upDot);
                                }
                                upDot.GetComponent<Dot>().isMatched = true;

                                if (!currentMatches.Contains(downDot))
                                {
                                    currentMatches.Add(downDot);
                                }
                                downDot.GetComponent<Dot>().isMatched = true;

                                if (!currentMatches.Contains(currentDot))
                                {
                                    currentMatches.Add(currentDot);
                                }
                                currentDot.GetComponent<Dot>().isMatched = true;
                                */
                                
                                currentMatches.Union(isColumnBomb(upDotDot, currentDotDot, downDotDot));

                                currentMatches.Union(isRowBomb(upDotDot, currentDotDot, downDotDot));

                                currentMatches.Union(isAdjacentBomb(upDotDot, currentDotDot, downDotDot));
                                

                                GetNearbyPieces(upDot, currentDot, downDot);
                                
                            }
                        }                      
                    }
                }
            }
        }
    }
    
    public void MatchPiecesOfColor(string color)
    {
        for (int i = 0; i < board.width; i ++)
        {
            for (int j = 0; j < board.height; j ++)
            {
                //Check if that piece exists
                if (board.allDots[i, j] != null)
                {
                    //Check the tag on that dog
                    if (board.allDots[i, j].tag == color)
                    {
                        //Set that dot to be matched
                        board.allDots[i, j].GetComponent<Dot>().isMatched = true;
                    }
                }
            }
        }
    }

    List<GameObject> GetAdjacentPieces(int column, int row)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = column - 1; i <= column + 1; i ++)
        {
            for (int j = row - 1; j <= row + 1; j ++)
            {
                //Check if the piece is inside the board
                if (i >= 0 && i < board.width && j >= 0 && j < board.height)
                {
                    if (board.allDots[i, j] != null)
                    {
                        dots.Add(board.allDots[i, j]);
                        board.allDots[i, j].GetComponent<Dot>().isMatched = true;
                    }
                    
                }
            }
        }
        return dots;
    }
    

    List<GameObject> GetColumnPieces(int column)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < board.height; i ++)
        {
            if (board.allDots[column, i] != null)
            {
                Dot dot = board.allDots[column, i].GetComponent<Dot>();
                
                if (dot.isRowBomb)
                {
                    dots.Union(GetRowPieces(i)).ToList();
                }
                
                dots.Add(board.allDots[column, i]);
                dot.isMatched = true;
            }
        }
        return dots;
    }
    
    List<GameObject> GetRowPieces(int row)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < board.width; i ++)
        {
            if (board.allDots[i, row] != null)
            {
                Dot dot = board.allDots[i, row].GetComponent<Dot>();
                
                if (dot.isColumnBomb)
                {
                    dots.Union(GetColumnPieces(i)).ToList();
                }
                
                dots.Add(board.allDots[i, row]);
                dot.isMatched = true;
            }
        }
        return dots;
    }
    
    public void CheckBombs(MatchType matchType)
    {
        //Did the player move something?
        if (board.currentDot != null)
        {
            //Is the piece they moved matched?
            if (board.currentDot.isMatched && board.currentDot.tag == matchType.color)
            {
                //make it unmatched
                board.currentDot.isMatched = false;
                //Decide what kind of bomb to make
                int typeOfBomb = Random.Range(0, 100);
                if (typeOfBomb < 50)
                {
                    //Make a row bomb
                    board.currentDot.MakeRowBomb();
                } else if (typeOfBomb >= 50)
                {
                    //Make a column bomb
                    board.currentDot.MakeColumnBomb();
                }
                
                /*if ((board.currentDot.swipeAngle > -45 && board.currentDot.swipeAngle <= 45) 
                || (board.currentDot.swipeAngle < -135 || board.currentDot.swipeAngle >= 135))
                {
                    board.currentDot.MakeRowBomb();
                }
                else
                {
                    board.currentDot.MakeColumnBomb();
                }*/
            }
            //Is the other piece matched?
            else if (board.currentDot.otherDot != null)
            {
                Dot otherDot = board.currentDot.otherDot.GetComponent<Dot>();
                //Is the other dot matched?
                if (otherDot.isMatched && otherDot.tag == matchType.color)
                {
                    //Make it unmatched
                    otherDot.isMatched = false;

                    int typeOfBomb = Random.Range(0, 100);
                    if (typeOfBomb < 50)
                    {
                        //Make a row bomb
                        otherDot.MakeRowBomb();
                    }
                    else if (typeOfBomb >= 50)
                    {
                        //Make a column bomb
                        otherDot.MakeColumnBomb();
                    }
                    /*
                    //Decide what kind of bomb to make
                    if ((board.currentDot.swipeAngle > -45 && board.currentDot.swipeAngle <= 45) 
                    || (board.currentDot.swipeAngle < -135 || board.currentDot.swipeAngle >= 135))
                    {
                        otherDot.MakeRowBomb();
                    }
                    else
                    {
                        otherDot.MakeColumnBomb();
                    }
                    */
                }
            }
        }
    }
    
}
