using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Game.Building
{
    [System.Serializable]
    public class Building : Interactable
    {

        public static int maxComponents = 3;
        public List<BuildingComponent> components = new List<BuildingComponent>();

        public List<Enemy> targetedEnemies = new List<Enemy>();
        public bool isTargeting = false;

        public event Action EnableTargeting;
        public event Action DisableTargeting;
        public event Action UpdateTargeting;

        public Particle DustEmitter;

        public bool on = true;

        public void BuildTurret(BuildingData data)
        {
            //Particle de = Instantiate(DustEmitter, this.transform);
            //de.transform.position = this.transform.position;

            for (int i = 0; i < data.components.Count; i++)
            {
                AddComponent(data.components[i]);
            }
        }

        public void AddComponent(BuildingComponent component)
        {
            if (components.Count <= Building.maxComponents)
            {
                BuildingComponent newComponent = Instantiate(component, transform);
                newComponent.transform.Translate(components.Count * BuildingComponent.size * Vector3.up + Vector3.up * 0.8f);
                newComponent.building = this;
                components.Add(newComponent);
            }
            else
            {
                Debug.LogError("Too many components");
            }

        }

        public override void Interact(Player player)
        {
        }

        public override void BuildingInteract(Player player)
        {
            if (player.buildingPlacer.buildingData.components.Count + components.Count <= Building.maxComponents)
            {
                foreach (BuildingComponent component in player.buildingPlacer.buildingData.components)
                {
                    AddComponent(component);
                }

                player.buildingPlacer.Reset();
            }
        }

        public void CheckTargets()
        {
            if (targetedEnemies.Count > 0 && !isTargeting)
            {
                if (EnableTargeting != null)
                {
                    EnableTargeting();
                    isTargeting = true;
                }
            }
            else
            {
                if (DisableTargeting != null)
                {
                    DisableTargeting();
                    isTargeting = false;
                }
            }
        }
    }
}

