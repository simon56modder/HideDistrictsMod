using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ColossalFramework.Plugins;
using ColossalFramework;
using UnityEngine;

namespace HideDistricts.ModdingSkeletonCode
{
    // Code of the methods IsValid, IsAlive and ConstructDistrictsList
    // extracted and modified from https://github.com/romanoza/cities-skylines-renamer/blob/master/RomanozasMod/LoadingExtension.cs

    public static class DistrictExtensions
    {
        public static bool IsValid(this District district)
        {
            return (district.m_flags != District.Flags.None);
        }

        public static bool IsAlive(this District district)
        {
            // Get the flags on the district, to ensure we don't access garbage memory if it doesn't have a flag for District.Flags.Created
            return ((district.m_flags & District.Flags.Created) == District.Flags.Created);
        }
        public static void ConstructDistrictsList(this DistrictManager districtManager, out Dictionary<int, string> districtNames, bool includeCityAsDistrict)
        {
            districtNames = new Dictionary<int, string>();
            District[] districts = districtManager.m_districts.m_buffer;
            int i = 0;
            foreach (District d in districts)
            {
                string districtName;
                if (i == 0)
                {
                    if (includeCityAsDistrict)
                    {
                        districtName = "[Rest of the city] " + Singleton<SimulationManager>.instance.m_metaData.m_CityName;
                        //DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, "Nazwa distr 0 = " + districtName);
                    }
                    else
                        districtName = null;
                }
                else
                    districtName = districtManager.GetDistrictName(i);
                if (d.IsValid() && d.IsAlive() && districtName != null)
                    districtNames[i] = districtName.Replace("\"", string.Empty);
                i++;
            }
        }
        public static void ConstructEmptyShowPairsList(this Dictionary<int, string> namesList, out Dictionary<int, bool> constructedDict)
        {
            constructedDict = new Dictionary<int, bool>();
            foreach (KeyValuePair<int, string> kvp in namesList)
            {
                constructedDict[kvp.Key] = true;
            }
        }

        public static void HideDistrict(this int district)
        {
            var mBuildings = BuildingManager.instance.m_buildings;
            for (ushort index = 0; index < mBuildings.m_size; index++)
            {
                var building = mBuildings.m_buffer[index];
                if (building.m_flags == Building.Flags.None)
                {
                    continue;
                }
                var d = (int)DistrictManager.instance.GetDistrict(building.m_position);
                if (d != district)
                {
                    continue;
                }
                var id = index;

                building.m_flags = Building.Flags.Hidden;
                //  DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, BuildingManager.instance.GetComponents<MeshRenderer>().Count() + " mesh rendereres !");
            }
        }
        /*
        public static DistrictDataContainer[] GetDistrictDataContainerArray(this Dictionary<int, bool> showDistricts)
        {
            var _list = new List<DistrictDataContainer>();
            foreach (KeyValuePair<int, bool> kvp in showDistricts)
                _list.Add(new DistrictDataContainer(kvp.Key, kvp.Value));
            return _list.ToArray();
        } */
    }
}
