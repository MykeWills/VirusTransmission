// Decompiled with JetBrains decompiler
// Type: UnityEngine.UI.SetPropertyUtility
// Assembly: UnityEngine.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3D03935D-4C70-4E01-A7B6-19C0F9853214
// Assembly location: C:\Users\claws\Desktop\Super Battle Mode\Library\UnityAssemblies\UnityEngine.UI.dll

using System.Collections.Generic;
using UnityEngine;

namespace FaceHorns.UI
{
    internal static class SetPropertyUtility
    {
        public static bool SetColor(ref Color currentValue, Color newValue)
        {
            if ((double)currentValue.r == (double)newValue.r && (double)currentValue.g == (double)newValue.g && ((double)currentValue.b == (double)newValue.b && (double)currentValue.a == (double)newValue.a))
                return false;
            currentValue = newValue;
            return true;
        }

        public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
        {
            if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
                return false;
            currentValue = newValue;
            return true;
        }

        public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
        {
            if ((object)currentValue == null && (object)newValue == null || (object)currentValue != null && currentValue.Equals((object)newValue))
                return false;
            currentValue = newValue;
            return true;
        }
    }
}
