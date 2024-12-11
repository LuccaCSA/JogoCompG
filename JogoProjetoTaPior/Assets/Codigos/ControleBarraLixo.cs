using System;
using UnityEngine;

namespace MagicPigGames
{
    public class ControleBarraLixo : MonoBehaviour
    {
        [Header("Configurações da Barra de Poluição")]
        public float valorIncrementoPorLixo = 0.05f; // Valor de incremento por objeto com tag "Lixo" por segundo
        [Range(0f, 1f)]
        public float progress = 0f; // Progresso atual da barra de poluição

        private int numeroDeLixos = 0; // Número de lixos atualmente detectados
        private float _ultimoProgress = 0f;
        private ProgressBar _progressBar;

        void Start()
        {
            // Inicializa o progresso da barra como zero
            progress = 0f;
            _ultimoProgress = 0f;

            // Configura a barra de progresso inicial para zero
            if (_progressBar == null)
                _progressBar = GetComponent<ProgressBar>();

            if (_progressBar != null)
                _progressBar.SetProgress(progress);
        }

        protected virtual void Update()
        {
            // Incrementa a barra proporcionalmente ao número de lixos presentes
            if (numeroDeLixos > 0)
            {
                IncrementarBarra(numeroDeLixos);
            }

            // Atualiza o progresso da barra se houver alteração
            if (Math.Abs(_ultimoProgress - progress) > 0.001f)
            {
                _ultimoProgress = progress;
                _progressBar.SetProgress(progress);
            }
        }

        public void AtualizarNumeroDeLixos(int quantidade)
        {
            numeroDeLixos = quantidade;
        }

        private void IncrementarBarra(int quantidadeDeLixos)
        {
            // Incrementa a barra proporcionalmente ao número de lixos presentes
            float incremento = valorIncrementoPorLixo * quantidadeDeLixos * Time.deltaTime;
            progress = Mathf.Clamp(progress + incremento, 0f, 1f);


            // Verifica se o progresso atingiu o máximo e termina o jogo
            if (Mathf.Approximately(progress, 1f))
            {
                Debug.Log("Poluição máxima atingida! Fim do jogo.");
                // Adicione sua lógica de fim de jogo aqui
            }
        }
    }
}
