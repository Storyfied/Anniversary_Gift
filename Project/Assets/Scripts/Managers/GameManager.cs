using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private List<Character> m_characters = null;
    private const string TRIVIA_JSON_PATH = "Assets/GameData/TriviaData.json";

    private void Awake()
    {
        // Load character's data from json file
        Dictionary<string, object> dict = ParseUtils.GetParseData(TRIVIA_JSON_PATH).dict;
        m_characters = ParseUtils.GetTypedList<Character>(dict, "characters");
    }

    public void UpdateCharacterProgress(string _name)
    {
        Character character = GetCharacter(_name);
        character.UpdateCurrentProgress();
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
}