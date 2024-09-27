using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDatas", menuName = "Game /Datas")]
public class GameDatasSO : ScriptableObject
{
    [Header("Game Difficulty Settings")]
    public Difficulty difficulty;
    public int rows;
    public int columns;

    public Sprite background;

    [Header("Grid Layout")]
    public int preferredTopBottomPadding;
    public Vector2 spacing;
}
