using UnityEngine;
using System.Collections;
using System.IO;
public class JSONSave : MonoBehaviour {
    public Vector3 lastPos = Vector3.zero;
    public Vector3 lastScale = Vector3.zero;
    public Quaternion lastRot = Quaternion.identity;
    public Transform thisTransform = null;
    public int str, agi, intel;
    string savePath;
    bool movingUp,moveDown,moveRight,moveLeft,scaleUp,scaleDown;
	// Use this for initialization
 
	void Start () {
        savePath = Application.streamingAssetsPath + "/SaveGame.json";
        LoadGame();
    }
	
	// Update is called once per frame
	void Update () {
        if (movingUp)
            transform.position += transform.up;
        if (moveDown)
            transform.position -= transform.up;
        if (moveLeft)
            transform.position -= transform.right;
        if (moveRight)
            transform.position += transform.right;
        if(scaleUp)
            transform.localScale += Vector3.one;
        if (scaleDown)
            transform.localScale -= Vector3.one;
    }
    void OnDestroy()
    {
        SaveGame();
    }
    public void SaveGame()
    {
        lastPos = thisTransform.position;
        lastRot = thisTransform.rotation;
        lastScale = thisTransform.localScale;
        StreamWriter write = new StreamWriter(savePath);
        write.WriteLine(JsonUtility.ToJson(this));
        write.Close();
    }
    public void LoadGame()
    {
       StreamReader load = new StreamReader(savePath);
        string JSONString = load.ReadToEnd();
        JsonUtility.FromJsonOverwrite(JSONString, this);
        load.Close();
        thisTransform.position = lastPos;
        thisTransform.rotation = lastRot;
        thisTransform.localScale = lastScale;
    }
    public void MoveUp(int value)
    {
        if (value == 1)
            movingUp = true;
        else
            movingUp = false;
    }
    public void MoveDown(int value)
    {
        if (value == 1)
            moveDown = true;
        else
            moveDown = false;
    }
    public void MoveLeft(int value)
    {
        if (value == 1)
            moveLeft = true;
        else
            moveLeft = false;
    }
    public void MoveRight(int value)
    {
        if (value == 1)
            moveRight = true;
        else
            moveRight = false;
    }
    public void ScaleUp(int value)
    {
        if (value == 1)
            scaleUp = true;
        else
            scaleUp = false;
    }
    public void ScaleDown(int value)
    {
        if (value == 1)
            scaleDown = true;
        else
            scaleDown = false;
    }
}
