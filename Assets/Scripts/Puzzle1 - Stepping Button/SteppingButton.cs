using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class SteppingButton : MonoBehaviour
{
    [Title("Status")]
    [Tooltip("If this is currently able to be solved or not")]
    public bool isActive = true;

    [Title("Stepping Order")]
    [Tooltip("What is an order of this button of the puzzle")]
    public int steppingOrder;

    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
        
    public void Stepping(GameObject player)
    {
        if (isActive)
        {
            CheckingAnswer(steppingOrder);
        }
    }

    public void CheckingAnswer(int buttonOrder)
    {
        if (!OnPuzzle.isEqual)
        {
            if (buttonOrder == OnPuzzle.puzzleAnswer[OnPuzzle.seq])
            {
                OnPuzzle.seq = OnPuzzle.seq +1;
                isActive = false;
                spriteRenderer.sprite = null; // get rid of the psrite of hide from the user

                // feedback of the success
                Debug.Log("Checking Puzzle Answer : matched an order of the puzzle");
            } else
            {
                // feedback of the failure
                spriteRenderer.color = new Color(255, 0, 0);
                Debug.Log("Checking Puzzle Answer : mismatched an order of the puzzle");
            }

            if (OnPuzzle.seq == OnPuzzle.puzzleAnswer.Length)
            {
                OnPuzzle.isEqual = true;

                // rewards of solving the puzzle
                Debug.Log("Congratulation. Puzzle is solved. Go forward.");
            }
        }
    }

    [Serializable]
    public class OnPuzzle : PuzzleSet {}
}
