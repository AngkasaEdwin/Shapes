﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Enemy {

    public Transform target;//TODO: let ShooterAI assign the target
    public float fireRate = 0.5f;
    public float bulletDamage = 30f;
    public LayerMask hitLayer;

    float timeToFire = 0;
    Transform firePoint;
    public Transform BulletTrailPrefab;

    void Awake() {
        firePoint = transform.FindChild("FirePoint");
        if (firePoint == null) {
            Debug.LogError("Firepoint is null");
        }
    }
	
	// Update is called once per frame
	void Update () {
        //TODO: Check if target is within detection radius
        //Laser, basically
        if (fireRate == 0) {
            Shoot();
        }
        //Single bullets
        else {
            if (Time.time > timeToFire) {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
	}

    void Shoot() {
        Vector2 targetPos = new Vector2(target.position.x, target.position.y);
        Vector2 firePointPos = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPos, targetPos - firePointPos, 100, hitLayer);
        BulletEffect();
    }

    void BulletEffect() {
        Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
    }
}