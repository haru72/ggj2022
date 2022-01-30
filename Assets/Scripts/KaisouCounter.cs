using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaisouCounter : MonoBehaviour
{
    const int m_maxCount = 7; // 燭台の数
    int m_nowCount = 0;

    string[] m_kaisouText = new string[]
    {
          "ここ すごく暗くて怖いな……"
        , "おじいちゃんのロウソク 暖かいな……"
        , "早くここから出なくちゃ……"
        , "何か大事なことを忘れていたっけ……"
        , "おじいちゃんが待ってる気がする……"
        , "ロウソク 残しておかなくちゃ……"
        , "そういえば…… 今日は僕の誕生日だ"
    };

    public string GetKaisouText()
    {
        if (m_nowCount < m_maxCount) m_nowCount++;
        return m_kaisouText[m_nowCount - 1];
    }
}
