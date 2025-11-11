// =====================================================================================
// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   10/11/2025
// Descripción: Clase de cálculo del Estado de Resultados (actual e histórico)
// =====================================================================================

using System;
using System.Data;
using System.Linq;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_EstadoResultados_Calculo
    {
        private readonly Cls_EstadoResultados_Dao gDao = new Cls_EstadoResultados_Dao();

        // ---------------------------------------------------------------------------------
        // Método: fun_calcular_estado_resultados
        // Calcula el estado de resultados (modo actual)
        // ---------------------------------------------------------------------------------
        public DataTable fun_calcular_estado_resultados(int iNivel)
        {
            DataTable dts_Resultados = gDao.fun_consultar_estado_resultados(3);
            if (dts_Resultados.Rows.Count == 0)
                return dts_Resultados;

            DataTable dts_Acumulado = Fun_Acumular_Saldos(dts_Resultados);
            return Fun_Filtrar_Por_Nivel(dts_Acumulado, iNivel);
        }

        // ---------------------------------------------------------------------------------
        // Método: fun_calcular_estado_resultados_historico
        // Calcula el estado de resultados (modo histórico)
        // ---------------------------------------------------------------------------------
        public DataTable fun_calcular_estado_resultados_historico(int iNivel, int iAnio, int iMes)
        {
            DataTable dts_Resultados = gDao.fun_consultar_estado_resultados_historico(3, iAnio, iMes);
            if (dts_Resultados.Rows.Count == 0)
                return dts_Resultados;

            DataTable dts_Acumulado = Fun_Acumular_Saldos(dts_Resultados);
            return Fun_Filtrar_Por_Nivel(dts_Acumulado, iNivel);
        }

        // ---------------------------------------------------------------------------------
        // Método: fun_calcular_utilidad_neta
        // Calcula la utilidad neta del ejercicio actual
        // ---------------------------------------------------------------------------------
        public decimal fun_calcular_utilidad_neta()
        {
            return gDao.fun_calcular_utilidad_neta();
        }

        // ---------------------------------------------------------------------------------
        // Método: fun_calcular_utilidad_neta_historico
        // Calcula la utilidad neta del ejercicio histórico
        // ---------------------------------------------------------------------------------
        public decimal fun_calcular_utilidad_neta_historico(int iAnio, int iMes)
        {
            return gDao.fun_calcular_utilidad_neta_historico(iAnio, iMes);
        }

        // ---------------------------------------------------------------------------------
        // Método: fun_obtener_tipo_resultado
        // Determina si el resultado es utilidad, pérdida o neutro
        // ---------------------------------------------------------------------------------
        public string fun_obtener_tipo_resultado(decimal deUtilidadNeta)
        {
            if (deUtilidadNeta > 0)
                return "UTILIDAD NETA";
            else if (deUtilidadNeta < 0)
                return "PERDIDA NETA";
            else
                return "RESULTADO NEUTRO";
        }

        // ---------------------------------------------------------------------------------
        // Método privado: Fun_Acumular_Saldos
        // Acumula los saldos de subcuentas en sus cuentas madre
        // ---------------------------------------------------------------------------------
        private DataTable Fun_Acumular_Saldos(DataTable dts_Resultados)
        {
            DataTable dts_Acumulado = dts_Resultados.Copy();

            var cuentasOrdenadas = dts_Resultados.AsEnumerable()
                .Select(r => r.Field<string>("Codigo"))
                .OrderByDescending(c => c.Split('.').Length)
                .ToList();

            foreach (string cuenta in cuentasOrdenadas)
            {
                string[] partes = cuenta.Split('.');
                for (int i = partes.Length - 1; i > 0; i--)
                {
                    string madre = string.Join(".", partes.Take(i));

                    DataRow sub = dts_Resultados.AsEnumerable()
                        .FirstOrDefault(r => r.Field<string>("Codigo") == cuenta);

                    DataRow madreFila = dts_Acumulado.AsEnumerable()
                        .FirstOrDefault(r => r.Field<string>("Codigo") == madre);

                    if (sub != null && madreFila != null)
                    {
                        decimal deSaldoSub = Convert.ToDecimal(sub["Saldo"]);
                        decimal deSaldoMadre = Convert.ToDecimal(madreFila["Saldo"]);
                        madreFila["Saldo"] = deSaldoMadre + deSaldoSub;
                    }
                }
            }

            return dts_Acumulado;
        }

        // ---------------------------------------------------------------------------------
        // Método privado: Fun_Filtrar_Por_Nivel
        // Retorna solo las cuentas que pertenecen al nivel seleccionado
        // ---------------------------------------------------------------------------------
        private DataTable Fun_Filtrar_Por_Nivel(DataTable dts_Acumulado, int iNivel)
        {
            DataTable dts_Filtrado = dts_Acumulado.Clone();
            foreach (DataRow fila in dts_Acumulado.Rows)
            {
                int nivelCuenta = fila.Field<string>("Codigo").Split('.').Length;
                if (nivelCuenta <= iNivel)
                    dts_Filtrado.ImportRow(fila);
            }
            return dts_Filtrado;
        }
    }
}

// Fin de código de Arón Ricardo Esquit Silva
// =====================================================================================
