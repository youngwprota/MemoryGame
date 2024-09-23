using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDatas", menuName = "Game /Datas")]
public class GameDataSO : ScriptableObject
{
    [Header("Game Difficulty Settings")]
    public Difficulty difficulty;
    public int rows;
    public int columns;

    [Header("Grid Layout")]
    public int preferredTopBottomPadding;
    public Vector2 spacing;
}
