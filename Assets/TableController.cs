using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TableController : MonoBehaviour
{
    public static TextMeshProUGUI[] textMeshProArray;
    public static TextMeshProUGUI textMeshProVolt;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObj = GameObject.Find("ImageTable");
        TextMeshProUGUI[] allTextMeshPro = gameObj.GetComponentsInChildren<TextMeshProUGUI>();
        List<TextMeshProUGUI> includedTextMeshProList = new List<TextMeshProUGUI>();
        foreach (TextMeshProUGUI textMeshPro in allTextMeshPro)
        {
            if (textMeshPro.gameObject.name.StartsWith("Text (TMP)"))
            {
                includedTextMeshProList.Add(textMeshPro);
            }
            if(textMeshPro.gameObject.name.StartsWith("VoltTable"))
            {
                textMeshProVolt = textMeshPro;
            }
        }
        textMeshProArray = includedTextMeshProList.ToArray();

        textMeshProArray[0].text = "100";
        textMeshProArray[10].text = "100";
        textMeshProArray[20].text = "100";
        textMeshProArray[30].text = "100";

        textMeshProArray[2].text = "60";
        textMeshProArray[12].text = "300";
        textMeshProArray[22].text = "600";
        textMeshProArray[32].text = "---";

        textMeshProArray[7].text = "30";
        textMeshProArray[17].text = "30";
        textMeshProArray[27].text = "30";
        textMeshProArray[37].text = "30";

        textMeshProArray[8].text = "1,5*E-3";
        textMeshProArray[18].text = "1,5*E-3";
        textMeshProArray[28].text = "1,5*E-3";
        textMeshProArray[38].text = "1,5*E-3";

        textMeshProArray[33].text = "---";

        textMeshProArray[1].text = "";
        textMeshProArray[3].text = "";
        textMeshProArray[4].text = "";
        textMeshProArray[5].text = "";
        textMeshProArray[6].text = "";
        textMeshProArray[9].text = "";
        textMeshProArray[11].text = "";
        textMeshProArray[13].text = "";
        textMeshProArray[14].text = "";
        textMeshProArray[15].text = "";
        textMeshProArray[16].text = "";
        textMeshProArray[19].text = "";
        textMeshProArray[21].text = "";
        textMeshProArray[23].text = "";
        textMeshProArray[24].text = "";
        textMeshProArray[25].text = "";
        textMeshProArray[26].text = "";
        textMeshProArray[29].text = "";
        textMeshProArray[31].text = "";
        textMeshProArray[34].text = "";
        textMeshProArray[35].text = "";
        textMeshProArray[36].text = "";
        textMeshProArray[39].text = "";


    }

    // Update is called once per frame
    void Update()
    {
    }


}
