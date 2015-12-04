using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProperConvey.ADO
{
    

    public class ADOMedicoMock
    {
        private readonly string[] Users = { "Astolfo:senha123" };
        public  bool validarLogin(string username, string senha) {
            
            for(int cont=0;cont<Users.Length; cont++)
            {
                string[] autenticacao = Users[cont].Split(':');
                if(autenticacao[0].Equals(username)&& autenticacao[1].Equals(senha))
                {
                    return true;
                }
            }

            return false;
        }
    }
}