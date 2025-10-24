using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Controlador_Seguridad;
using Capa_Modelo_Seguridad;


namespace Capa_Controlador_Navegador
{
    public class Cls_Privilegios_Seguridad
    {
        /// <summary>
        /// Obtiene permisos combinados (usuario + perfil) usando NOMBRES.
        /// Úsalo si desde el form conoces los nombres "Módulo" y "Aplicación".
        /// </summary>
        public Cls_Permiso_Aplicacion_Usuario VerificarPermisos(string nombreModulo, string nombreAplicacion)
        {
            var pu = new Cls_Permiso_Usuario();
            int idModulo = pu.ObtenerIdModuloPorNombre(nombreModulo);
            int idAplicacion = pu.ObtenerIdAplicacionPorNombre(nombreAplicacion);

            if (idModulo <= 0 || idAplicacion <= 0)
                return GetAllFalse();

            return VerificarPermisos(idAplicacion, idModulo);
        }

        /// <summary>
        /// Obtiene permisos combinados (usuario + perfil) usando IDs.
        /// Este es el que usa el navegador internamente.
        /// </summary>
        public Cls_Permiso_Aplicacion_Usuario VerificarPermisos(int idAplicacion, int idModulo)
        {
            int idUsuario = Capa_Modelo_Seguridad.Cls_Usuario_Conectado.iIdUsuario;

            // 2) Perfil del usuario (0 si no tiene)
            var ctrlUsuario = new Cls_Usuario_Controlador();
            int idPerfil = 0;
            try { idPerfil = ctrlUsuario.ObtenerIdPerfilDeUsuario(idUsuario); }
            catch { idPerfil = 0; }

            // 3) Combinar permisos (usuario OR perfil)
            var flags = Cls_Aplicacion_Permisos.ObtenerPermisosCombinados(idUsuario, idAplicacion, idModulo, idPerfil);

            // 4) Devolver en el DTO que usa todo el proyecto
            return new Cls_Permiso_Aplicacion_Usuario
            {
                Fk_Id_Usuario = idUsuario,
                FK_Id_Aplicacion = idAplicacion,
                Cmp_Ingresar_Permiso_Aplicacion_Usuario = flags.ingresar,
                Cmp_Consultar_Permiso_Aplicacion_Usuario = flags.consultar,
                Cmp_Modificar_Permiso_Aplicacion_Usuario = flags.modificar,
                Cmp_Eliminar_Permiso_Aplicacion_Usuario = flags.eliminar,
                Cmp_Imprimir_Permiso_Aplicacion_Usuario = flags.imprimir
            };
        }

        private Cls_Permiso_Aplicacion_Usuario GetAllFalse()
        {
            return new Cls_Permiso_Aplicacion_Usuario
            {
                Cmp_Ingresar_Permiso_Aplicacion_Usuario = false,
                Cmp_Consultar_Permiso_Aplicacion_Usuario = false,
                Cmp_Modificar_Permiso_Aplicacion_Usuario = false,
                Cmp_Eliminar_Permiso_Aplicacion_Usuario = false,
                Cmp_Imprimir_Permiso_Aplicacion_Usuario = false
            };
        }


    }
}
