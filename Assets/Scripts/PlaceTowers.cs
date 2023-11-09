using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTowers : MonoBehaviour
{
    [SerializeField] GameObject[] Towers;
    [SerializeField] LayerMask PlaceableGroundLayer;
    [SerializeField] LayerMask TowerOverlap;
    [SerializeField] float MaxReach;
    [SerializeField] Vector3 TowerOffset;
    [SerializeField] Vector3 OverlapRadius;
    [SerializeField] Vector3 OverlapRadiusOffset;

    int TowerSelected;
    bool Collision = false;

    private void Update()
    {
        MousePositionToPosition();

        if (Input.GetMouseButtonDown(0) && Collision == false)
        {
            PlaceTower(TowerSelected);
        }
    }

    private void MousePositionToPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycasthit, MaxReach, PlaceableGroundLayer))
        {
            transform.position = raycasthit.point;
        }
    }

    void PlaceTower(int TowerInList)
    {
        Instantiate(Towers[TowerInList], transform.position + TowerOffset, Quaternion.identity);
    }

    public void SelectTower(int towerSelected)
    {
        TowerSelected = towerSelected;
    }

    void MyCollisions()
    {
        if (Physics.OverlapBox(transform.position, OverlapRadius, Quaternion.identity/*, TowerOverlap*/) != null)
        {
            Collision = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position + OverlapRadiusOffset, OverlapRadius);
    }
}
