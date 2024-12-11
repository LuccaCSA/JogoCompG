using System;
using UnityEngine;

namespace MagicPigGames
{
    public class BarraVidaBoss : MonoBehaviour
    {
        [Header("Configurações da Barra de Vida do Boss")]
        [Range(0f, 1f)]
        public float progress = 0f; // Progresso atual da barra (0.0 = cheia, 1.0 = vazia)
        private float _ultimoProgress = 0f;
        private ProgressBar _progressBar;

        void Start()
        {
            // Inicializa a barra de vida do boss como cheia
            progress = 0f;
            _ultimoProgress = 0f;

            // Obtém a referência ao componente ProgressBar no GameObject
            if (_progressBar == null)
                _progressBar = GetComponent<ProgressBar>();

            if (_progressBar != null)
                _progressBar.SetProgress(progress);
        }

        protected virtual void Update()
        {
            // Atualiza a barra de vida se houver alteração no progresso
            if (Math.Abs(_ultimoProgress - progress) > 0.001f)
            {
                _ultimoProgress = progress;
                AtualizarBarra();
            }
        }

        private void OnValidate()
        {
            // Garante que a barra seja configurada corretamente no editor
            if (_progressBar == null)
                _progressBar = GetComponent<ProgressBar>();

            if (_progressBar != null)
            {
                _progressBar.SetProgress(progress);
            }
        }

        public void AtualizarBarraDeVida(float porcentagem)
        {
            // Atualiza o progresso da barra com base na porcentagem fornecida
            progress = Mathf.Clamp(porcentagem, 0f, 1f);
        }

        private void AtualizarBarra()
        {
            // Aplica o progresso atualizado na barra de progresso
            if (_progressBar != null)
            {
                _progressBar.SetProgress(progress);
            }
        }
    }
}
