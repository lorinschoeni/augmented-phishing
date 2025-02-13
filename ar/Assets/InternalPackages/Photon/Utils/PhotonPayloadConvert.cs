using Newtonsoft.Json;
using System;
using UnityEngine;

namespace PhishAR.Photon.Utils
{
    public static class PhotonPayloadConvert
    {
        public static bool TryDeserializeObject<T>(object payload, out T result)
        {
            result = default;

            if (payload == null)
            {
                Debug.LogError("Payload is empty");
                return false;
            }

            if (!TrySerializeObject(payload, out var json)) return false;

            try
            {
                result = JsonConvert.DeserializeObject<T>(json);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Error while deserializing object\n{e}");
                return false;
            }
        }

        public static bool TrySerializeObject(object payload, out string result)
        {
            result = null;
            try
            {
                result = JsonConvert.SerializeObject(payload);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Error while serializing object\n{e}");
                return false;
            }
        }
    }
}
