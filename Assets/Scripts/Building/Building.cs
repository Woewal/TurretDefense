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

        public Energy energy;
        public Health health;

        public Action OnComponentUpdate;

        [SerializeField] RectTransform userInterface;

        [SerializeField] Collider buildingCollider = null;

        /// <summary>
        /// Initializes the building
        /// </summary>
        private void Start()
        {
            energy = GetComponent<Energy>();
            health = GetComponent<Health>();
            health.ZeroHealth += DestroyBuilding;
        }
        
        /// <summary>
        /// Adds the component to the building
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent(BuildingComponent component)
        {
            if (components.Count <= maxComponents)
            {
                BuildingComponent newComponent = Instantiate(component, transform);
                newComponent.transform.position = new Vector3(newComponent.transform.position.x, components.Count * BuildingComponent.size + baseHeight, newComponent.transform.position.z);
                newComponent.building = this;
                components.Add(newComponent);

                ChangeUIHeight(components.Count);

                newComponent.Initialize();

                if(OnComponentUpdate != null)
                    OnComponentUpdate();
            }
        }

        /// <summary>
        /// Adds a list of components to the building
        /// </summary>
        /// <param name="newComponents"></param>
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

        /// <summary>
        /// Changes whether the building is colliding
        /// </summary>
        /// <param name="on"></param>
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

        /// <summary>
        /// Enables the player to pick up the building
        /// </summary>
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

        /// <summary>
        /// Disables collision with the building
        /// </summary>
        public override void DisableCollision()
        {
            SetColliding(false);
        }

        /// <summary>
        /// Changes the position of the UI based on how many components the building has
        /// </summary>
        /// <param name="amountOfComponents"></param>
        private void ChangeUIHeight(int amountOfComponents)
        {
            userInterface.transform.localPosition = Vector3.up * amountOfComponents * BuildingComponent.size + Vector3.up * Building.baseHeight + Vector3.up * 0.3f;
        }

        /// <summary>
        /// Destroys the building
        /// </summary>
        void DestroyBuilding()
        {
            Destroy(gameObject);
        }
    }
}

