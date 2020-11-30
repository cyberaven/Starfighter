using System.Runtime.Serialization;
using UnityEngine;

namespace Net.Utils
{
    public class Vector3SerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Vector3 v3 = (Vector3) obj;
            info.AddValue("x", v3.x);
            info.AddValue("y", v3.y);
            info.AddValue("z", v3.z);
            // Debug.Log(v3);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Vector3 v3 = (Vector3) obj;
            v3.x = (float)info.GetValue("x", typeof(float));
            v3.y = (float)info.GetValue("y", typeof(float));
            v3.z = (float)info.GetValue("z", typeof(float));
            obj = v3;
            return obj;   // Formatters ignore this return value //Seems to have been fixed!
        }
    }
    
    public class QuaternionSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var v3 = (Quaternion) obj;
            info.AddValue("w", v3.w);
            info.AddValue("x", v3.x);
            info.AddValue("y", v3.y);
            info.AddValue("z", v3.z);
            // Debug.Log(v3);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var v3 = (Quaternion) obj;
            v3.w = (float)info.GetValue("w", typeof(float));
            v3.x = (float)info.GetValue("x", typeof(float));
            v3.y = (float)info.GetValue("y", typeof(float));
            v3.z = (float)info.GetValue("z", typeof(float));
            obj = v3;
            return obj;   // Formatters ignore this return value //Seems to have been fixed!
        }
    }
}