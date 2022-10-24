using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

public static class ParseUtils
{
    /// <summary>
    /// Parses a json file and returns a Dictionary with a KEY type of STRING and an VALUE type of OBJECT.
    /// </summary>
    /*public static ParseData GetParseData(string _jsonPath)
    {
        StreamReader stream = new StreamReader(_jsonPath);
        if (stream != null)
        {
            return JsonConvert.DeserializeObject<ParseData>(stream.ReadToEnd());
        }

        Debug.LogErrorFormat("Unable to read json at path -> {0}", _jsonPath);
        return null;
    }*/

    public static ParseData GetParseData(string _jsonString)
    {
        if (!string.IsNullOrEmpty(_jsonString))
        {
            return JsonConvert.DeserializeObject<ParseData>(_jsonString);
        }

        Debug.LogErrorFormat("Unable to read json!");
        return null;
    }

    /// <summary>
    /// Returns an int if retrievable. Otherwise, fallback value of -1 will be returned instead.
    /// </summary>
    public static int GetInt32(Dictionary<string, object> _dict, string _key, int? _fallbackValue = null)
    {
        if (_dict.ContainsKey(_key))
            return Convert.ToInt32(_dict[_key]);

        else
        {
            Debug.LogErrorFormat("Unable to get int with key [{0}]!", _key);
            return _fallbackValue ?? -1;
        }
    }

    /// <summary>
    /// Returns a float if retrievable. Otherwise, fallback value of -1 will be returned instead.
    /// </summary>
    public static float GetFloat(Dictionary<string, object> _dict, string _key, float? _fallbackValue = null)
    {
        if (_dict.ContainsKey(_key))
            return (float)Convert.ToDouble(_dict[_key]);

        else
        {
            Debug.LogErrorFormat("Unable to get float with key [{0}]!", _key);
            return _fallbackValue ?? -1f;
        }
    }

    /// <summary>
    /// Returns a string if retrievable. Otherwise, fallback value of "null" will be returned instead.
    /// </summary>
    public static string GetString(Dictionary<string, object> _dict, string _key, string _fallbackValue = null)
    {
        if (_dict.ContainsKey(_key))
            return Convert.ToString(_dict[_key]);

        else
        {
            Debug.LogErrorFormat("Unable to get string with key [{0}]!", _key);
            return _fallbackValue ?? "null";
        }
    }

    /// <summary>
    /// Returns a typed object if retrievable.
    /// </summary>
    public static T GetTypedObject<T>(Dictionary<string, object> _dict, string _key) where T : IResultList, new()
    {
        if (_dict.ContainsKey(_key))
        {
            T objectInfo = new T();
            Dictionary<string, object> dict = (Dictionary<string, object>)_dict[_key];
            objectInfo.Init(dict);

            return objectInfo;
        }
        else
        {
            Debug.LogErrorFormat("Unable to get typed object with key [{0}]", _key);
            return default(T);
        }
    }

    /// <summary>
    /// Returns a list of strings if retrievable.
    /// </summary>
    public static List<string> GetStringList(Dictionary<string, object> _dict, string _key)
    {
        List<object> objList = GetList(_dict, _key);
        if (objList != null)
        {
            List<string> stringList = new List<string>(objList.Count);
            foreach (object obj in objList)
            {
                stringList.Add(obj.ToString());
            }

            return stringList;
        }

        else
        {
            Debug.LogErrorFormat("Unable to get string list with key [{0}]", _key);
            return null;
        }
    }

    /// <summary>
    /// Returns a list of typed objects if retrievable.
    /// </summary>
    public static List<T> GetTypedList<T>(Dictionary<string, object> _dict, string _key) where T : IResultList, new()
    {
        List<object> objList = GetList(_dict, _key);
        if (objList != null)
        {
            List<T> resultList = new List<T>(objList.Count);
            foreach (object obj in objList)
            {
                T newObj = new T();
                //Dictionary<string, object> objDict = (Dictionary<string, object>)obj;
                string json = JsonConvert.SerializeObject(obj);
                Dictionary<string, object> objDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                newObj.Init(objDict);
                resultList.Add(newObj);
            }

            return resultList;
        }

        else
        {
            Debug.LogErrorFormat("Unable to get typed list with key [{0}]", _key);
            return null;
        }
    }

    /// <summary>
    /// Returns a Dictionary with a KEY type of STRING and an VALUE type of OBJECT.
    /// </summary>
    public static Dictionary<string, object> GetDictionary(Dictionary<string, object> _dict, string _key)
    {
        if(_dict.ContainsKey(_key))
        {
            return (Dictionary<string, object>)_dict[_key];
        }

        else
        {
            Debug.LogErrorFormat("Unable to get dictionary with key [{0}]", _key);
            return null;
        }
    }

    /// <summary>
    /// Returns a list of objects.
    /// </summary>
    public static List<object> GetList(Dictionary<string, object> _dict, string _key)
    {
        if (_dict.ContainsKey(_key))
        {
            return (_dict[_key] as IEnumerable<object>).Cast<object>().ToList();
        }

        else
        {
            Debug.LogErrorFormat("Key {0} not found", _key);
            return null;
        }
    }
}

public class ParseData
{
    public Dictionary<string, object> dict;
}
