using tabuleiro; 

namespace xadrez
{
    class PosicaoXadrez
    {
        public char coluna { get; set; } 
        public int linha { get; set; }  


        public PosicaoXadrez(char coluna,int linha)
        {
            this.coluna = coluna;
            this.linha = linha;

        }

        public override string ToString()
        {
            return "" + coluna + linha;
        }

        public Posicao toPos(Tabuleiro tab)
        {
            Posicao p =  new Posicao(8 - linha, coluna - 'a');
            tab.validarPosicao(p);
            return p;

        }
    }
}
