using System;
using UnityEngine;

namespace Artngame.SKYMASTER
{
    [ExecuteInEditMode]
    public class WaterTileSM : MonoBehaviour
    {
        public PlanarReflectionSM reflection;
        public WaterBaseSM waterBase;


        public void Start()
        {
            AcquireComponents();
        }


        void AcquireComponents()
        {
            if (!reflection)
            {
                if (transform.parent)
                {
					reflection = transform.parent.GetComponent<PlanarReflectionSM>();
                }
                else
                {
					reflection = transform.GetComponent<PlanarReflectionSM>();
                }
            }

            if (!waterBase)
            {
                if (transform.parent)
                {
					waterBase = transform.parent.GetComponent<WaterBaseSM>();
                }
                else
                {
					waterBase = transform.GetComponent<WaterBaseSM>();
                }
            }
        }


#if UNITY_EDITOR
        public void Update()
        {
            AcquireComponents();
        }
#endif


        public void OnWillRenderObject()
        {
            if (reflection)
            {
                reflection.WaterTileBeingRendered(transform, Camera.current);
            }
            if (waterBase)
            {
                waterBase.WaterTileBeingRendered(transform, Camera.current);
            }
        }
    }
}