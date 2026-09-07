using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bnny.Scripts.Services.Settings
{
    public interface ISettingsService
    {
        public bool IsDebugPlayerActiveSetting { get; set; }
    }
}
