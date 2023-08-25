using DG.Tweening;
using ShatterShapes.Game;
using ShatterShapes.Game.Level;
using ShatterShapes.Player;
using UnityEngine;

namespace ShatterShapes.UI.Camera
{
    public class CameraTransitionController
    {
        private Transform _cameraTransform;
        
        private float _transitionTime = 2;

        public CameraTransitionController(Transform cameraTransform)
        {
            _cameraTransform = cameraTransform;
            LevelEventsHandler.NewStageGenerated += OnNewStageGenerated;
        }

        public void Dispose()
        {
            LevelEventsHandler.NewStageGenerated -= OnNewStageGenerated;
        }

        private void OnNewStageGenerated(Vector3 pos)
        {
            if (_cameraTransform == null) return;
            Quaternion targetRotation = Quaternion.LookRotation(pos - _cameraTransform.position);
            _cameraTransform.DORotate(targetRotation.eulerAngles, _transitionTime).SetEase(Ease.OutSine)
                .OnComplete(() => GameStateController.SetGameState(GameState.Playing));
        }
    }
}