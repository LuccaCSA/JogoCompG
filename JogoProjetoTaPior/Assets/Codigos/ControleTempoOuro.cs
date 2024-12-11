using System;
using UnityEngine;

namespace MagicPigGames
{
    public class ControleTempoOuro : MonoBehaviour
    {
        [Header("Configurações da Barra de Ouro")]
        public float tempoExtracao = 10f; // Tempo total para esgotar a barra (em segundos)
        [Range(0f, 1f)]
        public float progress = 0f; // Progresso atual da barra (0.0 = cheia, 1.0 = vazia)

        private float _ultimoProgress = 0f; // Último progresso registrado
        private ProgressBar _progressBar;
        private bool extracaoAtiva = true; // Inicia a extração ativa por padrão
        private float tempoPassado = 0f; // Tempo acumulado desde o início da extração

        public ChangeScene changeScene;

        public Boss boss;
        void Start()
        {
            // Inicializa a barra de ouro como cheia (progress = 0)
            progress = 0f;
            _ultimoProgress = 0f;
            tempoPassado = 0f;

            // Obtém a referência ao componente ProgressBar no GameObject
            if (_progressBar == null)
                _progressBar = GetComponent<ProgressBar>();

            if (_progressBar != null)
                _progressBar.SetProgress(progress);

            // Inicia a extração automaticamente ao carregar a cena
           // Debug.Log("Extração de ouro iniciada automaticamente.");
        }

        protected virtual void Update()
        {
            if (extracaoAtiva)
            {
                // Incrementa o tempo passado
                tempoPassado += Time.deltaTime;

                // Calcula o progresso com base no tempo passado
                progress = Mathf.Clamp(tempoPassado / tempoExtracao, 0f, 1f);

                // Verifica se a barra chegou ao fim
                if (Mathf.Approximately(progress, 1f))
                {
                    extracaoAtiva = false; // Para a extração quando a barra atinge 1 (vazia)
                    //Debug.Log("Barra de Ouro esgotada! Fim da extração.");
                    changeScene.LoadScene("TelaDerrotaPirata");
                }
            }

            // Atualiza a barra visualmente se houver alteração
            if (Math.Abs(_ultimoProgress - progress) > 0.001f && !boss.estaAfundando)
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
