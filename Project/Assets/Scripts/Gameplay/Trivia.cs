using System.Collections.Generic;

[System.Serializable]
public class Trivia : IResultList
{
    public string Question { get; private set; }
    public List<string> Answers { get; private set; }
    public int CorrectAnswer { get; private set; }

    public Trivia()
    {
    }

    public void Init(Dictionary<string, object> _dict)
    {
        Question = ParseUtils.GetString(_dict, "question");
        Answers = ParseUtils.GetStringList(_dict, "answers");
        CorrectAnswer = ParseUtils.GetInt32(_dict, "correctAnswer");
    }
}
