﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.Client.Utils.Helpers.Interfaces
{
    public interface IAppSettingsProvider
    {
        void Save();
        void Load();
    }
}
