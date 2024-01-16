using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Lights Out/Item Picker Data", fileName = "Item Picker Data")]
public class ItemPickerData : ScriptableObject
{
    public List<ItemPicker> platformItems;
}
