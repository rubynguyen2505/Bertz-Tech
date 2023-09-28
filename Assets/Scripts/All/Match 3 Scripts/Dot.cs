using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    
    [Header("Board Variables")]
    public int column;
    public int row;
    public int previousColumn;
    public int previousRow;
    public int targetX;
    public int targetY;
    public bool isMatched = false;
    

    
    //private Animator animator;
    //private float shineDelay;
    //private float shineDelaySecond;
    //private EndGameManager endgameManager;
    //private HintManager hintManager;
    

    private FindMatches findMatches;
    private Board board;
    public GameObject otherDot;
    private Vector2 firstTouchPosition = Vector2.zero;
    private Vector2 finalTouchPosition = Vector2.zero;
    private Vector2 tempPosition;

    [Header("Swipe Stuff")]
    public float swipeAngle = 0;
    public float swipeResist = 1f;

    
    [Header("Powerup Stuff")]
    public bool isColumnBomb;
    public bool isRowBomb;
    public bool isColorBomb;
    public bool isAdjacentBomb;
    public GameObject rowArrow;
    public GameObject columnArrow;
    public GameObject colorBomb;
    public GameObject adjacentMarker;
    

    // Start is called before the first frame update
    void Start()
    {
        
        isColumnBomb = false;
        isRowBomb = false;
        isColorBomb = false;
        isAdjacentBomb = false;
        /*
        shineDelay = Random.Range(3f, 6f);
        shineDelaySecond = shineDelay;
        animator = GetComponent<Animator>();
        endgameManager = FindObjectOfType<EndGameManager>();
        hintManager = FindObjectOfType<HintManager>();
        */

        //board = GameObject.FindWithTag("Board").GetComponent<Board>(); ;
        board = FindObjectOfType<Board>();
        findMatches = FindObjectOfType<FindMatches>();
        //targetX = (int)transform.position.x;
        //targetY = (int)transform.position.y;
        //row = targetY;
        //column = targetX;
        //previousRow = row;
        //previousColumn = column;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)) {
            isColorBomb = true;
            GameObject color = Instantiate(colorBomb, transform.position, Quaternion.identity);
            color.transform.parent = this.transform;
        }
    }

    /*
    //This is for testing and debug only
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isAdjacentBomb = true;
            GameObject marker = Instantiate(adjacentMarker, transform.position, Quaternion.identity);
            marker.transform.parent = this.transform;
        }
    }
    */

    
    // Update is called once per frame
    void Update()
    {
        /*
        shineDelaySecond -= Time.deltaTime;
        if (shineDelaySecond <= 0)
        {
            shineDelaySecond = shineDelay;
            StartCoroutine(StartShineCo());
        }
        
        */
        
        /*
        if (isMatched)
        {
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.color = new Color(1f, 1f, 1f, .2f);
        }
        */

        targetX = column;
        targetY = row;
        
        if (Mathf.Abs(targetX - transform.position.x) > .1)
        {
            //move Towards the target
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            
            if (board.allDots[column, row] != this.gameObject)
            {
                board.allDots[column, row] = this.gameObject;
                findMatches.FindAllMatches();
            }
            
            //findMatches.FindAllMatches(); //Adjust later
        }
        else
        {
            //Directly set the position
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
            //board.allDots[column, row] = this.gameObject;
        }

        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            //move Towards the target
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            
            if (board.allDots[column, row] != this.gameObject)
            {
                board.allDots[column, row] = this.gameObject;
                findMatches.FindAllMatches();
            }
            
            //findMatches.FindAllMatches(); //Adjust later
        }
        else
        {
            //Directly set the position
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
            //board.allDots[column, row] = this.gameObject;
        }
        
    }
    
    
    /*
    IEnumerator StartShineCo()
    {
        animator.SetBool("Shine", true);
        yield return null;
        animator.SetBool("Shine", false);
    }
    

    
    public void PopAnimation()
    {
        animator.SetBool("Popped", true);
    }
    */

    
    public IEnumerator CheckMoveCo()
    {
        
        if (isColorBomb)
        {
            //This piece is a color bomb, and the other piece is the color to destroy
            findMatches.MatchPiecesOfColor(otherDot.tag);
            isMatched = true;
        }
        else if (otherDot.GetComponent<Dot>().isColorBomb)
        {
            //The other piece is a color bomb, and this piece jas the color to destroy
            findMatches.MatchPiecesOfColor(this.gameObject.tag);
            otherDot.GetComponent<Dot>().isMatched = true;
        }
        
        
        yield return new WaitForSeconds(.5f);
        if (otherDot != null)
        {
            if (!isMatched && !otherDot.GetComponent<Dot>().isMatched)
            {
                otherDot.GetComponent<Dot>().row = row;
                otherDot.GetComponent<Dot>().column = column;
                row = previousRow;
                column = previousColumn;
                yield return new WaitForSeconds(.5f);
                board.currentDot = null;
                board.currentState = gameState.move;
            }
            
            else
            {
                /*
                if (endgameManager != null)
                {
                    if (endgameManager.requirements.gameType == GameType.Moves)
                    {
                        endgameManager.DecreaseCounterValue();
                    }
                }*/
                
                board.DestroyMatches();
            }
            
            //otherDot = null;
        }
        
    }
    

    private void OnMouseDown()
    {
        /*
        if (animator != null)
        {
            animator.SetBool("Touched", true);
        }
        //Destroy the hint
        if (hintManager != null)
        {
            hintManager.DestroyHint();
        }
        */


        
        if (board.currentState == gameState.move)
        {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
    }
    

    
    private void OnMouseUp()
    {
        //animator.SetBool("Touched", false);
        if (board.currentState == gameState.move)
        {
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
        }
    }
    

    void CalculateAngle()
    {
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            board.currentState = gameState.wait;
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MovePieces();
            board.currentDot = this;
        }
        else
        {
            board.currentState = gameState.move;
        }
    }

    /*
    void MovePiecesActual(Vector2 direction)
    {
        otherDot = board.allDots[column + (int)direction.x, row + (int)direction.y];
        previousRow = row;
        previousColumn = column;
        //if (board.lockTiles[column, row] == null && board.lockTiles[column + (int)direction.x, row + (int)direction.y] == null)
        //{
            if (otherDot != null)
            {
                otherDot.GetComponent<Dot>().column += -1 * (int)direction.x;
                otherDot.GetComponent<Dot>().row += -1 * (int)direction.y;
                column += (int)direction.x;
                row += (int)direction.y;
                StartCoroutine(CheckMoveCo());
            }
            else
            {
                //board.currentState = gameState.move;
            }
        //}
        //else
        //{
            //board.currentState = gameState.move;
        //}


    }
    */

    
    void MovePieces()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width - 1) 
        {
            //Right Swipe
            
            otherDot = board.allDots[column + 1, row];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().column -= 1;
            column += 1;
            StartCoroutine(CheckMoveCo());
            
            //MovePiecesActual(Vector2.right);
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1)
        {
            //Up Swipe
            
            otherDot = board.allDots[column, row + 1];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().row -= 1;
            row += 1;
            StartCoroutine(CheckMoveCo());
            
            //MovePiecesActual(Vector2.up);
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135 ) && column > 0)
        {
            //Left Swipe
            
            otherDot = board.allDots[column - 1, row];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().column += 1;
            column -= 1;
            StartCoroutine(CheckMoveCo());
            
            //MovePiecesActual(Vector2.left);
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            //Down Swipe
            
            otherDot = board.allDots[column, row - 1];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().row += 1;
            row -= 1;
            StartCoroutine(CheckMoveCo());
            
            //MovePiecesActual(Vector2.down);
        }
        else
        {
            //board.currentState = gameState.move;
        }
        //StartCoroutine(CheckMoveCo());
    }
    

    
    void FindMatches()
    {
        if (column > 0 && column < board.width - 1)
        {
            GameObject leftDot1 = board.allDots[column - 1, row];
            GameObject rightDot1 = board.allDots[column + 1, row];
            if (leftDot1 != null && rightDot1 != null)
            {
                if (leftDot1.tag == this.gameObject.tag && rightDot1.tag == this.gameObject.tag)
                {
                    leftDot1.GetComponent<Dot>().isMatched = true;
                    rightDot1.GetComponent<Dot>().isMatched = true;
                    isMatched = true;
                }
            }
            
        }

        if (row > 0 && row < board.height - 1)
        {
            GameObject upDot1 = board.allDots[column, row + 1];
            GameObject downDot1 = board.allDots[column, row - 1];
            if (upDot1 != null && downDot1 != null)
            {
                if (upDot1.tag == this.gameObject.tag && downDot1.tag == this.gameObject.tag)
                {
                    upDot1.GetComponent<Dot>().isMatched = true;
                    downDot1.GetComponent<Dot>().isMatched = true;
                    isMatched = true;
                }
            }
        }
    }

    
    public void MakeRowBomb()
    {
        if (!isColumnBomb && !isColorBomb && !isAdjacentBomb)
        {
            Console.WriteLine("Row");
            isRowBomb = true;
            GameObject arrow = Instantiate(rowArrow, transform.position, Quaternion.identity);
            arrow.transform.parent = this.transform;
        }
        
    }
    

    
    public void MakeColumnBomb()
    {
        
        if (!isRowBomb && !isColorBomb && !isAdjacentBomb)
        {
            Console.WriteLine("Column");
            isColumnBomb = true;
            GameObject arrow = Instantiate(columnArrow, transform.position, Quaternion.identity);
            arrow.transform.parent = this.transform;
        }
        
    }
    

    
    public void MakeColorBomb()
    {
        if (!isRowBomb && !isColumnBomb && !isAdjacentBomb)
        {
            isColorBomb = true;
            GameObject color = Instantiate(colorBomb, transform.position, Quaternion.identity);
            color.transform.parent = this.transform;
            this.gameObject.tag = "Color";
        }
        
    }
    

    
    public void MakeAdjacentBomb()
    {
        if (!isColorBomb && !isColumnBomb && !isRowBomb)
        {
            isAdjacentBomb = true;
            GameObject marker = Instantiate(adjacentMarker, transform.position, Quaternion.identity);
            marker.transform.parent = this.transform;
        }
        
    }
    
}
