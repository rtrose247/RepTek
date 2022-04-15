using System;
using System.Collections.Generic;
using System.Text;

namespace Conscript.IDE
{
    internal class ScriptHistoryManager
    {
        #region Private Classes

        private class ScriptHistory
        {
            public List<String> m_listHistory;
            public int m_iCurrentState;
        }

        #endregion

        #region Private Variables

        private Dictionary<String, ScriptHistory> m_dictScriptHistory;

        #endregion

        #region Public Methods

        public ScriptHistoryManager()
        {
            m_dictScriptHistory
                = new Dictionary<string, ScriptHistory>();
        }

        public bool CanUndo(String strScriptName)
        {
            if (!m_dictScriptHistory.ContainsKey(strScriptName))
                return false;
            ScriptHistory scriptHistory
                = m_dictScriptHistory[strScriptName];
            return scriptHistory.m_iCurrentState > 0;
        }

        public bool CanRedo(String strScriptName)
        {
            if (!m_dictScriptHistory.ContainsKey(strScriptName))
                return false;
            ScriptHistory scriptHistory
                = m_dictScriptHistory[strScriptName];
            return scriptHistory.m_iCurrentState
                < scriptHistory.m_listHistory.Count - 1;
        }

        public void InitialiseHistory(
            String strScriptName, String strScriptSource)
        {
            ScriptHistory scriptHistory = new ScriptHistory();
            scriptHistory.m_listHistory = new List<string>();
            scriptHistory.m_listHistory.Add(strScriptSource);
            scriptHistory.m_iCurrentState = 0;
            m_dictScriptHistory[strScriptName] = scriptHistory;
        }

        public void RenameScript(
            String strScriptNameOld, String strScriptNameNew)
        {
            if (!m_dictScriptHistory.ContainsKey(strScriptNameOld))
                return;

            ScriptHistory scriptHistory = m_dictScriptHistory[strScriptNameOld];
            m_dictScriptHistory.Remove(strScriptNameOld);
            m_dictScriptHistory[strScriptNameNew] = scriptHistory;
        }

        public void ClearHistory(String strScriptName)
        {
            m_dictScriptHistory.Remove(strScriptName);
        }

        public void AddState(String strScriptName, String strScriptSource)
        {
            if (!m_dictScriptHistory.ContainsKey(strScriptName))
            {
                InitialiseHistory(strScriptName, strScriptSource);
                return;
            }

            ScriptHistory scriptHistory
                = m_dictScriptHistory[strScriptName];

            // remove future 'redo's
            int iRedoCount = scriptHistory.m_listHistory.Count - 1
                - scriptHistory.m_iCurrentState;
            if (iRedoCount > 0)
                scriptHistory.m_listHistory.RemoveRange(
                    scriptHistory.m_iCurrentState + 1,
                    iRedoCount);

            scriptHistory.m_listHistory.Add(strScriptSource);
            scriptHistory.m_iCurrentState = scriptHistory.m_listHistory.Count - 1;
        }

        public String Undo(String strScriptName)
        {
            if (!CanUndo(strScriptName)) return null;

            ScriptHistory scriptHistory
                = m_dictScriptHistory[strScriptName];
            --scriptHistory.m_iCurrentState;
            return scriptHistory.m_listHistory[
                scriptHistory.m_iCurrentState];
        }

        public String Redo(String strScriptName)
        {
            if (!CanRedo(strScriptName)) return null;

            ScriptHistory scriptHistory
                = m_dictScriptHistory[strScriptName];
            ++scriptHistory.m_iCurrentState;
            return scriptHistory.m_listHistory[
                scriptHistory.m_iCurrentState];
        }

        public int GetChangePosition(
            String strSourceBefore, String strSourceAfter)
        {
            int iIndexBefore = strSourceBefore.Length;
            int iIndexAfter = strSourceAfter.Length;

            while (iIndexBefore > 0 && iIndexAfter > 0)
            {
                if (strSourceBefore[--iIndexBefore] != strSourceAfter[--iIndexAfter])
                {
                    return iIndexAfter + 1;
                }
            }

            return 0;
        }

        #endregion
    }
}
