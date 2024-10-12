using GestionFlotas.model;
using GestionFlotas.dataaccess;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace GestionFlotas.business
{
	public class TbClienteBL
	{
		private readonly FlotasContext _db;
		public TbClienteBL(FlotasContext db)
		{
			_db = db;
		}
		public async Task<TbClienteModel> Obtener(int _TbClienteId)
		{
			var cliente = await (from p in _db.TbCliente
									 where p.TbClienteId == _TbClienteId
									 select (new TbClienteModel
									 {
										 TbClienteId = p.TbClienteId,
										 Rut = p.Rut,
										 Digito = p.Digito,
										 Nombre = p.Nombre.Trim(),
										 RazonSocial = p.RazonSocial,
										 Activo = p.Activo,
										 ActivoString = p.Activo ? "SI" : "NO",
										 RutFormatado = string.Format("{0}-{1}", p.Rut, p.Digito)
									 })).FirstOrDefaultAsync();


			return cliente;
		}
		public async Task<TbClienteModel> ObtenerConSucursal(int _TbClienteId)
		{
			var cliente = await (from p in _db.TbCliente
								 where p.TbClienteId == _TbClienteId
								 select (new TbClienteModel
								 {
									 TbClienteId = p.TbClienteId,
									 Rut = p.Rut,
									 Digito = p.Digito,
									 Nombre = p.Nombre.Trim(),
									 RazonSocial = p.RazonSocial,
									 Activo = p.Activo,
									 ActivoString = p.Activo ? "SI" : "NO",
									 RutFormatado = string.Format("{0}-{1}", p.Rut, p.Digito)
								 })).FirstOrDefaultAsync();

			cliente.MisSucursales = await new TbClienteSucursalBL(_db).ListarByClienteId(_TbClienteId);
			return cliente;
		}
		public IQueryable<TbClienteModel> ListarAsQuerable()
		{
			var cliente = (from p in _db.TbCliente
								select (new TbClienteModel
								{
									TbClienteId = p.TbClienteId,
									Rut = p.Rut,
									Digito = p.Digito,
									Nombre = p.Nombre.Trim(),
									RazonSocial = p.RazonSocial,
									Activo = p.Activo,
									ActivoString = p.Activo ? "SI" : "NO",
									RutFormatado = string.Format("{0}-{1}", p.Rut, p.Digito)
								})).AsQueryable();

			return cliente;
		}
		public async Task<List<TbClienteModel>> ListarByDDL()
		{
			var cliente = await (from p in _db.TbCliente
									  where p.Activo
									  select (new TbClienteModel
									  {
										  TbClienteId = p.TbClienteId,
										  Nombre = p.Nombre.Trim(),
									  })).ToListAsync();

			return cliente;
		}
		public async Task Guardar(TbClienteModel _TbCliente)
		{
			try
			{
				//List<ErrorValidacionModel> validacionModelo = ValidadorModelBL.valida(_TbCliente);
				//if (validacionModelo.Count > 0) throw new Exception(string.Join("<br/>", validacionModelo.Select(x => x.Mensaje)));

				TbCliente oCliente = null;
				if (_TbCliente.TbClienteId == 0)
				{
					oCliente = new TbCliente
					{
						Rut = _TbCliente.Rut,
						Digito = _TbCliente.Digito,
						Nombre = _TbCliente.Nombre,
						RazonSocial = _TbCliente.RazonSocial,
						Activo = _TbCliente.Activo,
					};
					_db.Add(oCliente);
				}
				else
				{
					oCliente = await _db.TbCliente.Where(x => x.TbClienteId == _TbCliente.TbClienteId).FirstAsync();
					if (oCliente == null) throw new Exception($"Cliente no existe para el ID: {_TbCliente.TbClienteId}");

					oCliente.Rut = _TbCliente.Rut;
					oCliente.Digito = _TbCliente.Digito;
					oCliente.Nombre = _TbCliente.Nombre;
					oCliente.RazonSocial = _TbCliente.RazonSocial;
					oCliente.Activo = _TbCliente.Activo;

					_db.Update(oCliente);
				}
				await _db.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<int> Eliminar(int _TbClienteId)
		{
			try
			{
				return await _db.TbCliente.Where(x => x.TbClienteId == _TbClienteId).ExecuteDeleteAsync();

			}
			catch (Exception ex)
			{
				if (ex.Message.ToUpper().Contains("CONSTRAI"))
					throw new Exception("No se puede eliminar el registro cliente porque esta siendo utilizado en el sistema");
				else
					throw;
			}
		}
	}
}
