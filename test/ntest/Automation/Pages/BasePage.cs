﻿using Automation.Helpers.Parameters;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Automation.Pages
{
    public abstract class BasePage
    {
        public BasePage(SetupHelperParameter setup)
        {

        }

        private static Dictionary<string, Action> s_validationCheckPoint = new Dictionary<string, Action>();

        protected Action GetValidationCheckPoint(string checkPointName)
        {
            if (s_validationCheckPoint.ContainsKey(checkPointName))
            {
                return s_validationCheckPoint[checkPointName];
            }

            return null;
        }

        public void AddValidationCheckPoint(string checkPointName, Action action)
        {
            Debug.Assert(!string.IsNullOrEmpty(checkPointName), "The check point name is empty.");
            Debug.Assert(action != null, "Action is empty.");

            if (!s_validationCheckPoint.ContainsKey(checkPointName))
            {
                s_validationCheckPoint.Add(checkPointName, action);
            }
            else
            {
                s_validationCheckPoint[checkPointName] = action;
            }
        }

        public virtual void Reset()
        {
            s_validationCheckPoint.Clear();
        }
    }
}
