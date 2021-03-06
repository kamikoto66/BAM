﻿using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericTable<T> where T : BaseDescriptor
{
    public Dictionary<string, T> dictionary;

    public GenericTable(string path)
    {
        var descriptors = GetDescriptors(path);
        CreateDictionary(descriptors);
    }

    public List<T> GetDescriptors(string path)
    {
        var textAsset = Resources.Load<TextAsset>(path);
        var json = textAsset.text;
        return JsonConvert.DeserializeObject<List<T>>(json);
    }

    public void CreateDictionary(List<T> descriptors)
    {
        dictionary = new Dictionary<string, T>();
        foreach (var d in descriptors)
        {
            dictionary.Add(d.Id, d);
        }
    }

    public T Find(string id, bool showLog = true)
    {
        T desc = null;
        if (dictionary.TryGetValue(id, out desc))
        {
            return desc;
        }
        else
        {
            if (showLog)
                Debug.LogErrorFormat("{0} Descriptor not found - {1}", this.GetType().Name, id);

            return null;
        }
    }

    public IEnumerable<T> All()
    {
        return dictionary.Values;
    }
}