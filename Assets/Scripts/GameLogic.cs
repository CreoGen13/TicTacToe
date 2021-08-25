using TMPro;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public Animator canvasAnimator;
    public Animator cameraAnimator;
    public Animator tableAnimator;

    public GameBehaviour gameBehaviour;
    public TextMeshProUGUI text;

    private int tableSize = 3; //Размер таблицы
    private int winLength = 3; //Длина комбинации для выйгрыша
    private bool[,] tableBool;

    private void Start()
    {
        tableBool = new bool[tableSize, tableSize];
    }
    public void StartAnimation()
    {
        canvasAnimator.SetBool("Start", true);
        cameraAnimator.SetBool("Start", true);
        tableAnimator.SetBool("Start", true);
    }
    public void BackAnimation()
    {
        gameBehaviour.DestroyDetails();
        canvasAnimator.SetBool("Start", false);
        cameraAnimator.SetBool("Start", false);
        tableAnimator.SetBool("Start", false);
    }
    public void GenerateTable()
    {
        text.text = "";

        for (int i = 0; i < tableSize; i++)
            for (int j = 0; j < tableSize; j++)
                tableBool[i, j] = Random.Range(0f, 1f) > 0.5f;

        gameBehaviour.SpawnDetails(tableBool);
    }
    public void CheckTable()
    {
        int minorSize = tableSize - winLength + 1;
        int secondMinor = tableSize - minorSize;
        for (int i = 0; i < tableSize; i++)
        {
            for(int j = 0; j < minorSize; j++)
            {
                bool horizontalWin = true;
                bool verticalWin = true;
                bool firstDiagonalWin = i < minorSize ? true : false;
                bool secondDiagonalWin = i >= secondMinor ? true : false;

                for (int k = 0; k < winLength; k++)
                {
                    horizontalWin &= (tableBool[i, j] == tableBool[i, j + k]);
                    verticalWin &= (tableBool[j, i] == tableBool[j + k, i]);
                    if(i < minorSize)
                        firstDiagonalWin &= (tableBool[i, j] == tableBool[i + k, j + k]);
                    else if (i >= secondMinor)
                        secondDiagonalWin &= (tableBool[i, j] == tableBool[i - k, j + k]);
                }

                if (horizontalWin)
                {
                    text.text = tableBool[i, j] ? "Circles win!" : "Crosses win!";
                    gameBehaviour.ChangeMaterial(i, j, 0, 1);
                    return;
                }
                else if (verticalWin)
                {
                    text.text = tableBool[j, i] ? "Circles win!" : "Crosses win!";
                    gameBehaviour.ChangeMaterial(j, i, 1, 0);
                    return;
                }
                else if (firstDiagonalWin)
                {
                    text.text = tableBool[i, j] ? "Circles win!" : "Crosses win!";
                    gameBehaviour.ChangeMaterial(i, j, 1, 1);
                    return;
                }
                else if (secondDiagonalWin)
                {
                    text.text = tableBool[i, j] ? "Circles win!" : "Crosses win!";
                    gameBehaviour.ChangeMaterial(i, j, -1, 1);
                    return;
                }
            }
        }

        text.text = "Nobody Wins :(";
    }
}