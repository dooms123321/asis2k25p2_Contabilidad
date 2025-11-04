// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   30/10/2025

using System;
using System.Data;
using System.Data.Odbc;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_FlujoDeEfectivo_Dao
    {
        private readonly Cls_Conexion gConexion = new Cls_Conexion();

        // Método para consultar el flujo de efectivo desde el balance de saldos
        public DataTable fun_consultar_flujo_efectivo()
        {
            DataTable dts_Flujo = new DataTable();

            try
            {
                // Conexión ODBC abierta
                using (OdbcConnection conn = gConexion.conexion())
                {
                    string sQuery = @"
                        SELECT 
                            b.Fk_Codigo_Cuenta AS Codigo,
                            b.Cmp_Nombre_Cuenta AS Nombre,
                            CASE
                                WHEN b.Fk_Codigo_Cuenta LIKE '1.1%' OR b.Fk_Codigo_Cuenta LIKE '1.2%'
                                     OR b.Fk_Codigo_Cuenta LIKE '4%' OR b.Fk_Codigo_Cuenta LIKE '5%' OR b.Fk_Codigo_Cuenta LIKE '6%' THEN 'Operativa'
                                WHEN b.Fk_Codigo_Cuenta LIKE '1.5%' THEN 'Inversión'
                                WHEN b.Fk_Codigo_Cuenta LIKE '2.2%' THEN 'Financiación'
                                ELSE 'No clasificada'
                            END AS TipoActividad,
                            CASE 
                                WHEN b.Cmp_Saldo > 0 THEN b.Cmp_Saldo ELSE 0 END AS Entrada,
                            CASE 
                                WHEN b.Cmp_Saldo < 0 THEN ABS(b.Cmp_Saldo) ELSE 0 END AS Salida
                        FROM Tbl_Balance_Saldos b
                        WHERE b.Fk_Codigo_Cuenta LIKE '1%' 
                           OR b.Fk_Codigo_Cuenta LIKE '2%' 
                           OR b.Fk_Codigo_Cuenta LIKE '4%' 
                           OR b.Fk_Codigo_Cuenta LIKE '5%' 
                           OR b.Fk_Codigo_Cuenta LIKE '6%'
                        ORDER BY b.Fk_Codigo_Cuenta;
                    ";

                    OdbcDataAdapter adapter = new OdbcDataAdapter(sQuery, conn);
                    adapter.Fill(dts_Flujo);
                }
            }
            catch (OdbcException ex)
            {
                Console.WriteLine("Error en la conexión o consulta SQL del flujo de efectivo: " + ex.Message);
            }

            return dts_Flujo;
        }
    }
}
