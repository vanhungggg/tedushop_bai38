select r.Name from ApplicationUserRoles ur
inner join ApplicationRoles r
on r.Id=ur.RoleId
inner join ApplicationUsers u
on ur.UserId=u.Id
where u.UserName='tedu'

select * from ApplicationRoles
where Id='144da971-ce11-4bca-9d01-f9c6007ba815'