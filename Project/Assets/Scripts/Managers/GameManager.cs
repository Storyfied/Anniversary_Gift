using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private bool m_isIntroCompleted = false;
    private List<Character> m_characters = null;
    private List<string> m_bunnyMessages = null;
    private const string TRIVIA_JSON_PATH = "GameData/TriviaData";
    private const string BUNNY_JSON_PATH = "GameData/BunnyData";
    private const string INTRO_COMPLETED_KEY = "isIntroCompleted";

    private void Awake()
    {
        // Load character's data from json file
        TextAsset gameFile = Resources.Load(TRIVIA_JSON_PATH) as TextAsset;
        string gameString = gameFile.ToString();

        Dictionary<string, object> dict = ParseUtils.GetParseData(gameString).dict;
        m_characters = ParseUtils.GetTypedList<Character>(dict, "characters");

        //Load bunny data from json file
        TextAsset bunnyFile = Resources.Load(BUNNY_JSON_PATH) as TextAsset;
        string bunnyString = bunnyFile.ToString();

        Dictionary<string, object> bunnyDict = ParseUtils.GetParseData(bunnyString).dict;
        m_bunnyMessages = ParseUtils.GetStringList(bunnyDict, "bunnyMessages");

        // Load temper-able data
        m_isIntroCompleted = System.Convert.ToBoolean(PlayerPrefs.GetInt(INTRO_COMPLETED_KEY, 0));
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

    public List<string> GetBunnyMessages()
    {
        return m_bunnyMessages;
    }

    public void SetIntroCompleted()
    {
        m_isIntroCompleted = true;
        PlayerPrefs.SetInt(INTRO_COMPLETED_KEY, 1);
    }

    public bool GetIsIntroCompleted()
    {
        return m_isIntroCompleted;
    }
}