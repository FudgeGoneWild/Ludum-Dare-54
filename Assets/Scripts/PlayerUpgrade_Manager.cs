using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade_Manager : MonoBehaviour
{
    [SerializeField] public Item_Data item_Data;

    private Player_Health_Controller healthController;
    private Player_Shooting_Controller shootingController;
    private PlayerMovement_Controller movementController;
    private BulletScript _bulletScript;
    // Start is called before the first frame update
    void Start()
    {
        healthController = GetComponent<Player_Health_Controller>();
        shootingController = GetComponent<Player_Shooting_Controller>();
        movementController = GetComponent<PlayerMovement_Controller>();
        _bulletScript = FindObjectOfType<BulletScript>();
    }

    public void UpGrade()
    {
        if (item_Data.ItemBoost == "MaxHealth")
        {
            healthController.healthBoost = (int)item_Data.amount;
            healthController.UpdateMaxHealth();
        }

        if (item_Data.ItemBoost == "MovementSpeed")
        {
            movementController.speedboost += movementController.speed * item_Data.amount;
        }

        if (item_Data.ItemBoost == "DashCoolDown")
        {
            movementController.dashCooldownBoost += movementController.setDashReady * item_Data.amount;
            movementController.UpgradeDashCooldown();
        }

        if (item_Data.ItemBoost == "DashStrengh")
        {
            movementController.dashStrenghBoost += item_Data.amount;
            movementController.UpgradeDashStrengh();
        }

        if (item_Data.ItemBoost == "Spray")
        {
            shootingController.sprayBoost += shootingController.spray * item_Data.amount;
        }

        if (item_Data.ItemBoost == "FireRate")
        {
            shootingController.FireRateBoost += shootingController.fireRate * item_Data.amount;
        }

        if (item_Data.ItemBoost == "MaxAmmo")
        {
            shootingController.maxAmmoBoost += (int)item_Data.amount;
        }
    }
}
