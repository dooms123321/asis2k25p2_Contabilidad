// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   30/10/2025

using System.Data;
using Capa_Modelo_Estados_Financieros;

namespace Capa_Controlador_Estados_Financieros
{
    public class Cls_FlujoDeEfectivo_Controlador
    {
        private readonly Cls_FlujoDeEfectivo_Calculo gCalculo = new Cls_FlujoDeEfectivo_Calculo();

        // Método para obtener el flujo de efectivo ya calculado y listo para mostrar en el DataGridView
        public DataTable fun_obtener_flujo_efectivo()
        {
            return gCalculo.fun_calcular_flujo_efectivo();
        }
    }
}
