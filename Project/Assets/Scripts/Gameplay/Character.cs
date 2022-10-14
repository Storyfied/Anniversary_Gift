using System.Collections.Generic;

[System.Serializable]
public class Character : IResultList
{
    public string Name { get; private set; }
    public List<Trivia> Trivias { get; private set; }
    public int CurrentProgress { get; private set; }

    public Character()
    {
    }
    public void Init(Dictionary<string, object> _dict)
    {
        Name = ParseUtils.GetString(_dict, "name");
        Trivias = ParseUtils.GetTypedList<Trivia>(_dict, "trivias");
        CurrentProgress = ParseUtils.GetInt32(_dict, "currentProgress");
    }

    public void UpdateCurrentProgress()
    {
        if(CurrentProgress < Trivias.Count)
            CurrentProgress++;
    }
}