using System.Collections.Generic;

public class Character
{
    public string Name { get; private set; }
    public List<Trivia> Trivias { get; private set; }

    public Character(Dictionary<string, object> dict)
    {
        Name = ParseUtils.GetString(dict, "name");
        Trivias = ParseUtils.GetTypedList<Trivia>(dict, "trivias");
    }
}

public class Trivia : IResultList
{
    public string Question { get; private set; }
    public List<string> Answers { get; private set; }
    public int CorrectAnswer { get; private set; }

    public Trivia()
    {
    }

    public void Init(Dictionary<string, object> dict)
    {
        Question = ParseUtils.GetString(dict, "question");
        Answers = ParseUtils.GetStringList(dict, "answers");
        CorrectAnswer = ParseUtils.GetInt32(dict, "correctAnswer");
    }
}