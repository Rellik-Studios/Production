using System.Collections.Generic;
using UnityEngine;

namespace Himanshu
{
    [System.Serializable]
    public class NarratorDialogue
    {

        private List<string> m_idleRoom;

        private List<string> m_ballRoom;

        private List<string> m_cafeteria;

        private List<string> m_maze;

        private List<string> m_bathroom;

        private List<string> m_mirrorShatter;

        private List<string> m_hospital1950;

        private List<string> m_hospitalIdle;

        private List<string> m_hospital1870;

        private List<string> m_hospitalFuturistic;

        private List<string> m_hospitalPresent;

        private List<string> m_hub1;

        private List<string> m_hub2;

        private List<string> m_finish1;
        
        private List<string> m_spottedLines;
        
        private List<string> m_madeSound;
        
        private List<string> m_corridor;
        
        private List<string> m_breathing;




        public void SetValues(Narrator _narrator)
        {
            m_idleRoom = _narrator.m_idleRoom;
            m_ballRoom = _narrator.m_ballRoom;
            m_cafeteria = _narrator.m_cafeteria;
            m_maze = _narrator.m_maze;
            m_bathroom = _narrator.m_bathroom;
            m_mirrorShatter = _narrator.m_mirrorShatter;
            m_hospital1950 = _narrator.m_hospital1950;
            m_hospitalIdle = _narrator.m_hospitalIdle;
            m_hospital1870 = _narrator.m_hospital1870;
            m_hospitalFuturistic = _narrator.m_hospitalFuturistic;
            m_hospitalPresent = _narrator.m_hospitalPresent;
            m_hub1 = _narrator.m_hub1;
            m_hub2 = _narrator.m_hub2;
            m_finish1 = _narrator.m_finish1;
            m_spottedLines = _narrator.m_spottedLines;
            m_madeSound = _narrator.m_madeSound;
            m_corridor = _narrator.m_corridor;
            m_breathing = _narrator.m_breathing;
        }

        public void RetrieveValues(Narrator _narrator)
        {
            _narrator.m_idleRoom = m_idleRoom;
            _narrator.m_ballRoom = m_ballRoom;
            _narrator.m_cafeteria = m_cafeteria;
            _narrator.m_maze = m_maze;
            _narrator.m_bathroom = m_bathroom;
            _narrator.m_mirrorShatter = m_mirrorShatter;
            _narrator.m_hospital1950 = m_hospital1950;
            _narrator.m_hospitalIdle = m_hospitalIdle;
            _narrator.m_hospital1870 = m_hospital1870;
            _narrator.m_hospitalFuturistic = m_hospitalFuturistic;
            _narrator.m_hospitalPresent = m_hospitalPresent;
            _narrator.m_hub1 = m_hub1;
            _narrator.m_hub2 = m_hub2;
            _narrator.m_finish1 = m_finish1;
            _narrator.m_spottedLines = m_spottedLines;
            _narrator.m_madeSound = m_madeSound;
            _narrator.m_corridor = m_corridor;
            _narrator.m_breathing = m_breathing;
        }
    }
}