using UnityEngine;
using UnityEngine.Serialization;

namespace Cinemachine
{
    public class AmbientZoneCounterClockwise : MonoBehaviour
    {
        [Tooltip("Cinemachine Path to follow")]
        public CinemachinePathBase m_Path;
        [Tooltip("Character to track")]
        public GameObject Player;

        float m_Position;
        private CinemachinePathBase.PositionUnits m_PositionUnits = CinemachinePathBase.PositionUnits.PathUnits;

        void Update()
        {
            SetCartPosition(m_Path.FindClosestPoint(Player.transform.position, 0, -1, 10));
            Vector3 Sub = transform.position - Player.transform.position;
            Vector3 Spline = transform.right;
            if (Vector3.Dot(Sub, Spline) > 0)
            {
                transform.position = Player.transform.position;
                transform.rotation = Player.transform.rotation;
            }
        }

        void SetCartPosition(float distanceAlongPath)
        {
            m_Position = m_Path.StandardizeUnit(distanceAlongPath, m_PositionUnits);
            transform.position = m_Path.EvaluatePositionAtUnit(m_Position, m_PositionUnits);
            transform.rotation = m_Path.EvaluateOrientationAtUnit(m_Position, m_PositionUnits);
        }
    }
}
