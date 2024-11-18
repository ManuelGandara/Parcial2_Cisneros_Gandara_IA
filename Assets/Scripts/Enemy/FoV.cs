using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoV : MonoBehaviour 
{
    [SerializeField] LayerMask wallLayer;
    [SerializeField] float _viewRadius;
    [SerializeField] float _viewAngle;


    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, GetDirFromAngle(_viewAngle)* _viewRadius, Color.cyan);
        Debug.DrawRay(transform.position, GetDirFromAngle(-_viewAngle) * _viewRadius, Color.cyan);
    }

    public bool InLineOfSight(Vector3 target) //crea un rayo entre el agente y el objeto que quiere ver
    {
        Vector3 dir = target - transform.position;
        return !Physics.Raycast(transform.position, dir, dir.magnitude, wallLayer);   //No ve la pared? ... True
    }
    
    public bool InFieldOfView(Vector3 target) // V si el target esta a menos de _viewAngle/2 grados del vector transform.forward
    {
        Vector3 dir = target - transform.position;
        if (dir.sqrMagnitude > _viewRadius * _viewRadius) return false;
        if (!InLineOfSight(target)) return false; 
        return Vector3.Angle(transform.forward, dir) <= _viewAngle / 2;
    }

    public Vector3 GetDirFromAngle(float angleInDegrees) 
    {
        float angle = angleInDegrees + transform.eulerAngles.y; // a la rotacion dada se le suma la rotación del objeto
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad)); 
        // se calcula el vector resultante de la suma de rotaciones 
    }

}
