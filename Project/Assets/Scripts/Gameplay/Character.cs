using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character : IResultList
{
    public string Name { get; private set; }
    public List<Trivia> Trivias { get; private set; }
    public int CurrentProgress { get; private set; }

    private const string m_currentProgressKey = "currentProgress_{0}";

    public Character()
    {
    }
    public void Init(Dictionary<string, object> _dict)
    {
        Name = ParseUtils.GetString(_dict, "name");
        Trivias = ParseUtils.GetTypedList<Trivia>(_dict, "trivias");
        CurrentProgress = PlayerPrefs.GetInt(string.Format(m_currentProgressKey, Name), 0);
    }

    public void UpdateCurrentProgress()
    {
        if (CurrentProgress < Trivias.Count)
        {
            CurrentProgress++;
            PlayerPrefs.SetInt(string.Format(m_currentProgressKey, Name), CurrentProgress);
        }
    }
}