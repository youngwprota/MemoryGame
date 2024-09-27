using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Collection", menuName = "Card Game Objects / Card Collection")]

public class CardCollectionSO : ScriptableObject
{
    public List<CardSO> cards;
}
