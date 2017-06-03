using ICities;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using HideDistricts.ModdingSkeletonCode;

namespace HideDistricts
{
    public class HideDistrictsMod : LoadingExtensionBase, IUserMod
    {
        public static GameObject gameObject;


        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            if (mode != LoadMode.NewGame && mode != LoadMode.LoadGame)
            {
                return;
            }

            if (gameObject != null)
            {
                return;
            }
            gameObject = new GameObject("HideDistricts_ManagerObject");
            gameObject.AddComponent<HideDistrictsLogic>();
        }
        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();

            if (gameObject == null)
            {
                return;
            }
            Object.Destroy(gameObject);
            gameObject = null;
        }

        public string Name
        {
            get { return "Hide Districts"; }
        }

        public string Description
        {
            get { return "[DEV VERSION] Reduces the amount of items rendered in districts to improve perfs"; }
        }
    }
}
