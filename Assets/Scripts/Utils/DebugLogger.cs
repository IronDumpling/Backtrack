using System.Collections;
using UnityEngine;

public static class DebugLogger {
    public static void Error(string objectName, string message) {
        Debug.LogError("["+ objectName + "]: " + message);
        Debug.Break();
    }
}
