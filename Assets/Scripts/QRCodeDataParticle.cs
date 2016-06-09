using System.Linq;
using UnityEngine;
using ZXing;
using Object = UnityEngine.Object;

namespace Assets.Scripts {
    class QRCodeDataParticle  : QrCodeData {
        private readonly Color _startColor;
        private readonly Color _endColor;

        public QRCodeDataParticle(ResultPoint[] resultPoints, int id, DataType type) : base(resultPoints, id, type)
        {
            var actParticle = GlobalState.Instance.AllParticles.particles.First(x => x.id == id);
            _startColor = actParticle.StartColor;
            _endColor = actParticle.EndColor;
        }

        public override void CreateModel(Transform parent)
        {
            GameObject model = Object.Instantiate(Resources.Load("ParticleSystem")) as GameObject;

            if (model != null)
            {
                var colorOverLifetimeModule = model.GetComponent<ParticleSystem>().colorOverLifetime;
                var gradient = new Gradient();
                var gradientColorKeys = new GradientColorKey[]
                {new GradientColorKey(_startColor, 0f), new GradientColorKey(_endColor, 1f)};
                var gradientAlphaKeys = new GradientAlphaKey[]
                {new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 0.8f), new GradientAlphaKey(0f, 1f)};
                // only fade out in the last 20% of lifetime

                gradient.SetKeys(gradientColorKeys, gradientAlphaKeys);
                colorOverLifetimeModule.color = new ParticleSystem.MinMaxGradient(gradient);

                model.transform.SetParent(parent);
                model.transform.localPosition = LocalCoinPosition();
                SetModel(model);
            }
        }
    }
}
