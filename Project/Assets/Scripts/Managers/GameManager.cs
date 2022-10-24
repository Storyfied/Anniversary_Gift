using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private List<Character> m_characters = null;
    private List<string> m_bunnyMessages = null;
    private const string TRIVIA_JSON_PATH = "GameData/TriviaData";

    private void Awake()
    {
        // Load character's data from json file
        TextAsset jsonFile = Resources.Load(TRIVIA_JSON_PATH) as TextAsset;
        string jsonString = jsonFile.ToString();

        Dictionary<string, object> dict = ParseUtils.GetParseData(jsonString).dict;
        m_characters = ParseUtils.GetTypedList<Character>(dict, "characters");
        m_bunnyMessages = ParseUtils.GetStringList(dict, "bunnyMessages");
    }

    public List<Character> GetCharacterList()
    {
        return m_characters;
    }

    public Character GetCharacter(string _name)
    {
        // using First() instead of Find() because I want the system to throw an exception incase something goes wrong
        return m_characters.First(x => x.Name.ToLower() == _name.ToLower());
    }

    public bool HasProgression()
    {
        return m_characters.Any(x => x.CurrentProgress > 0);
    }

    public List<string> GetBunnyMessages()
    {
        return m_bunnyMessages;
    }
}