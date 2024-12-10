using System;
using UnityEngine;

namespace MagicPigGames{
    public class ControleBarraPoluicao : MonoBehaviour
    {
        [Header("Configurações da Barra de Poluição")]
        public float valorIncremento = 0.01f; // Valor de incremento por placa ativa
        [Range(0f, 1f)] 
        public float progress = 0f; // Progresso atual da barra de poluição

        private float _ultimoProgress = 0f;
        private ProgressBar _progressBar;
        private int placasAtivas = 0; // Contador de placas ativas
        private bool incrementoAtivo = false; // Controle de incremento contínuo

        //att tg
        public ChangeScene changeScene;
        //
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
            // Incrementa a barra continuamente se houver placas ativas
            if (incrementoAtivo && placasAtivas > 0)
            {
                IncrementarBarra();
            }

            // Atualiza o progresso da barra se houver alteração
            if (Math.Abs(_ultimoProgress - progress) > 0.001f)
            {
                _ultimoProgress = progress;
                _progressBar.SetProgress(progress);
            }
        }

        private void OnValidate()
        {
            // Obtém a referência do componente ProgressBar no objeto
            if (_progressBar == null)
                _progressBar = GetComponent<ProgressBar>();
        }

        public void AtivarIncremento()
        {
            placasAtivas++; // Aumenta o contador de placas ativas
            if (!incrementoAtivo) 
            {
                incrementoAtivo = true; // Ativa o incremento contínuo quando a primeira placa aparece
            }
        }

        public void DesativarIncremento()
        {
            placasAtivas = Mathf.Max(0, placasAtivas - 1); // Reduz o contador, sem ficar negativo
            if (placasAtivas == 0)
            {
                incrementoAtivo = false; // Desativa o incremento contínuo quando não há mais placas
            }
        }

        private void IncrementarBarra()
        {
            // Incrementa a barra proporcionalmente ao número de placas ativas
            progress = Mathf.Clamp(progress + (valorIncremento * placasAtivas * Time.deltaTime), 0f, 1f);

            // Verifica se o progresso atingiu o máximo e termina o jogo
            if (Mathf.Approximately(progress, 1f))
            {
               // Debug.Log("Poluição máxima atingida! Fim do jogo.");
                // Adicione a lógica de fim de jogo aqui
                changeScene.LoadScene("TelaDerrotaLixo");
            }
        }
    }
}
