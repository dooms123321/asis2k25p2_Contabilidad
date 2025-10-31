// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   29/10/2025

using System;
using System.Data;
using Capa_Modelo_Estados_Financieros;

namespace Capa_Controlador_Estados_Financieros
{
    public class Cls_BalanceDeSaldos_Controlador
    {
        private readonly Cls_BalanceDeSaldos_Calculo gCalculo = new Cls_BalanceDeSaldos_Calculo();

        // Método público que obtiene el DataTable ya calculado
        public DataTable fun_obtener_balance_saldos()
        {
            DataTable dts_Balance = new DataTable();

            try
            {
                dts_Balance = gCalculo.fun_calcular_balance_saldos();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el balance de saldos: " + ex.Message);
            }

            return dts_Balance;
        }
    }
}
