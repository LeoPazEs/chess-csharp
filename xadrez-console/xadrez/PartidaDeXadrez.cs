﻿using System.Collections.Generic;
using tabuleiro;
namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab {get; private set;}
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set;  } 
        public bool xeque { get; private set; }


        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaDeXadrez()
        {
            xeque = false;
            tab = new Tabuleiro(8, 8); 
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>(); 
            colocarPecas();
        } 

        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.plusMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p,destino); 
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
            return pecaCapturada; 

        } 

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.minusMovimentos();
            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada =executaMovimento(origem, destino); 
            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino , pecaCapturada);
                throw new TabuleiroException("Rei em Xeque!");
            }
            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
            turno++;
            mudaJogador();
        } 

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>(); 
            foreach (Peca x in capturadas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                } 
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            return Cor.Branca;
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        } 

        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não há rei no jogo");
            }
            foreach ( Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça."); 
            } 
            if (jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("Peça errada.");
            }
            if (!tab.peca(pos).mvPossiveis())
            {
                throw new TabuleiroException("Peça não possui movimentos possíveis.");
            } 
        } 

        public void validarPosicaoDeDestino(Posicao origem,Posicao destino)
        {
            if (!tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Peça não pode fazer esse movimento.");
            }
        }
        private void mudaJogador()
        {
            if(jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        public void colocarNovaPeca(char coluna, int linha,Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPos(tab));
            pecas.Add(peca);
        }

        public void colocarPecas()
        {
            colocarNovaPeca('c', 1,new Torre(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));

            colocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));
        }
    }
}