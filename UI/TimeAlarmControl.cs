/*******************************************************************************
 * Copyright (c) 2016, George Sedov
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice,
 * this list of conditions and the following disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 * this list of conditions and the following disclaimer in the documentation
 * and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 ******************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace KSPPreciseManeuver.UI {
  [RequireComponent (typeof (RectTransform))]
  public class TimeAlarmControl : MonoBehaviour {
    [SerializeField]
    private Text m_TimeValue = null;
    UnityAction<string> timeValueUpdate;

    [SerializeField]
    private Toggle m_ToggleAlarm = null;

    private ITimeAlarmControl m_Control = null;

    public void SetControl (ITimeAlarmControl control) {
      m_Control = control;
      timeValueUpdate = m_Control.ReplaceTextComponentWithTMPro (m_TimeValue);
      UpdateGUI ();
      m_Control.RegisterUpdateAction (UpdateGUI);
    }

    public void OnDestroy () {
      m_Control.DeregisterUpdateAction (UpdateGUI);
      m_Control = null;
    }

    public void ToggleAlarmAction (bool state) {
      m_Control.alarmToggle (state);
    }

    public void UpdateGUI () {
      timeValueUpdate?.Invoke (m_Control.TimeValue);
      if (m_Control.AlarmAvailable) {
        m_ToggleAlarm.interactable = true;
        m_ToggleAlarm.GetComponent<Image> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
        m_ToggleAlarm.isOn = m_Control.AlarmEnabled;
      } else {
        m_ToggleAlarm.interactable = true;
        m_ToggleAlarm.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        m_ToggleAlarm.isOn = m_Control.AlarmEnabled;
      }
    }
  }
}
