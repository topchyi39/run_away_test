using Cinemachine;
using UnityEngine;

namespace Scripts.Player
{
    public class CameraSystem
    {
        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        
        public CameraSystem(CinemachineVirtualCamera cinemachineVirtualCamera)
        {
            _cinemachineVirtualCamera = cinemachineVirtualCamera;
        }
        
        public void SetFollowObject(Transform followObject)
        {
            _cinemachineVirtualCamera.Follow = followObject;
        }
    }
}