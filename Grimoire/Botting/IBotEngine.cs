using Grimoire.Botting.Commands.Quest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grimoire.Botting
{
    public interface IBotEngine
    {
        bool IsRunning
        {
            get;
            set;
        }

        int Index
        {
            get;
            set;
        }
        bool paused 
        { 
            get; 
            set; 
        }

        bool IsVar(string value);

        string ResolveVars(string value);
        Configuration Configuration
        {
            get;
            set;
        }

        int CurrentConfiguration { get; set; }

        string GetVar(string value);

        event Action<bool> IsRunningChanged;

        event Action<int> IndexChanged;

        event Action<Configuration> ConfigurationChanged;

        void StartQuestList();

        void StopQuestList();

        void Start(Configuration config);

        void Stop();
    }
}