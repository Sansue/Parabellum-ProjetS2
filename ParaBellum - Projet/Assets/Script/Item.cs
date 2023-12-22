using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string name;
    public int ammo = 10;

}