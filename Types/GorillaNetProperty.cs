using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;

namespace GorillaRPC.Types
{
    public class GorillaNetProperty
    {
        public string Key { get; }
        public object Value { get; }

        public GorillaNetProperty(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or whitespace.", nameof(key));

            Key = key;
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public void AddToHashtable(Hashtable hashtable)
        {
            if (hashtable == null)
                throw new ArgumentNullException(nameof(hashtable));

            if (hashtable.ContainsKey(Key))
                hashtable[Key] = Value;
            else
                hashtable.Add(Key, Value);
        }

        public static GorillaNetProperty FromHashtable(Hashtable hashtable, string key)
        {
            if (hashtable == null)
                throw new ArgumentNullException(nameof(hashtable));

            if (!hashtable.ContainsKey(key))
                throw new KeyNotFoundException($"Key '{key}' not found in hashtable.");

            return new GorillaNetProperty(key, hashtable[key]);
        }
    }
}
