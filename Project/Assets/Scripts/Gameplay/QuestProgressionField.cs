﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestProgressionField : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_questDescText = null;
    [SerializeField] private TextMeshProUGUI m_questProgressText = null;

    public void Setup(Character _character)
    {
        m_questDescText.text = string.Format("Complete {0}'s trivia", _character.Name);
        UpdateProgress(_character);
    }

    public void UpdateProgress(Character _character)
    {
        m_questProgressText.text = string.Format("{0}/{1}", _character.CurrentProgress, _character.Trivias.Count);
    }
}