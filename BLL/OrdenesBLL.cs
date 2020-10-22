using Microsoft.EntityFrameworkCore;
using RegistroPedidos.DAL;
using RegistroPedidos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace RegistroPedidos.BLL
{
    class OrdenesBLL
    {
        public static bool Guardar(Ordenes orden)
        {
            if (!Existe(orden.OrdenId))
                return Insertar(orden);
            else
                return Modificar(orden);
        }

        private static bool Insertar(Ordenes orden)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Ordenes.Add(orden);
                paso = contexto.SaveChanges() > 0;
                
                Productos producto;
                List<OrdenesDetalle> detalle = orden.Detalle;
                foreach (OrdenesDetalle m in detalle)
                {
                    producto = ProductosBLL.Buscar(m.ProductoId);
                    producto.Inventario += m.Cantidad;
                    ProductosBLL.Guardar(producto);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        private static bool Modificar(Ordenes orden)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                Productos producto;
                List<OrdenesDetalle> detalle = Buscar(orden.OrdenId).Detalle;
                foreach (OrdenesDetalle m in detalle)
                {
                    producto = ProductosBLL.Buscar(m.ProductoId);
                    producto.Inventario -= m.Cantidad;
                    ProductosBLL.Guardar(producto);
                }
                contexto.Database.ExecuteSqlRaw($"Delete FROM OrdenesDetalle Where OrdenId={orden.OrdenId}");
                foreach (var item in orden.Detalle)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }

                List<OrdenesDetalle> nuevo = orden.Detalle;
                foreach (OrdenesDetalle m in nuevo)
                {
                    producto = ProductosBLL.Buscar(m.ProductoId);
                    producto.Inventario += m.Cantidad;
                    ProductosBLL.Guardar(producto);
                }

                contexto.Entry(orden).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var orden = OrdenesBLL.Buscar(id);
                Productos producto;
                List<OrdenesDetalle> viejosDetalles = Buscar(orden.OrdenId).Detalle;
                foreach (OrdenesDetalle d in viejosDetalles)
                {
                    producto = ProductosBLL.Buscar(d.ProductoId);
                    producto.Inventario -= d.Cantidad;
                    ProductosBLL.Guardar(producto);
                }
                if (orden != null)
                {
                    contexto.Entry(orden).State = EntityState.Deleted;
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static Ordenes Buscar(int id)
        {
            Ordenes orden = new Ordenes();
            Contexto contexto = new Contexto();

            try
            {
                orden = contexto.Ordenes.Include(x => x.Detalle)
                    .Where(x => x.OrdenId == id)
                    .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return orden;
        }

        public static List<Ordenes> GetList(Expression<Func<Ordenes, bool>> criterio)
        {
            List<Ordenes> Lista = new List<Ordenes>();
            Contexto contexto = new Contexto();

            try
            {
                Lista = contexto.Ordenes.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Lista;
        }
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Ordenes.Any(e => e.OrdenId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return encontrado;
        }
    }
}
