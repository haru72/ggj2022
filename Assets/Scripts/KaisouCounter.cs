using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaisouCounter : MonoBehaviour
{
    const int m_maxCount = 7; // �C��̐�
    int m_nowCount = 0;

    string[] m_kaisouText = new string[]
    {
          "���� �������Â��ĕ|���ȁc�c"
        , "�����������̃��E�\�N �g�����ȁc�c"
        , "������������o�Ȃ�����c�c"
        , "�����厖�Ȃ��Ƃ�Y��Ă��������c�c"
        , "����������񂪑҂��Ă�C������c�c"
        , "���E�\�N �c���Ă����Ȃ�����c�c"
        , "���������΁c�c �����͖l�̒a������"
    };

    public string GetKaisouText()
    {
        if (m_nowCount < m_maxCount) m_nowCount++;
        return m_kaisouText[m_nowCount - 1];
    }
}
