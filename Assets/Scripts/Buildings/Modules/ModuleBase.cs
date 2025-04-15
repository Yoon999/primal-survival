using System.Collections;
using UnityEngine;

public class ModuleBase : MonoBehaviour
{
   public float cost;
   public float interval = 1.0f;
   public float duration;

   private ResourceConsumer resourceConsumer;
   private bool isTicking;

   private void Start()
   {
      // TODO: REMOVE THIS
      ActivateModule();
   }

   public void ActivateModule()
   {
      resourceConsumer = gameObject.AddComponent<ResourceConsumer>();
      resourceConsumer.resourceType = ResourceManager.ResourceType.Fuel;
      resourceConsumer.consumeAmount = 0;
      
      Debug.Log("Module activated");
      StartCoroutine(TickModule());
   }
   
   private IEnumerator TickModule()
   {
      Debug.Log("TickModule Called");
      isTicking = true;
      while (isTicking)
      {
         Debug.Log("Module ticked");
         yield return new WaitForSeconds(interval);

         var success = resourceConsumer.ConsumeOnce();

         if (success)
         {
            OnTick();
            continue;
         }

         StopModule();
      }
   }

   public void StopModule()
   {
      isTicking = false;
   }

   public virtual void OnTick()
   {
      
   }
}

