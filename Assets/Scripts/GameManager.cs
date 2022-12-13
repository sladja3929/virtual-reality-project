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
    private TextMeshProUGUI keyNumUI;

    GameObject monster;
    // Start is called before the first frame update
    void Start()
    {
        getKeyNum = 0;
        keyNumUI.text= getKeyNum.ToString();
        monster = GameObject.FindWithTag("Monster");
        //keyNumUI.GetComponent<TextMesh>().text = getKeyNum.ToString();
    }
    
    public void increaseKeyValue()
    {
        getKeyNum++;
        keyNumUI.text = getKeyNum.ToString();
        //keyNumUI.GetComponent<TextMesh>().text = getKeyNum.ToString();

        //¿­¼è È¹µæ ½Ã Àüµî ÇÏ³ª¾¿ on
        GoalLight.GetComponent<LightOn>().Light(getKeyNum-1);

        //¿­¼è È¹µæ ½Ã ¸ó½ºÅÍ AI º¯È­
        monster.GetComponent<MonsterAI>().ChangeAILevel(getKeyNum);
    }
    public int GetKeyNum()
    {
        return getKeyNum;
    }
    
}
