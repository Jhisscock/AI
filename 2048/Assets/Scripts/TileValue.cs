using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileValue : MonoBehaviour
{
    public int tileNum;

    void Start(){
        tileNum = int.Parse(this.gameObject.transform.Find("Canvas/Text").GetComponent<Text>().text);
    }
    void Update()
    {
        
    }

    public int GetTileNum(){
        return this.tileNum;
    }

    public  int ChangeTileNum(int newTileNum){
        this.tileNum = newTileNum;
        return this.tileNum;
    }
}
