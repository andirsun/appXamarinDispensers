using DispensadoresApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispensadoresApp.Servicios.BaseDatos
{
    public class UsuarioDB
    {
        private readonly BaseDeDatos bd;

        public UsuarioDB() : base()
        {
            //bd = App.dataBase;
        }

        public UsuarioModelo BuscarUsuarioActual()
        {
            UsuarioModelo usuario = bd.Set<UsuarioModelo>().FirstOrDefault();
            return usuario;
        }

        public async Task<Boolean> AgregarUsuario(UsuarioModelo usuario)
        {
            try
            {
                await bd.Set<UsuarioModelo>().AddAsync(usuario);
                await bd.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<Boolean> BorrarUsuario(UsuarioModelo cliente)
        {
            try
            {
                bd.Remove(cliente);
                await bd.SaveChangesAsync();
                return true;
            }

            catch (Exception e)
            {
                return false;
            }
        }
    }
}
