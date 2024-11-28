using System;
using UnityEngine;

namespace MagicPigGames
{
    public class ControleBarraOuro : MonoBehaviour
    {
        [Header("Configurações da Barra de Ouro")]
        public float valorIncremento = 0.01f; // Valor de incremento por barco ativo
        [Range(0f, 1f)] 
        public float progress = 0f; // Progresso atual da barra de ouro (começa cheia em 0)

        private float _ultimoProgress = 0f;
        private ProgressBar _progressBar;
        private int barcosAtivos = 1; // Contador de barcos ativos
        private bool incrementoAtivo = false; // Controle de incremento contínuo

        void Awake()
        {
            // Inicializa o progresso da barra como cheia em 0 no Awake
            progress = 0f;
            _ultimoProgress = 0f;

            // Obtém a referência do componente ProgressBar no objeto durante o Awake
            if (_progressBar == null)
                _progressBar = GetComponent<ProgressBar>();

            // Garante que a barra comece cheia
            if (_progressBar != null)
            {
                _progressBar.SetProgress(progress); // Inicializa a barra cheia
                //Debug.Log("Barra inicializada com progresso: " + progress);
            }
        }

        void Start()
        {
            // Reafirma a inicialização no Start para garantir que o valor seja corretamente aplicado
            if (_progressBar != null)
            {
                _progressBar.SetProgress(progress); // Certifica que a barra ainda está cheia no início
            }
        }

        protected virtual void Update()
        {
            // Incrementa a barra continuamente se houver barcos ativos
            if (incrementoAtivo && barcosAtivos > 0)
            {
               // Debug.Log("Incrementando a barra... Barcos ativos: " + barcosAtivos);
                IncrementarBarra();
            }

            // Atualiza o progresso da barra se houver alteração
            if (Math.Abs(_ultimoProgress - progress) > 0.001f)
            {
                _ultimoProgress = progress;
                AtualizarBarraDeProgresso();
            }
        }

        private void OnValidate()
        {
            // Obtém a referência do componente ProgressBar no objeto
            if (_progressBar == null)
                _progressBar = GetComponent<ProgressBar>();

            // Garante que a barra comece corretamente no Editor, mesmo ao ajustar valores no Inspector
            if (_progressBar != null)
            {
                _progressBar.SetProgress(progress);
            }
        }

        public void AtualizarBarcosAtivos(int quantidade)
        {
            // Atualiza o número de barcos ativos
            barcosAtivos = Mathf.Max(0, barcosAtivos + quantidade);

            // Ativa ou desativa o incremento conforme o número de barcos ativos
            incrementoAtivo = barcosAtivos > 0;

           // Debug.Log("Número de barcos ativos atualizado para: " + barcosAtivos + ", incremento ativo: " + incrementoAtivo);

            // Atualiza a barra imediatamente após ajustar o número de barcos ativos
            AtualizarBarraDeProgresso();
        }

        private void IncrementarBarra()
        {
            // Incrementa a barra proporcionalmente ao número de barcos ativos
            float incremento = valorIncremento * barcosAtivos * Time.deltaTime;
            progress = Mathf.Clamp(progress + incremento, 0f, 1f);

            // Debug.Log("Barra incrementada: " + progress);

            // Verifica se o progresso atingiu o máximo e termina o jogo
            if (Mathf.Approximately(progress, 1f))
            {
               // Debug.Log("Ouro esgotado! Fim do jogo.");
                // Adicione a lógica de fim de jogo aqui
            }
        }

        private void AtualizarBarraDeProgresso()
        {
            // Verifica se a barra de progresso foi encontrada e atualiza o progresso
            if (_progressBar != null)
            {
                _progressBar.SetProgress(progress);
                //Debug.Log("Progresso da Barra de Ouro atualizado para: " + progress);
            }
        }
    }
}
