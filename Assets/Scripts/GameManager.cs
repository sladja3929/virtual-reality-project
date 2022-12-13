using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    
    public int getKeyNum;
    public GameObject GoalLight;
    [SerializeField]
    private TMP_Text keyNumUI;
    // Start is called before the first frame update
    void Start()
    {
        getKeyNum = 0;
        keyNumUI.text= getKeyNum.ToString();
        //keyNumUI.GetComponent<TextMesh>().text = getKeyNum.ToString();
    }
    
    public void increaseKeyValue()
    {
        getKeyNum++;
        keyNumUI.text = getKeyNum.ToString();
        //keyNumUI.GetComponent<TextMesh>().text = getKeyNum.ToString();

        //¿­¼è È¹µæ ½Ã Àüµî ÇÏ³ª¾¿ on
        GoalLight.GetComponent<LightOn>().Light(getKeyNum-1);
    }
    public int GetKeyNum()
    {
        return getKeyNum;
    }
    
}
