﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public static class SharedResources
    {
        public static JArray players { get; set; }
        public static string selectedPlayer { get; set; }
    }
}
