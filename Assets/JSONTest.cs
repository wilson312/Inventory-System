using UnityEngine;
using System.Collections;
using System.IO;
public class JSONTest : MonoBehaviour {
    string path;
    string jsonString;
	// Use this for initialization
	void Start () {
        path = Application.streamingAssetsPath + "/Items.json";
        jsonString = File.ReadAllText(path);
        Items Sword = JsonUtility.FromJson<Items>(jsonString);
        Debug.Log(Sword.Rarity);
        Sword.Rarity = "Legendary";
        string newSword = JsonUtility.ToJson(Sword);
        Debug.Log(newSword);
	}
	
}

[System.Serializable]
public class Items
{
    public string Name;
    public int Cost;
    public string Rarity;

}
