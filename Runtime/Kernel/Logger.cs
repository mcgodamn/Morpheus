﻿using System;
using System.Diagnostics;

namespace Morpheus
{
<<<<<<<< HEAD:Runtime/Kernel/Kernel.Logger.cs
<<<<<<<< HEAD:Runtime/Kernel/Kernel.Logger.cs
    [Flags]
    public enum LogChannel
    {
        None = 0,
        GameTime = 1,
        Resource = GameTime << 1,
        Input = Resource << 1,
        Network = Input << 1,
        GameState = Network << 1,
        Audio = GameState << 1,
        UI = Audio << 1,
        All = -1
    }

    public static partial class Kernel
========
    public static class Logger
>>>>>>>> 15abe62 (Refacetor naming: DebugLogger -> Logger):Runtime/Kernel/Logger.cs
========
    public static class Logger
>>>>>>>> 15abe62f3f2376d09dd6cb0ddfc39848993e5620:Runtime/Kernel/Logger.cs
    {
        public const int AllLogChannel = -1;
        public const string TraceLogCondition = "TRACE_LOG";
        public const string DebugLogCondition = "DEBUG_LOG";

        public static int TraceLogChannel = AllLogChannel;
        public static int DebugLogChannel = AllLogChannel;

        public static void FileLog(string message, int channel = AllLogChannel)
        {
            // TODO: Implement
        }

        [Conditional(TraceLogCondition)]
        public static void TraceLog(string message, int channel = AllLogChannel)
        {
            if ((channel & TraceLogChannel) != 0)
            {
                UnityEngine.Debug.Log(message);
            }
        }

        [Conditional(DebugLogCondition)]
        public static void Log(string message, int channel = AllLogChannel)
        {
            if ((channel & DebugLogChannel) != 0)
            {
                UnityEngine.Debug.Log(message);
            }
        }

        [Conditional(DebugLogCondition)]
        public static void LogWarning(string message, int channel = AllLogChannel)
        {
            if ((channel & DebugLogChannel) != 0)
            {
                UnityEngine.Debug.LogWarning(message);
            }
        }

        [Conditional(DebugLogCondition)]
        public static void LogError(string message)
        {
            UnityEngine.Debug.LogError(message);
        }

        [Conditional(DebugLogCondition)]
        public static void Assert(bool condition, string message)
        {
            UnityEngine.Debug.Assert(condition, message);
        }
    }
}