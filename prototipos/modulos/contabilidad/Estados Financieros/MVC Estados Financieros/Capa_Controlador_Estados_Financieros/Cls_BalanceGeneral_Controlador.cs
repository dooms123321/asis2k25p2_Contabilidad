// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   30/10/2025

using System.Data;
using Capa_Modelo_Estados_Financieros;

namespace Capa_Controlador_Estados_Financieros
{
    public class Cls_BalanceGeneral_Controlador
    {
        private readonly Cls_BalanceGeneral_Calculo gCalculo = new Cls_BalanceGeneral_Calculo();

        // Método para obtener el balance general completo
        public DataTable fun_obtener_balance_general()
        {
            return gCalculo.fun_calcular_balance_general();
        }
    }
}
