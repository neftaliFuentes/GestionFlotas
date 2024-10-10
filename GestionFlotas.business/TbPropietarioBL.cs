using GestionFlotas.model;
using GestionFlotas.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace GestionFlotas.business
{
	public class TbPropietarioBL
	{
		private readonly FlotasContext _db;
		public TbPropietarioBL(FlotasContext db)
		{
			_db = db;
		}
		public async Task<TbPropietarioModel> Obtener(int _TbPropietarioId)
		{
			var propietario = await (from p in _db.TbPropietario
										  where p.TbPropietarioId == _TbPropietarioId
										  select (new TbPropietarioModel
										  {
											  TbPropietarioId = p.TbPropietarioId,
											  Rut = p.Rut,
											  Digito = p.Digito,
											  Nombre = p.Nombre.Trim(),
											  RazonSocial = p.RazonSocial,
											  Activo = p.Activo,
											  ActivoString = p.Activo ? "SI" : "NO",
											  RutFormatado = string.Format("{0}-{1}", p.Rut, p.Digito)
										  })).FirstOrDefaultAsync();


			return propietario;
		}
		public IQueryable<TbPropietarioModel> ListarAsQuerable()
		{
			var propietarios = (from p in _db.TbPropietario
									 select (new TbPropietarioModel
									 {
										 TbPropietarioId = p.TbPropietarioId,
										 Rut = p.Rut,
										 Digito = p.Digito,
										 Nombre = p.Nombre.Trim(),
										 RazonSocial = p.RazonSocial,
										 Activo = p.Activo,
										 ActivoString = p.Activo ? "SI" : "NO",
										 RutFormatado = string.Format("{0}-{1}", p.Rut, p.Digito)
									 })).AsQueryable();

			return propietarios;
		}
		public async Task<List<TbPropietarioModel>> ListarByDDL()
		{
			var propietarios = await (from p in _db.TbPropietario
										   where p.Activo
										   select (new TbPropietarioModel
										   {
											   TbPropietarioId = p.TbPropietarioId,
											   Nombre = p.Nombre.Trim(),
										   })).ToListAsync();

			return propietarios;
		}
		public async Task Guardar(TbPropietarioModel _TbPropietario)
		{
			try
			{
				//List<ErrorValidacionModel> validacionModelo = ValidadorModelBL.valida(_TbPropietario);
				//if (validacionModelo.Count > 0) throw new Exception(string.Join("<br/>", validacionModelo.Select(x => x.Mensaje)));

				TbPropietario oPropietario = null;
				if (_TbPropietario.TbPropietarioId == 0)
				{
					oPropietario = new TbPropietario
					{
						Rut = _TbPropietario.Rut,
						Digito = _TbPropietario.Digito,
						Nombre = _TbPropietario.Nombre,
						RazonSocial = _TbPropietario.RazonSocial,
						Activo = _TbPropietario.Activo,
					};
					_db.Add(oPropietario);
				}
				else
				{
					oPropietario = await _db.TbPropietario.Where(x => x.TbPropietarioId == _TbPropietario.TbPropietarioId).FirstAsync();
					if (oPropietario == null) throw new Exception($"Propietario no existe para el ID: {_TbPropietario.TbPropietarioId}");

					oPropietario.Rut = _TbPropietario.Rut;
					oPropietario.Digito = _TbPropietario.Digito;
					oPropietario.Nombre = _TbPropietario.Nombre;
					oPropietario.RazonSocial = _TbPropietario.RazonSocial;
					oPropietario.Activo = _TbPropietario.Activo;

					_db.Update(oPropietario);
				}
				await _db.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<int> Eliminar(int _TbPropietarioId)
		{
			try
			{
				return await _db.TbPropietario.Where(x => x.TbPropietarioId == _TbPropietarioId).ExecuteDeleteAsync();

			}
			catch (Exception ex)
			{
				if (ex.Message.ToUpper().Contains("CONSTRAI"))
					throw new Exception("No se puede eliminar el registro propietario porque esta siendo utilizado en el sistema");
				else
					throw;
			}
		}
	}
}
