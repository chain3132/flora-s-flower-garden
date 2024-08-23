using UnityEngine;

[CreateAssetMenu (fileName = "NewFlower" , menuName = "Flower")]
public class Flower : ScriptableObject
{
    
    public string flowername;
    public Sprite[] sprites;
    public int[] growstage;
    


}