using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Modelo_Polizas
{
    public class Cls_SentenciasSQL
    {

        //seleccionar ultimo ID
        public string sSeleccionarUltimoID = @"
            SELECT IFNULL(MAX(Pk_EncCodigo_Poliza), 0)
            FROM Tbl_EncabezadoPoliza
            WHERE YEAR(Pk_Fecha_Poliza) = YEAR(CURDATE())
              AND MONTH(Pk_Fecha_Poliza) = MONTH(CURDATE());";

        //obtener siguiente para metodo de modulo externo
        public string sObtenerSiguienteIdEncabezado = @"
            SELECT IFNULL(MAX(Pk_EncCodigo_Poliza), 0) + 1
            FROM Tbl_EncabezadoPoliza
            WHERE Pk_Fecha_Poliza = ?;";


        //Encabezado

        // Insertar encabezado
        public string sInsertarEncabezado = @"
            INSERT INTO Tbl_EncabezadoPoliza 
            (Pk_EncCodigo_Poliza, Pk_Fecha_Poliza, Cmp_Concepto_Poliza, Cmp_Valor_Poliza, Cmp_Estado_Poliza)
            VALUES (?, ?, ?, 0, 1);";

        // Actualizar encabezado sin cambiar fecha
        public string sActualizarEncabezado = @"
            UPDATE Tbl_EncabezadoPoliza
            SET Cmp_Concepto_Poliza = ?
            WHERE Pk_EncCodigo_Poliza = ? AND Pk_Fecha_Poliza = ?;";

        // Eliminar encabezado 
        public string sEliminarEncabezado = @"
            DELETE FROM Tbl_EncabezadoPoliza 
            WHERE Pk_EncCodigo_Poliza = ? AND Pk_Fecha_Poliza = ?;";

        // Consultar encabezados
        public string sConsultarEncabezados = @"
        SELECT 
            Pk_EncCodigo_Poliza AS Codigo,
            Pk_Fecha_Poliza AS Fecha,
            Cmp_Concepto_Poliza AS Concepto,
            Cmp_Valor_Poliza AS Valor,
            Cmp_Estado_Poliza AS Estado
        FROM Tbl_EncabezadoPoliza
        ORDER BY Pk_Fecha_Poliza DESC, Pk_EncCodigo_Poliza DESC;";

        // Actualizar total del encabezado segun detalles
        public string sActualizarTotalEncabezado = @"
        UPDATE Tbl_EncabezadoPoliza
        SET Cmp_Valor_Poliza = (
            SELECT IFNULL(SUM(Cmp_Valor_Poliza), 0)
            FROM Tbl_DetallePoliza
            WHERE PkFk_EncCodigo_Poliza = ? AND PkFk_Fecha_Poliza = ?
        )
        WHERE Pk_EncCodigo_Poliza = ? AND Pk_Fecha_Poliza = ?;";


        // Detalle de polizas

        // Insertar detalle con fecha
        public string sInsertarDetalle = @"
        INSERT INTO Tbl_DetallePoliza
        (PkFk_EncCodigo_Poliza, PkFk_Fecha_Poliza, PkFk_Codigo_Cuenta, Cmp_Tipo_Poliza, Cmp_Valor_Poliza)
        VALUES (?, ?, ?, ?, ?);";

        // Eliminar detalle individual
        public string sEliminarDetalle = @"
        DELETE FROM Tbl_DetallePoliza
        WHERE PkFk_EncCodigo_Poliza = ? AND PkFk_Fecha_Poliza = ? AND PkFk_Codigo_Cuenta = ?;";

        // Eliminar todos los detalles de una póliza
        public string sEliminarDetallesDePoliza = @"
        DELETE FROM Tbl_DetallePoliza
        WHERE PkFk_EncCodigo_Poliza = ? AND PkFk_Fecha_Poliza = ?;";

        // Consultar detalles
        public string sConsultarDetalle = @"
        SELECT 
            d.PkFk_Codigo_Cuenta AS CodigoCuenta,
            c.Cmp_CtaNombre AS NombreCuenta,
            d.Cmp_Tipo_Poliza AS Tipo,
            d.Cmp_Valor_Poliza AS Valor
        FROM Tbl_DetallePoliza d
        INNER JOIN Tbl_Catalogo_Cuentas c 
            ON d.PkFk_Codigo_Cuenta = c.Pk_Codigo_Cuenta
        WHERE d.PkFk_EncCodigo_Poliza = ? AND d.PkFk_Fecha_Poliza = ?;";


        //consultar catalogo de cuentas

        public string sConsultarCuentasContables = @"
        SELECT Pk_Codigo_Cuenta, Cmp_CtaNombre
        FROM Tbl_Catalogo_Cuentas
        WHERE Cmp_CtaTipo = 1;";

        // totales de cargos y abonos por poliza

        public string sObtenerTotalCargos = @"
        SELECT IFNULL(SUM(Cmp_Valor_Poliza), 0)
        FROM Tbl_DetallePoliza
        WHERE PkFk_EncCodigo_Poliza = ? AND PkFk_Fecha_Poliza = ? AND Cmp_Tipo_Poliza = 1;";

        public string sObtenerTotalAbonos = @"
        SELECT IFNULL(SUM(Cmp_Valor_Poliza), 0)
        FROM Tbl_DetallePoliza
        WHERE PkFk_EncCodigo_Poliza = ? AND PkFk_Fecha_Poliza = ? AND Cmp_Tipo_Poliza = 0;";


        //actualizacion de saldos

        public string sResetearSaldos = @"
        UPDATE Tbl_Catalogo_Cuentas
        SET 
          Cmp_CtaCargoActual = 0,
          Cmp_CtaAbonoActual = 0,
          Cmp_CtaSaldoActual = Cmp_CtaSaldoInicial;";

        public string sActualizarSaldos = @"
        UPDATE Tbl_Catalogo_Cuentas c
        JOIN (
          SELECT PkFk_Codigo_Cuenta,
                 SUM(CASE WHEN Cmp_Tipo_Poliza = 1 THEN Cmp_Valor_Poliza ELSE 0 END) AS TotalCargos,
                 SUM(CASE WHEN Cmp_Tipo_Poliza = 0 THEN Cmp_Valor_Poliza ELSE 0 END) AS TotalAbonos
          FROM Tbl_DetallePoliza
          GROUP BY PkFk_Codigo_Cuenta
        ) d ON c.Pk_Codigo_Cuenta = d.PkFk_Codigo_Cuenta
        SET 
          c.Cmp_CtaCargoActual = d.TotalCargos,
          c.Cmp_CtaAbonoActual = d.TotalAbonos,
          c.Cmp_CtaSaldoActual =
            CASE 
              WHEN c.Cmp_CtaNaturaleza = 1 THEN (c.Cmp_CtaSaldoInicial + d.TotalCargos - d.TotalAbonos)
              ELSE (c.Cmp_CtaSaldoInicial - d.TotalCargos + d.TotalAbonos)
            END;";

        public string sPropagarSaldos = @"
        UPDATE Tbl_Catalogo_Cuentas madre
        JOIN (
            SELECT Cmp_CtaMadre, SUM(Cmp_CtaSaldoActual) AS SumaHijas
            FROM Tbl_Catalogo_Cuentas
            WHERE Cmp_CtaMadre IS NOT NULL
            GROUP BY Cmp_CtaMadre
        ) hijas ON madre.Pk_Codigo_Cuenta = hijas.Cmp_CtaMadre
        SET madre.Cmp_CtaSaldoActual = hijas.SumaHijas;";

        // cierre contable - anual o mensual -

        // Cierre mensual
        public string sCerrarMesContable = @"
        UPDATE Tbl_EncabezadoPoliza
        SET Cmp_Estado_Poliza = 0
        WHERE Pk_Fecha_Poliza <= ? 
          AND Cmp_Estado_Poliza = 1;";

        public string sCerrarAnioContable = @"
        UPDATE Tbl_EncabezadoPoliza
        SET Cmp_Estado_Poliza = 0
        WHERE YEAR(Pk_Fecha_Poliza) = YEAR(?)
          AND Cmp_Estado_Poliza = 1;";

        // Obtener periodo contable actual
        public string sObtenerPeriodoActual = @"
        SELECT 
            YEAR(CURDATE()) AS AnioActual,
            MONTH(CURDATE()) AS MesActual,
            DATE_FORMAT(CURDATE(), '%Y-%m') AS PeriodoTexto;";

        // Cambiar modo de operación batch o linea
        public string sActualizarModoOperacion = @"
            UPDATE Tbl_PeriodosContables
            SET Cmp_ModoActualizacion = ?
            WHERE Cmp_Estado = 1;";

          // Obtiene el modo actual (único período activo)
        public string sObtenerModoOperacion = @"
            SELECT Cmp_ModoActualizacion
            FROM Tbl_PeriodosContables
            WHERE Cmp_Estado = 1
            LIMIT 1;";

    }

}
