using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
  [SerializeField] private Transform[] m_WayPoints;
  [SerializeField] private Color m_color;
  [SerializeField] private bool m_plot;
  public Transform[] WayPoints => m_WayPoints;
 
  private void OnDrawGizmos() 
  {
    if(!m_plot)
        return;
    Gizmos.color= m_color;
    if(m_WayPoints!= null&& m_WayPoints.Length>1)
    {
        for (int i =0; i< m_WayPoints.Length-1; i++ )
        {
            Transform from = m_WayPoints[i];
            Transform to = m_WayPoints[i+1];
            Gizmos.DrawLine(from.position, to.position);
        }
    }
    Gizmos.DrawLine(m_WayPoints[m_WayPoints.Length-1].position,m_WayPoints[0].position );

  }
}
