using GestionFlotas.model;
using GestionFlotas.dataaccess;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace GestionFlotas.business
{
	public class TbBencineraEmpresaBL
	{
		private readonly FlotasContext _db;
		public TbBencineraEmpresaBL(FlotasContext db)
		{
			_db = db;
		}
		public async Task<TbBencineraEmpresaModel> Obtener(int _TbBencineraEmpresaId)
		{
			var bencineraEmpresa = await (from p in _db.TbBencineraEmpresa
								where p.TbBencineraEmpresaId == _TbBencineraEmpresaId
								select (new TbBencineraEmpresaModel
								{
									TbBencineraEmpresaId = p.TbBencineraEmpresaId,
									Rut = p.Rut,
									Digito = p.Digito,
									Nombre = p.Nombre.Trim(),
									RazonSocial = p.RazonSocial,
									Activo = p.Activo,
									ActivoString = p.Activo ? "SI" : "NO",
									RutFormatado = string.Format("{0}-{1}", p.Rut, p.Digito)
								})).FirstOrDefaultAsync();


			return bencineraEmpresa;
		}
		public IQueryable<TbBencineraEmpresaModel> ListarAsQuerable()
		{
			var bencineraEmpresas = (from p in _db.TbBencineraEmpresa
						   select (new TbBencineraEmpresaModel
						   {
							   TbBencineraEmpresaId = p.TbBencineraEmpresaId,
							   Rut = p.Rut,
							   Digito = p.Digito,
							   Nombre = p.Nombre.Trim(),
							   RazonSocial = p.RazonSocial,
							   Activo = p.Activo,
							   ActivoString = p.Activo ? "SI" : "NO",
							   RutFormatado = string.Format("{0}-{1}", p.Rut, p.Digito)
						   })).AsQueryable();

			return bencineraEmpresas;
		}
		public async Task<List<TbBencineraEmpresaModel>> ListarByDDL()
		{
			var bencineraEmpresas = await (from p in _db.TbBencineraEmpresa
								 where p.Activo
								 select (new TbBencineraEmpresaModel
								 {
									 TbBencineraEmpresaId = p.TbBencineraEmpresaId,
									 Nombre = p.Nombre.Trim(),
								 })).ToListAsync();

			return bencineraEmpresas;
		}
		public async Task Guardar(TbBencineraEmpresaModel _TbBencineraEmpresa)
		{
			try
			{
				//List<ErrorValidacionModel> validacionModelo = ValidadorModelBL.valida(_TbBencineraEmpresa);
				//if (validacionModelo.Count > 0) throw new Exception(string.Join("<br/>", validacionModelo.Select(x => x.Mensaje)));

				TbBencineraEmpresa oBencineraEmpresa = null;
				if (_TbBencineraEmpresa.TbBencineraEmpresaId == 0)
				{
					oBencineraEmpresa = new TbBencineraEmpresa
					{
						Rut = _TbBencineraEmpresa.Rut,
						Digito = _TbBencineraEmpresa.Digito,
						Nombre = _TbBencineraEmpresa.Nombre,
						RazonSocial = _TbBencineraEmpresa.RazonSocial,
						Activo = _TbBencineraEmpresa.Activo,
					};
					_db.Add(oBencineraEmpresa);
				}
				else
				{
					oBencineraEmpresa = await _db.TbBencineraEmpresa.Where(x => x.TbBencineraEmpresaId == _TbBencineraEmpresa.TbBencineraEmpresaId).FirstAsync();
					if (oBencineraEmpresa == null) throw new Exception($"Empresa bencinera no existe para el ID: {_TbBencineraEmpresa.TbBencineraEmpresaId}");

					oBencineraEmpresa.Rut = _TbBencineraEmpresa.Rut;
					oBencineraEmpresa.Digito = _TbBencineraEmpresa.Digito;
					oBencineraEmpresa.Nombre = _TbBencineraEmpresa.Nombre;
					oBencineraEmpresa.RazonSocial = _TbBencineraEmpresa.RazonSocial;
					oBencineraEmpresa.Activo = _TbBencineraEmpresa.Activo;

					_db.Update(oBencineraEmpresa);
				}
				await _db.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<int> Eliminar(int _TbBencineraEmpresaId)
		{
			try
			{
				return await _db.TbBencineraEmpresa.Where(x => x.TbBencineraEmpresaId == _TbBencineraEmpresaId).ExecuteDeleteAsync();

			}
			catch (Exception ex)
			{
				if (ex.Message.ToUpper().Contains("CONSTRAI"))
					throw new Exception("No se puede eliminar el registro empresa bencinera porque esta siendo utilizado en el sistema");
				else
					throw;
			}
		}
	}
}
