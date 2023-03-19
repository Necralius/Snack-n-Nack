using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace NekraliusDevelopmentStudio
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //SerializableDictionary - (0.3)
        //State: Functional (Needs Refactoring, Needs Coments)
        //This code represents (Code functionality or code meaning)

        [Serializable]
        private struct SerialiableKeyValuePair
        {
            public TKey key;
            public TValue value;

            public SerialiableKeyValuePair(TKey key, TValue value)
            {
                this.key = key;
                this.value = value;
            }
        }

        [SerializeField] private List<SerialiableKeyValuePair> _serializedPairs;

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {          
            _serializedPairs = new List<SerialiableKeyValuePair>();
            foreach (var kvp in this) _serializedPairs.Add(new SerialiableKeyValuePair(kvp.Key, kvp.Value));
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.Clear();

            for (int i = 0; i != _serializedPairs.Count; i++)
            {
                if (this.ContainsKey(_serializedPairs[i].key))
                {
                    TKey[] attempt = new TKey[1] { new string("change the key").ConvertTo<TKey>() };
                    this.Add(attempt[0], _serializedPairs[i].value);
                }
                else this.Add(_serializedPairs[i].key, _serializedPairs[i].value);
            }
        }
        void OnGUI()
        {
            foreach (var kvp in this) GUILayout.Label("Key: " + kvp.Key + " value: " + kvp.Value);
        }
    }
}