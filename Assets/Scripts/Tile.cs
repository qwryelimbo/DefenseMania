using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject _turretPreview;
    [SerializeField] private GameObject _turretPreviewRed;
    [SerializeField] private GameObject _turret;

    private GameObject _crTurretPreview;
    private bool _build;
    private bool _spawned;
    private bool _used;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            _build = !_build;

            if(_crTurretPreview != null)
            {
                Destroy(_crTurretPreview);
            }

            _used = false;
        }
    }

    private void OnMouseDown()
    {
        if(_build && !_spawned && GameM.instance._gold >= GameM.instance._turretCost)
        {
            _spawned = true;
            _used = false;
            GameM.instance._gold -= GameM.instance._turretCost;
            GameM.instance.UpdateGold();
            Instantiate(_turret, transform.position, Quaternion.identity);
            Destroy(_crTurretPreview);
        }
    }

    private void OnMouseExit()
    {
        if(_crTurretPreview != null)
        {
            Destroy(_crTurretPreview);
        }

        _used = false;
    }

    private void OnMouseOver()
    {
        if(_crTurretPreview == null && _build && !_spawned)
        {
            if (GameM.instance._gold >= GameM.instance._turretCost)
            {
                _crTurretPreview = Instantiate(_turretPreview, transform.position, Quaternion.identity);
            }
            else
            {
                _crTurretPreview = Instantiate(_turretPreviewRed, transform.position, Quaternion.identity);
            }
        }

        if(_crTurretPreview != null && _build && !_spawned && !_used && GameM.instance._gold >= GameM.instance._turretCost)
        {
            _used = true;
            Destroy(_crTurretPreview);
            _crTurretPreview = Instantiate(_turretPreview, transform.position, Quaternion.identity);
        }
    }
}