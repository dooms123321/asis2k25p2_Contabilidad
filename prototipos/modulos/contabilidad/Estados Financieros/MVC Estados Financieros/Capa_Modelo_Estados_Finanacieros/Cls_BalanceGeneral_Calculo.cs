// =====================================================================================
// Autor: Arón Ricardo Esquit Silva
// Carné: 0901-22-13036
// Fecha: 10/11/2025
// Descripción: Cálculo jerárquico del Balance General (actual e histórico)
// =====================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_BalanceGeneral_Calculo
    {
        private readonly Cls_BalanceGeneral_Dao gDao = new Cls_BalanceGeneral_Dao();

        // ---------------------------------------------------------------
        // Clase interna: estructura contable
        // ---------------------------------------------------------------
        public class Cls_CuentaContable
        {
            public string sCodigo { get; set; }
            public string sNombre { get; set; }
            public string sCodigoMadre { get; set; }
            public int iTipo { get; set; }           // 0 = agrupadora, 1 = movimiento
            public int iNaturaleza { get; set; }     // 1 = deudora, 0 = acreedora
            public decimal deSaldoActual { get; set; }
            public int iNivel => sCodigo?.Count(c => c == '.') + 1 ?? 0;
            public bool EsVirtual { get; set; } = false;
            public decimal deDebe => iNaturaleza == 1 ? deSaldoActual : 0;
            public decimal deHaber => iNaturaleza == 0 ? deSaldoActual : 0;
        }

        // ---------------------------------------------------------------
        // MODO ACTUAL
        // ---------------------------------------------------------------
        public List<Cls_CuentaContable> Fun_Obtener_Cuentas_Por_Nivel(int iNivel)
        {
            // 1. Obtener todas las cuentas con saldos acumulados
            DataTable dtsCuentas = gDao.Fun_Consultar_SaldosJerarquicos();
            List<Cls_CuentaContable> lstCuentas = Fun_Procesar_Cuentas(dtsCuentas, iNivel);

            // 2. Agregar totales (ACTIVO, PASIVO, CAPITAL)
            var (deTotalActivo, deTotalPasivo, deTotalCapital) = Fun_Obtener_Totales_Balance();
            lstCuentas.Add(Fun_Crear_Cuenta_Total(deTotalPasivo + deTotalCapital));

            return lstCuentas;
        }

        // ---------------------------------------------------------------
        // MODO HISTÓRICO
        // ---------------------------------------------------------------
        public List<Cls_CuentaContable> Fun_Obtener_Cuentas_Por_Nivel_Historico(int iNivel, int iAnio, int iMes)
        {
            // 1. Obtener todas las cuentas del histórico
            DataTable dtsCuentas = gDao.Fun_Consultar_SaldosJerarquicos_Historico(iAnio, iMes);
            List<Cls_CuentaContable> lstCuentas = Fun_Procesar_Cuentas(dtsCuentas, iNivel);

            // 2. Agregar totales históricos
            var (deTotalActivo, deTotalPasivo, deTotalCapital) = Fun_Obtener_Totales_Balance_Historico(iAnio, iMes);
            lstCuentas.Add(Fun_Crear_Cuenta_Total(deTotalPasivo + deTotalCapital));

            return lstCuentas;
        }

        // ---------------------------------------------------------------
        // Procesamiento común (para actual e histórico)
        // ---------------------------------------------------------------
        private List<Cls_CuentaContable> Fun_Procesar_Cuentas(DataTable dtsCuentas, int iNivel)
        {
            List<Cls_CuentaContable> lstCuentas = new List<Cls_CuentaContable>();

            foreach (DataRow drFila in dtsCuentas.Rows)
            {
                string sCodigo = drFila["Codigo"].ToString();
                int iPuntos = sCodigo.Count(c => c == '.');

                if ((iNivel == 1 && iPuntos == 0) ||
                    (iNivel == 2 && iPuntos <= 1) ||
                    (iNivel == 3))
                {
                    lstCuentas.Add(new Cls_CuentaContable
                    {
                        sCodigo = sCodigo,
                        sNombre = drFila["Nombre"].ToString(),
                        iTipo = Convert.ToInt32(drFila["Tipo"]),
                        iNaturaleza = Convert.ToInt32(drFila["Naturaleza"]),
                        deSaldoActual = Convert.ToDecimal(drFila["SALDO_ACTUAL"]),
                        sCodigoMadre = ObtenerCodigoMadre(sCodigo)
                    });
                }
            }

            return lstCuentas;
        }

        // ---------------------------------------------------------------
        // Totales (actual e histórico)
        // ---------------------------------------------------------------
        public (decimal deTotalActivo, decimal deTotalPasivo, decimal deTotalCapital) Fun_Obtener_Totales_Balance()
        {
            DataTable dtsCuentas = gDao.Fun_Consultar_SaldosJerarquicos();
            return Fun_Calcular_Totales(dtsCuentas);
        }

        public (decimal deTotalActivo, decimal deTotalPasivo, decimal deTotalCapital) Fun_Obtener_Totales_Balance_Historico(int iAnio, int iMes)
        {
            DataTable dtsCuentas = gDao.Fun_Consultar_SaldosJerarquicos_Historico(iAnio, iMes);
            return Fun_Calcular_Totales(dtsCuentas);
        }

        // ---------------------------------------------------------------
        // Funciones de apoyo
        // ---------------------------------------------------------------
        private (decimal, decimal, decimal) Fun_Calcular_Totales(DataTable dtsCuentas)
        {
            decimal deTotalActivo = 0, deTotalPasivo = 0, deTotalCapital = 0;

            foreach (DataRow drFila in dtsCuentas.Rows)
            {
                string sCodigo = drFila["Codigo"].ToString();
                decimal deSaldo = Convert.ToDecimal(drFila["SALDO_ACTUAL"]);

                if (sCodigo == "1") deTotalActivo = deSaldo;
                else if (sCodigo == "2") deTotalPasivo = deSaldo;
                else if (sCodigo == "3") deTotalCapital = deSaldo;
            }

            return (deTotalActivo, deTotalPasivo, deTotalCapital);
        }

        private Cls_CuentaContable Fun_Crear_Cuenta_Total(decimal deSaldo)
        {
            return new Cls_CuentaContable
            {
                sCodigo = "",
                sNombre = "TOTAL PASIVO + CAPITAL",
                iTipo = 0,
                iNaturaleza = 0,
                deSaldoActual = deSaldo,
                sCodigoMadre = null,
                EsVirtual = true
            };
        }

        private string ObtenerCodigoMadre(string sCodigo)
        {
            int iUltimoPunto = sCodigo.LastIndexOf('.');
            return iUltimoPunto > 0 ? sCodigo.Substring(0, iUltimoPunto) : null;
        }
    }
}

// Fin del código de Arón Ricardo Esquit Silva
// =====================================================================================
