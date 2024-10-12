using GestionFlotas.model;
using GestionFlotas.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace GestionFlotas.business
{
	public class TbClienteSucursalBL
	{
		private readonly FlotasContext _db;
		public TbClienteSucursalBL(FlotasContext db)
		{
			_db = db;
		}
		public async Task<TbClienteSucursalModel> Obtener(int _TbClienteSucursalId)
		{
			var sucursal = await (from p in _db.TbClienteSucursal
							  where p.TbClienteSucursalId == _TbClienteSucursalId
							  select (new TbClienteSucursalModel
							  {
								  TbClienteSucursalId = p.TbClienteSucursalId,
								  TbClienteId = p.TbClienteId,
								  Nombre = p.Nombre.Trim(),
								  Direccion = p.Direccion,
								  TbComunaId = p.TbComunaId,
								  Activo = p.Activo,
								  ActivoString = p.Activo ? "SI" : "NO",
							  })).FirstOrDefaultAsync();


			return sucursal;
		}
		public IQueryable<TbClienteSucursalModel> ListarAsQuerable()
		{
			var sucursal = (from p in _db.TbClienteSucursal
						 select (new TbClienteSucursalModel
						 {
							 TbClienteSucursalId = p.TbClienteSucursalId,
							 TbClienteId = p.TbClienteId,
							 Nombre = p.Nombre.Trim(),
							 Direccion = p.Direccion,
							 TbComunaId = p.TbComunaId,
							 Activo = p.Activo,
							 ActivoString = p.Activo ? "SI" : "NO",
						 })).AsQueryable();

			return sucursal;
		}
		public async Task Guardar(TbClienteSucursalModel _TbClienteSucursal)
		{
			try
			{
				List<ErrorValidacionModel> validacionModelo = ValidadorModelBL.valida(_TbClienteSucursal);
				if (validacionModelo.Count > 0) throw new Exception(string.Join("<br/>", validacionModelo.Select(x => x.Mensaje)));

				TbClienteSucursal oClienteSucursal = null;
				if (_TbClienteSucursal.TbClienteSucursalId == 0)
				{
					oClienteSucursal = new TbClienteSucursal
					{
						TbClienteSucursalId = _TbClienteSucursal.TbClienteSucursalId,
						TbClienteId = _TbClienteSucursal.TbClienteId,
						Nombre = _TbClienteSucursal.Nombre,
						Direccion = _TbClienteSucursal.Direccion,
						TbComunaId = _TbClienteSucursal.TbComunaId,
						Activo = _TbClienteSucursal.Activo,
					};
					_db.Add(oClienteSucursal);
				}
				else
				{
					oClienteSucursal = await _db.TbClienteSucursal.Where(x => x.TbClienteSucursalId == _TbClienteSucursal.TbClienteSucursalId).FirstAsync();
					if (oClienteSucursal == null) throw new Exception($"Sucursal cliente no existe para el ID: {_TbClienteSucursal.TbClienteSucursalId}");

					oClienteSucursal.TbClienteSucursalId = _TbClienteSucursal.TbClienteSucursalId;
					oClienteSucursal.TbClienteId = _TbClienteSucursal.TbClienteId;
					oClienteSucursal.Nombre = _TbClienteSucursal.Nombre;
					oClienteSucursal.Direccion = _TbClienteSucursal.Direccion;
					oClienteSucursal.TbComunaId = _TbClienteSucursal.TbComunaId;
					oClienteSucursal.Activo = _TbClienteSucursal.Activo;

					_db.Update(oClienteSucursal);
				}
				await _db.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<int> Eliminar(int _TbClienteSucursalId)
		{
			try
			{
				return await _db.TbClienteSucursal.Where(x => x.TbClienteSucursalId == _TbClienteSucursalId).ExecuteDeleteAsync();

			}
			catch (Exception ex)
			{
				if (ex.Message.ToUpper().Contains("CONSTRAI"))
					throw new Exception("No se puede eliminar el registro tipo porque esta siendo utilizado en el sistema");
				else
					throw;
			}
		}
		public async Task<List<TbClienteSucursalModel>> ListarByClienteId(int _ClienteId)
		{
			var tipos = await (from p in _db.TbClienteSucursal
						 where p.TbClienteId == _ClienteId
						 select (new TbClienteSucursalModel
						 {
							 TbClienteSucursalId = p.TbClienteSucursalId,
							 TbClienteId = p.TbClienteId,
							 Nombre = p.Nombre.Trim(),
							 Direccion = p.Direccion,
							 TbComunaId = p.TbComunaId,	
							 Activo = p.Activo,
							 ActivoString = p.Activo ? "SI" : "NO",
							 NombreComuna = p.TbComuna.Nombre
						 })).ToListAsync();

			return tipos;
		}
	}
}
