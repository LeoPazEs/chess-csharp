﻿using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tab,Cor cor) : base(tab, cor)
        {

        }
        public override string ToString()
        {
            return "R";
        } 

       

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            //cima
            pos.definirValores(posicao.linha - 1, posicao.coluna); 
            if (tab.posicaoValida(pos) && mvCheck(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            
            // ne
            pos.definirValores(posicao.linha -1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && mvCheck(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            //direita
            pos.definirValores(posicao.linha, posicao.coluna +1);
            if (tab.posicaoValida(pos) && mvCheck(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            //se
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && mvCheck(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            //s0
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && mvCheck(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            //abaixo
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            if (tab.posicaoValida(pos) && mvCheck(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            //esquerda
            pos.definirValores(posicao.linha, posicao.coluna -1);
            if (tab.posicaoValida(pos) && mvCheck(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            //no 
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && mvCheck(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            return mat;
        }
    }
}
