using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Serialization
{
    public static class JsonSerializer
    {
        public static void Serialize1(object fileName)
        {
            Serialize(fileName);
        }

        public static void Serialize2<T1, T2>(T1 fileName, T2 v1)
        {
            Serialize(fileName, v1);
        }

        public static void Serialize3<T1, T2, T3>(T1 fileName, T2 v1, T3 v2)
        {
            Serialize(fileName, v1, v2);
        }

        public static void Serialize4<T1, T2, T3, T4>(T1 fileName, T2 v1, T3 v2, T4 v3)
        {
            Serialize(fileName, v1, v2, v3);
        }

        public static void Serialize5<T1, T2, T3, T4, T5>(T1 fileName, T2 v1, T3 v2, T4 v3, T5 v4)
        {
            Serialize(fileName, v1, v2, v3, v4);
        }

        public static void Serialize6<T1, T2, T3, T4, T5, T6>(T1 fileName, T2 v1, T3 v2, T4 v3, T5 v4, T6 v5)
        {
            Serialize(fileName, v1, v2, v3, v4, v5);
        }

        public static void Serialize7<T1, T2, T3, T4, T5, T6, T7>(T1 fileName, T2 v1, T3 v2, T4 v3, T5 v4, T6 v5, T7 v6)
        {
            Serialize(fileName, v1, v2, v3, v4, v5, v6);
        }

        public static void Serialize8<T1, T2, T3, T4, T5, T6, T7, T8>(T1 fileName, T2 v1, T3 v2, T4 v3, T5 v4, T6 v5, T7 v6, T8 v7)
        {
            Serialize(fileName, v1, v2, v3, v4, v5, v6, v7);
        }

        public static void Serialize9<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 fileName, T2 v1, T3 v2, T4 v3, T5 v4, T6 v5, T7 v6, T8 v7, T9 v8)
        {
            Serialize(fileName, v1, v2, v3, v4, v5, v6, v7, v8);
        }

        public static void Serialize10<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 fileName, T2 v1, T3 v2, T4 v3, T5 v4, T6 v5, T7 v6, T8 v7, T9 v8, T10 v9)
        {
            Serialize(fileName, v1, v2, v3, v4, v5, v6, v7, v8, v9);
        }

        public static void Serialize11<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(T1 fileName, T2 v1, T3 v2, T4 v3, T5 v4, T6 v5, T7 v6, T8 v7, T9 v8, T10 v9, T11 v10)
        {
            Serialize(fileName, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10);
        }

        public static void Serialize12<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(T1 fileName, T2 v1, T3 v2, T4 v3, T5 v4, T6 v5, T7 v6, T8 v7, T9 v8, T10 v9, T11 v10, T12 v11)
        {
            Serialize(fileName, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11);
        }

        public static void Serialize(params object[] args)
        {
            var fileName = (string)args[0];
            if (args.Length != 1)
            {
                var values = args.Skip(1).ToArray();
                var json = JsonConvert.SerializeObject(
                    values,
                    Formatting.Indented,
                    new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Serialize, PreserveReferencesHandling = PreserveReferencesHandling.Objects }
                );
                File.WriteAllText(fileName, json);
            }
            else
            {
                File.WriteAllText(fileName, "");
            }
        }
    }
}