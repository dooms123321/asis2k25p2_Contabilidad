// =====================================================================================
// Autor: Arón Ricardo Esquit Silva
// Carné: 0901-22-13036
// Fecha: 10/11/2025
// Descripción: Puente entre modelo y vista para el Balance General (actual e histórico)
// =====================================================================================

using System;
using System.Collections.Generic;
using Capa_Modelo_Estados_Financieros;

namespace Capa_Controlador_Estados_Financieros
{
    public class Cls_BalanceGeneral_Controlador
    {
        private readonly Cls_BalanceGeneral_Calculo gCalculo = new Cls_BalanceGeneral_Calculo();

        // -------------------------------------------------------------------------
        // Estructura visual de cuenta
        // -------------------------------------------------------------------------
        public class Cls_CuentaVisual
        {
            public string sCuenta { get; set; }
            public string sNombre { get; set; }
            public string sDebe { get; set; }
            public string sHaber { get; set; }
            public bool EsVirtual { get; set; } = false;
            public decimal deSaldoOriginal { get; set; }
            public int iTipo { get; set; } // 0 = agrupadora, 1 = movimiento
            public int iNaturaleza { get; set; } // 1 = deudora, 0 = acreedora
            public int iNivel { get; set; } // 1, 2, 3, etc.
        }

        // -------------------------------------------------------------------------
        // ==========================  MODO ACTUAL  ================================
        // -------------------------------------------------------------------------
        public List<Cls_CuentaVisual> Fun_Obtener_BalanceVisual(int iNivel)
        {
            try
            {
                var lstOriginal = gCalculo.Fun_Obtener_Cuentas_Por_Nivel(iNivel);
                return Fun_Formatear_Cuentas(lstOriginal);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al construir balance visual: " + ex.Message);
                return new List<Cls_CuentaVisual>();
            }
        }

        public (decimal deTotalActivo, decimal deTotalPasivo, decimal deTotalCapital) Fun_Obtener_Totales_Balance()
        {
            try
            {
                return gCalculo.Fun_Obtener_Totales_Balance();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener totales del balance: " + ex.Message);
                return (0, 0, 0);
            }
        }

        // -------------------------------------------------------------------------
        // ==========================  MODO HISTÓRICO  =============================
        // -------------------------------------------------------------------------
        public List<Cls_CuentaVisual> Fun_Obtener_BalanceVisual_Historico(int iNivel, int iAnio, int iMes)
        {
            try
            {
                var lstOriginal = gCalculo.Fun_Obtener_Cuentas_Por_Nivel_Historico(iNivel, iAnio, iMes);
                return Fun_Formatear_Cuentas(lstOriginal);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al construir balance visual histórico: " + ex.Message);
                return new List<Cls_CuentaVisual>();
            }
        }

        public (decimal deTotalActivo, decimal deTotalPasivo, decimal deTotalCapital) Fun_Obtener_Totales_Balance_Historico(int iAnio, int iMes)
        {
            try
            {
                return gCalculo.Fun_Obtener_Totales_Balance_Historico(iAnio, iMes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener totales del balance histórico: " + ex.Message);
                return (0, 0, 0);
            }
        }

        // -------------------------------------------------------------------------
        // Formateo común para ambos modos
        // -------------------------------------------------------------------------
        private List<Cls_CuentaVisual> Fun_Formatear_Cuentas(List<Cls_BalanceGeneral_Calculo.Cls_CuentaContable> lstOriginal)
        {
            var lstVisual = new List<Cls_CuentaVisual>();

            foreach (var cuenta in lstOriginal)
            {
                lstVisual.Add(new Cls_CuentaVisual
                {
                    sCuenta = cuenta.sCodigo,
                    sNombre = cuenta.sNombre,
                    sDebe = FormatearDebe(cuenta.deDebe, cuenta.EsVirtual, cuenta.iTipo, cuenta.iNaturaleza, cuenta.iNivel),
                    sHaber = FormatearHaber(cuenta.deHaber, cuenta.EsVirtual, cuenta.iTipo, cuenta.iNaturaleza, cuenta.iNivel),
                    EsVirtual = cuenta.EsVirtual,
                    deSaldoOriginal = cuenta.deSaldoActual,
                    iTipo = cuenta.iTipo,
                    iNaturaleza = cuenta.iNaturaleza,
                    iNivel = cuenta.iNivel
                });
            }

            return lstVisual;
        }

        // -------------------------------------------------------------------------
        // Formato de columna DEBE
        // -------------------------------------------------------------------------
        private string FormatearDebe(decimal monto, bool esVirtual, int iTipo, int iNaturaleza, int iNivel)
        {
            if (esVirtual && monto == 0) return "";
            if (iNaturaleza == 1)
            {
                if (iTipo == 1 && monto == 0) return "Q0.00";
                if (iTipo == 0 && monto == 0 && iNivel >= 2) return "Q0.00";
                if (iTipo == 0 && monto == 0 && iNivel == 1) return "";
                return monto != 0 ? $"Q {monto:N2}" : "";
            }
            return "";
        }

        // -------------------------------------------------------------------------
        // Formato de columna HABER
        // -------------------------------------------------------------------------
        private string FormatearHaber(decimal monto, bool esVirtual, int iTipo, int iNaturaleza, int iNivel)
        {
            if (esVirtual && monto == 0) return "";
            if (iNaturaleza == 0)
            {
                if (iTipo == 1 && monto == 0) return "Q0.00";
                if (iTipo == 0 && monto == 0 && iNivel >= 2) return "Q0.00";
                if (iTipo == 0 && monto == 0 && iNivel == 1) return "";
                return monto != 0 ? $"Q {monto:N2}" : "";
            }
            return "";
        }
    }
}

// Fin del código de Arón Ricardo Esquit Silva
// =====================================================================================
