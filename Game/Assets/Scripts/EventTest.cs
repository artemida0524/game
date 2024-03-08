using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventTest
{
    public static Action action { get; set; }
    public static Action<int> action2 { get; set;}
    public static Func<int, string> action3 { get; set; }
}
