using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Game.Building
{
    [System.Serializable]
    public class Building : Pickup
    {
        public static float baseHeight = 0.7f;
        public static int maxComponents = 3;
        public List<BuildingComponent> components = new List<BuildingComponent>();

        public List<Enemy> targetedEnemies = new List<Enemy>();
        public bool isTargeting = false;

        public Particle DustEmitter;

        public Energy energy;

        [SerializeField] RectTransform userInterface;

        [SerializeField] Collider buildingCollider = null;

        private void Start()
        {
            energy = GetComponent<Energy>();
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

                ChangeUIHeight(components.Count);
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

        public void UpdateTargets()
        {
            targetedEnemies.RemoveAll(item => item == null);
        }

        public void SortEnemies()
        {
            targetedEnemies = targetedEnemies.OrderBy(
                x => Vector3.Distance(transform.position, x.transform.position)
            ).ToList();
        }

        public void SetColliding(bool on)
        {
            if (on)
            {
                buildingCollider.enabled = true;
            }
            else
            {
                buildingCollider.enabled = false;
            }
        }

        public override void Use()
        {
            if (player.interactionController.interactables.OfType<Building>().Any())
            {
                Building newBuilding = player.interactionController.interactables.OfType<Building>().First();
                newBuilding.AddComponents(components);
                Destroy(gameObject);
                Reset();
            }
            else if (player.indicator.CanPlace)
            {
                SetColliding(true);

                Drop();

                Reset();
            }
        }

        public override void DisableCollision()
        {
            SetColliding(false);
        }

        private void ChangeUIHeight(int amountOfComponents)
        {
            userInterface.transform.localPosition = Vector3.up * amountOfComponents * BuildingComponent.size + Vector3.up * Building.baseHeight + Vector3.up * 0.3f;
        }
    }
}

