namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao posicao { get; set; } 
        public Cor cor { get; protected set; } 
        public int qteMovimentos { get; protected set; } 
        public Tabuleiro tab { get; protected set; } 

        public Peca(Tabuleiro tab, Cor cor)
        {
            posicao = null;
            this.cor = cor;
            this.tab = tab;
            qteMovimentos = 0;
        } 

        public bool mvPossiveis()
        {
            bool[,] mat = movimentosPossiveis(); 
            for (int i = 0; i<tab.linhas; i++)
            {
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool mvCheck(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor;
        }

        public bool podeMoverPara(Posicao pos)
        {
            return movimentosPossiveis()[pos.linha, pos.coluna];
        }

        public void plusMovimentos()
        {
            qteMovimentos++;
        }

        public void minusMovimentos()
        {
            qteMovimentos--;
        }

        public abstract bool[,] movimentosPossiveis();
    }
}
