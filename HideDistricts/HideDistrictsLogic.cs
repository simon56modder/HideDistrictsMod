using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ColossalFramework.Threading;
using ColossalFramework;
using ColossalFramework.Plugins;
using ColossalFramework.UI;

using HideDistricts.ModdingSkeletonCode;

namespace HideDistricts
{
    public class HideDistrictsLogic : MonoBehaviour
    {
        private Rect WindowRect;
        private bool ShowWindow;
        private Vector2 scrollListPos = Vector2.zero;
        Dictionary<int, string> districtNames;
        public Dictionary<int, bool> showDistricts;

        void Awake()
        {
            ShowWindow = false;
        }
        void Start()
        {
            WindowRect = new Rect(200, 70, 250, 350);
            DistrictManager.instance.ConstructDistrictsList(out districtNames, true);
            districtNames.ConstructEmptyShowPairsList(out showDistricts);
            Apply(true);
        }
        void Update()
        {
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftApple)) && Input.GetKey(KeyCode.H))
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    DistrictManager.instance.ConstructDistrictsList(out districtNames, true);
                   // if (showDistricts == null)
                        districtNames.ConstructEmptyShowPairsList(out showDistricts);
                    ShowWindow = !ShowWindow;
                }
            }
        }
        void OnGUI()
        {
            if (ShowWindow)
                WindowRect = GUI.Window(GetInstanceID(), WindowRect, DrawGUIwindow, "Hide Districts");
        }
        private void DrawGUIwindow(int id)
        {
            GUI.DragWindow(new Rect(0, 0, 212, 32));
            if (GUI.Button(new Rect(218, 4, 28, 29), "X"))
                ShowWindow = false;
            GUI.Box(new Rect(2, 35, 244, 275), string.Empty);
            scrollListPos = GUI.BeginScrollView(new Rect(5, 37, 240, 271), scrollListPos, new Rect(0, 0, 222, districtNames.Count * 28 + 2));
            int i = 0;
            foreach (KeyValuePair<int, string> district in districtNames)
            {
                GUI.Label(new Rect(2, i * 28 + 2, 170, 28), district.Value);
                if (!showDistricts.ContainsKey(district.Key))
                    showDistricts[district.Key] = true;
                showDistricts[district.Key] = GUI.Toggle(new Rect(171, i * 28, 50, 28), showDistricts[district.Key], "Show");
                i++;
            }
            GUI.EndScrollView();
            if (GUI.Button(new Rect(5, 315, 107.5f, 30), "Apply")) { Apply(); }
            if (GUI.Button(new Rect(117.5f, 315, 127.5f, 30), "Refresh district list"))
            {
                Refresh();
            }
        }
        public void Refresh()
        {
            DistrictManager.instance.ConstructDistrictsList(out districtNames, true);
            districtNames.ConstructEmptyShowPairsList(out showDistricts);
            Apply(true);
        }

        public void Apply(bool forceEverythingShown = false)
        {
            var pMgr = PropManager.instance;
            var tMgr = TreeManager.instance;
            var dMgr = DistrictManager.instance;
            for (int i = 0; i < pMgr.m_props.m_buffer.Length; i++)
            {
                // check if the prop data instance is in use
                if (((PropInstance.Flags)pMgr.m_props.m_buffer[i].m_flags & PropInstance.Flags.Created) != PropInstance.Flags.None)
                {
                    var district = dMgr.GetDistrict(pMgr.m_props.m_buffer[i].Position);
                    if (!showDistricts.ContainsKey(district))
                        continue;
                    pMgr.m_props.m_buffer[i].Hidden = forceEverythingShown ? false : !showDistricts[district];

                }
            }
            for (int i = 0; i < tMgr.m_trees.m_buffer.Length; i++)
            {
                // check if the tree data instance is in use
                if (((TreeInstance.Flags)tMgr.m_trees.m_buffer[i].m_flags & TreeInstance.Flags.Created) != TreeInstance.Flags.None)
                {
                    var district = dMgr.GetDistrict(tMgr.m_trees.m_buffer[i].Position);
                    
                    if (!showDistricts.ContainsKey(district))
                        continue;
                  //  Debug.Log("[HideDistricts] Info of tree #" + i.ToString() + " before hiding. Flags : " + tMgr.m_trees.m_buffer[i].m_flags.ToString() + ", hidden state : " + tMgr.m_trees.m_buffer[i].Hidden.ToString());
                    tMgr.m_trees.m_buffer[i].Hidden = forceEverythingShown ? false : !showDistricts[district];
                  //  Debug.Log("[HideDistricts] Info of tree #" + i.ToString() + " after hiding. Flags : " + tMgr.m_trees.m_buffer[i].m_flags.ToString() + ", hidden state : " + tMgr.m_trees.m_buffer[i].Hidden.ToString());
                }
            }
        }
        /*
        public void LoadDistrictData(DistrictDataContainer[] dataContainer)
        {
            if (showDistricts == null)
            {
                DistrictManager.instance.ConstructDistrictsList(out districtNames, true);
                districtNames.ConstructEmptyShowPairsList(out showDistricts);
            }
            foreach (DistrictDataContainer data in dataContainer)
            {
                if (showDistricts.ContainsKey(data.DistrictId))
                    showDistricts[data.DistrictId] = data.HideState;
            }
        } */
    }
}
