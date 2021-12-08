
--create trigger tgr_asientos on facturacion
--after insert, update
--as
--begin
--	declare @factura int = (select id from inserted);
--	if(select idasiento from inserted) = 0
--	begin
--		update facturacion set idasiento = null where id = @factura;
--	end
--end

--alter view vFacturacion
--as
--Select
--	f.Id,
--	f.Comentario,
--	cast(f.fecha as date) Fecha,
--	(f.cantidad * f.PrecioUnitario) Monto,
--	f.IdAsiento
--From 
--	Facturacion f
--Where f.IdAsiento is null;

select * from vFacturacion;

--CONVERT(varchar, '2017-08-25', 101);