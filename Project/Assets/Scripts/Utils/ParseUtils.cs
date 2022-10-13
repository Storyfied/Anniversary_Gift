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
    public static ParseData GetParseData(string jsonPath)
    {
        StreamReader stream = new StreamReader(jsonPath);
        if (stream != null)
        {
            return JsonConvert.DeserializeObject<ParseData>(stream.ReadToEnd());
        }

        Debug.LogErrorFormat("Unable to read json at path -> {0}", jsonPath);
        return null;
    }

    /// <summary>
    /// Returns an int if retrievable. Otherwise, fallback value of -1 will be returned instead.
    /// </summary>
    public static int GetInt32(Dictionary<string, object> dict, string key, int? fallbackValue = null)
    {
        if (dict.ContainsKey(key))
            return Convert.ToInt32(dict[key]);

        else
        {
            Debug.LogErrorFormat("Unable to get int with key [{0}]!", key);
            return fallbackValue ?? -1;
        }
    }

    /// <summary>
    /// Returns a float if retrievable. Otherwise, fallback value of -1 will be returned instead.
    /// </summary>
    public static float GetFloat(Dictionary<string, object> dict, string key, float? fallbackValue = null)
    {
        if (dict.ContainsKey(key))
            return (float)Convert.ToDouble(dict[key]);

        else
        {
            Debug.LogErrorFormat("Unable to get float with key [{0}]!", key);
            return fallbackValue ?? -1f;
        }
    }

    /// <summary>
    /// Returns a string if retrievable. Otherwise, fallback value of "null" will be returned instead.
    /// </summary>
    public static string GetString(Dictionary<string, object> dict, string key, string fallbackValue = null)
    {
        if (dict.ContainsKey(key))
            return Convert.ToString(dict[key]);

        else
        {
            Debug.LogErrorFormat("Unable to get string with key [{0}]!", key);
            return fallbackValue ?? "null";
        }
    }

    /// <summary>
    /// Returns a typed object if retrievable.
    /// </summary>
    public static T GetTypedObject<T>(Dictionary<string, object> result, string key) where T : IResultList, new()
    {
        if (result.ContainsKey(key))
        {
            T objectInfo = new T();
            Dictionary<string, object> dict = (Dictionary<string, object>)result[key];
            objectInfo.Init(dict);

            return objectInfo;
        }
        else
        {
            Debug.LogErrorFormat("Unable to get typed object with key [{0}]", key);
            return default(T);
        }
    }

    /// <summary>
    /// Returns a list of strings if retrievable.
    /// </summary>
    public static List<string> GetStringList(Dictionary<string, object> dict, string key)
    {
        List<object> objList = GetList(dict, key);
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
            Debug.LogErrorFormat("Unable to get string list with key [{0}]", key);
            return null;
        }
    }

    /// <summary>
    /// Returns a list of typed objects if retrievable.
    /// </summary>
    public static List<T> GetTypedList<T>(Dictionary<string, object> dict, string key) where T : IResultList, new()
    {
        List<object> objList = GetList(dict, key);
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
            Debug.LogErrorFormat("Unable to get typed list with key [{0}]", key);
            return null;
        }
    }

    /// <summary>
    /// Returns a Dictionary with a KEY type of STRING and an VALUE type of OBJECT.
    /// </summary>
    public static Dictionary<string, object> GetDictionary(Dictionary<string, object> dict, string key)
    {
        if(dict.ContainsKey(key))
        {
            return (Dictionary<string, object>)dict[key];
        }

        else
        {
            Debug.LogErrorFormat("Unable to get dictionary with key [{0}]", key);
            return null;
        }
    }

    /// <summary>
    /// Returns a list of objects.
    /// </summary>
    public static List<object> GetList(Dictionary<string, object> dict, string key)
    {
        if (dict.ContainsKey(key))
        {
            return (dict[key] as IEnumerable<object>).Cast<object>().ToList();
        }

        else
        {
            Debug.LogErrorFormat("Key {0} not found", key);
            return null;
        }
    }
}

public class ParseData
{
    public Dictionary<string, object> dict;
}
