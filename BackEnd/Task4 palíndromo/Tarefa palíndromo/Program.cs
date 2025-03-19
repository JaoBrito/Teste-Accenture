using System;

class Program
{
    static bool VerificarPalindromo(string valor)
    {
        return VerificarPalindromoRecursivo(valor, 0, valor.Length - 1);
    }

    static bool VerificarPalindromoRecursivo(string valor, int esquerda, int direita)
    {
        // Caso base: string com 0 ou 1 caractere é um palíndromo
        if (esquerda >= direita)
            return true;

        // Verifica se os caracteres nas extremidades são iguais
        if (valor[esquerda] != valor[direita])
            return false;

        // Chama recursivamente para a substring interna
        return VerificarPalindromoRecursivo(valor, esquerda + 1, direita - 1);
    }

    static void Main(string[] args)
    {
        string palavra = "Rotor";  // Exemplo de palavra a ser testada
        palavra = palavra.ToUpper(); // Padronizando para evitar que a variação da letra nao interfira
        bool ehPalindromo = VerificarPalindromo(palavra);
        Console.WriteLine($"Termo dado: {palavra}\nResultado: {ehPalindromo}");
    }
}