using System;
using System.Collections.Generic;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            for (int i=0; i< tab.linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j< tab.colunas; j++) 
                {             
                    Tela.imprimirPeca(tab.peca(i, j));      
                }
                Console.WriteLine(); 
            }
            Console.WriteLine();
            Console.WriteLine("  A B C D E F G H"); 
        }
        public static void imprimirTabuleiro(Tabuleiro tab,bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray; 

            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado; 
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    Tela.imprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
            Console.BackgroundColor = fundoOriginal;
        }

        public static void imprimirPartida(PartidaDeXadrez partida)
        {
            imprimirTabuleiro(partida.tab);
            Console.WriteLine();
            imprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.turno);
            if (!partida.terminada)
            {
                Console.WriteLine("Peça : " + partida.jogadorAtual); 
                if (partida.xeque)
                {
                    Console.WriteLine("Xeque!");
                }
            }
            else
            {
                Console.WriteLine("XEQUE MATE!");
                Console.WriteLine("VENCEDOR:" + partida.jogadorAtual);
             
            }
        } 

        public static void imprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            imprimirConjunto(partida.pecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
        }

        public static void imprimirConjunto( HashSet<Peca> conjunto)
        {
            Console.Write("["); 
            foreach (Peca x in conjunto)
            {
                Console.Write(x + " "); 
            }
            Console.Write("]");
        }



        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine(); 
            try
            {
                char linha = s[0];
                int coluna = int.Parse(s[1] + "");
                return new PosicaoXadrez(linha, coluna);
            }
            catch
            {
                throw new TabuleiroException("Posição Inválida!");
            }
        }

        public static void imprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;

                }
                Console.Write(" ");
            }
        }
    }
}
