using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUIController : MonoBehaviour
{
   public PlayerHealth playerHealth;
   public TextMeshProUGUI healthText;

   void Update()
   {
      healthText.text = "Health: " + playerHealth.health.ToString();
   }
}
