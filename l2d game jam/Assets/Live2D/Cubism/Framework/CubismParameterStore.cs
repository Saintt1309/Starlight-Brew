using Live2D.Cubism.Core;
using UnityEngine;

namespace Live2D.Cubism.Framework
{
    /// <summary>
    /// Cubism parameter store, modified to allow runtime parameter changes.
    /// </summary>
    public class CubismParameterStore : MonoBehaviour, ICubismUpdatable
    {
        private CubismParameter[] DestinationParameters { get; set; }
        private CubismPart[] DestinationParts { get; set; }

        private float[] _parameterValues;
        private float[] _partOpacities;

        [HideInInspector]
        public bool HasUpdateController { get; set; }

        /// <summary>
        /// Allow disabling save/restore logic manually.
        /// </summary>
        [Tooltip("If true, this script will NOT overwrite runtime parameter changes.")]
        public bool DisableAutoSaveRestore = true;


        public int ExecutionOrder => CubismUpdateExecutionOrder.CubismParameterStoreSaveParameters;

        public bool NeedsUpdateOnEditing => false;

        public void Refresh()
        {
            var model = this.FindCubismModel();
            if (model == null)
            {
                Debug.LogWarning("[CubismParameterStore] No CubismModel found.");
                return;
            }

            DestinationParameters ??= model.Parameters;
            DestinationParts ??= model.Parts;

            HasUpdateController = (GetComponent<CubismUpdateController>() != null);

            if (!DisableAutoSaveRestore)
            {
                SaveParameters();
            }
        }

        public void OnLateUpdate()
        {
            if (!HasUpdateController || DisableAutoSaveRestore)
            {
                return;
            }

            SaveParameters();
        }

        public void SaveParameters()
        {
            if (!enabled) return;

            if (DestinationParameters != null && _parameterValues == null)
            {
                _parameterValues = new float[DestinationParameters.Length];
            }

            if (_parameterValues != null)
            {
                for (var i = 0; i < _parameterValues.Length; ++i)
                {
                    _parameterValues[i] = DestinationParameters[i].Value;
                }
            }

            if (DestinationParts != null && _partOpacities == null)
            {
                _partOpacities = new float[DestinationParts.Length];
            }

            if (_partOpacities != null)
            {
                for (var i = 0; i < _partOpacities.Length; ++i)
                {
                    _partOpacities[i] = DestinationParts[i].Opacity;
                }
            }
        }

        public void RestoreParameters()
        {
            if (!enabled || DisableAutoSaveRestore)
            {
                return;
            }

            if (_parameterValues != null)
            {
                for (var i = 0; i < _parameterValues.Length; ++i)
                {
                    DestinationParameters[i].OverrideValue(_parameterValues[i]);
                }
            }

            if (_partOpacities != null)
            {
                for (var i = 0; i < _partOpacities.Length; ++i)
                {
                    DestinationParts[i].Opacity = _partOpacities[i];
                }
            }
        }

        private void OnEnable()
        {
            Refresh();
        }
    }
}
