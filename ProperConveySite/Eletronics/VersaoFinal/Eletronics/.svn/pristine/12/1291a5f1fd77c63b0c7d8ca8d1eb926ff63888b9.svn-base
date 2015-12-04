namespace Eletronics.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    /// <summary>
    /// A class that contains the common validations to be used through layers of the project
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// Validate a client CPF
        /// </summary>
        /// <param name="cpf">A CPF to be validated</param>
        /// <returns>returns if a CPF passed in parameter is valid</returns>
        public static bool IsCPF(string cpf)
        {
            string valor = cpf.Replace(".", string.Empty);
            valor = valor.Replace("-", string.Empty);
            if (valor.Length != 11)
            {
                return false;
            }

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
            {
                if (valor[i] != valor[0])
                {
                    igual = false;
                }
            }

            if (igual || valor == "12345678909")
            {
                return false;
            }

            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)
            {
                numeros[i] = int.Parse(valor[i].ToString());
            }

            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += (10 - i) * numeros[i];
            }

            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                {
                    return false;
                }
            }
            else
            {
                if (numeros[9] != 11 - resultado)
                {
                    return false;
                }
            }

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += (11 - i) * numeros[i];
            }

            resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                {
                    return false;
                }
            }
            else
                if (numeros[10] != 11 - resultado)
                {
                    return false;
                }

            return true;
        }
        /// <summary>
        /// Validate a client name, checking if the characters that compose the name are only letters
        /// </summary>
        /// <param name="name">A name to be validated</param>
        /// <returns>returns if a name passed in parameter is valid</returns>
        public static bool IsValidName(string name)
        {
            if (name.Length > 200)
            {
                return false;
            }

            string nameValidator = @"^([\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\
            éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\
            ùÙ\\çÇaA-zZ]+)+((\s[\'\.\^\~\´\`\\áÁ\\àÀ
            \\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\
            õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+)?$";

            var match = Regex.Match(name, nameValidator);
            return match.Success;
        }

        /// <summary>
        /// Verify if the CNPJ is valid
        /// </summary>
        /// <param name="clientCNPJ">the cpf of the client</param>
        /// <returns>if the CNPJ is valid</returns>
        public static bool IsCNPJ(string clientCNPJ)
        {
            string cnpj = clientCNPJ.Replace(".", string.Empty);
            cnpj = cnpj.Replace("/", string.Empty);
            cnpj = cnpj.Replace("-", string.Empty);
            int[] digitos, soma, resultado;
            int dig;
            string ftmt;
            bool[] cnpjOk;
            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            cnpjOk = new bool[2];
            cnpjOk[0] = false;
            cnpjOk[1] = false;
            try
            {
                for (dig = 0; dig < 14; dig++)
                {
                    digitos[dig] = int.Parse(cnpj.Substring(dig, 1));
                    if (dig <= 11)
                    {
                        soma[0] += digitos[dig] * int.Parse(ftmt.Substring(dig + 1, 1));
                    }

                    if (dig <= 12)
                    {
                        soma[1] += digitos[dig] * int.Parse(ftmt.Substring(dig, 1));
                    }
                }

                for (dig = 0; dig < 2; dig++)
                {
                    resultado[dig] = soma[dig] % 11;

                    if ((resultado[dig] == 0) || (resultado[dig] == 1))
                    {
                        cnpjOk[dig] = digitos[12 + dig] == 0;
                    }
                    else
                    {
                        cnpjOk[dig] = digitos[12 + dig] == (11 - resultado[dig]);
                    }
                }

                return cnpjOk[0] && cnpjOk[1];
            }
            catch
            {
                return false;
            }
        }
    }
}
