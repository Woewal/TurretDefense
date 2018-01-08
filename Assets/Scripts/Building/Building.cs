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
        public static float baseHeight = 0.7f;
        public static int maxComponents = 3;
        public List<BuildingComponent> components = new List<BuildingComponent>();

        public List<Enemy> targetedEnemies = new List<Enemy>();
        public bool isTargeting = false;

        public event Action EnableTargeting;
        public event Action DisableTargeting;
        public event Action UpdateTargeting;

        public Particle DustEmitter;

        public bool on = true;

        private void Start()
        {
            UpdateTargeting += CheckTargets;
        }

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
                newComponent.transform.position = new Vector3(newComponent.transform.position.x, components.Count * BuildingComponent.size + baseHeight, newComponent.transform.position.z);
                newComponent.building = this;
                components.Add(newComponent);
            }
        }

        public void AddComponents(List<BuildingComponent> newComponents)
        {
            if (components.Count + newComponents.Count <= Building.maxComponents)
            {
                foreach(BuildingComponent component in newComponents)
                {
                    AddComponent(component);
                }
            }
            else
            {
                Debug.LogError("Too many components");
            }
        }

        public override void Interact(Player player)
        {
            player.buildingPlacer.InitiateBuilding(this);
            Destroy(this.gameObject);
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
            else if (targetedEnemies.Count == 0 && isTargeting)
            {
                if (DisableTargeting != null)
                {
                    DisableTargeting();
                    isTargeting = false;
                }
            }
        }

        public void SortEnemies()
        {
            targetedEnemies = targetedEnemies.OrderBy(
                x => Vector3.Distance(transform.position, x.transform.position)
            ).ToList();
        }
    }
}

