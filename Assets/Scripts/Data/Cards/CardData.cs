using System.Globalization;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "Data/CardData")]

public class CardData : ScriptableObject
{
    public string cardName;
    public string description;
    public int level;
    public Sprite imageSR;

}
