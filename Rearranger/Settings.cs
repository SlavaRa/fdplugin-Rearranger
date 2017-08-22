// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System;
using System.ComponentModel;
using System.Drawing;

namespace Rearranger
{
    [Serializable]
    public class Settings
    {
        [Browsable(false)] public Size RearrangerFormSize { get; set; }
    }
}