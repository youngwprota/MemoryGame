using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[CreateAssetMenu(fileName="Card", menuName = "Card Game Objects/Card")]
public class CardSO : ScriptableObject
{
    public string cardName;
    public string cardPair;
    public Sprite cardImage;
}
