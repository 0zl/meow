using System;

namespace Grimoire.Botting
{
    public interface IBotEngine
    {
        bool IsRunning
        {
            get;
            set;
        }

        int OldIndex
        {
            get;
            set;
        }

        int Index
        {
            get;
            set;
        }

        Configuration OldConfiguration
        {
            get;
            set;
        }

        bool IsVar(string value);

        Configuration Configuration
        {
            get;
            set;
        }

        string GetVar(string value);

        event Action<bool> IsRunningChanged;

        event Action<int> IndexChanged;

        event Action<Configuration> ConfigurationChanged;

        void Start(Configuration config);

        void Stop();
    }
}