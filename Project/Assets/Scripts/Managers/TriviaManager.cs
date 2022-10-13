﻿using System.Collections.Generic;
using UnityEngine;

public class TriviaManager : MonoSingleton<TriviaManager>
{
    private const string TRIVIA_JSON_PATH = "Assets/GameData/TriviaData.json";

    private void Awake()
    {
        GetTrivia();
    }

    public Trivia GetTrivia()
    {
        Dictionary<string, object> dict = ParseUtils.GetParseData(TRIVIA_JSON_PATH).dict;
        Character newCharacter = new Character(dict);

        Debug.LogFormat("{0} has {1} trivias!", newCharacter.Name, newCharacter.Trivias.Count);

        return null;
    }
}