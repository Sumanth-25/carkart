create database loginSignup
use loginSignup

create table signup(Id int identity(1,1) primary key,name varchar(20), email varchar(50), phoneNumber bigint, password varchar(20));
	

delete from signup;


alter procedure Sp_signup
(
@Id int=null,
@name varchar(20)=null,
@email varchar(50)=null,
@phoneNumber bigint=null,
@password varchar(20)=null,
@Action int
)
AS BEGIN
IF (@Action=1)
BEGIN
INSERT INTO signup VALUES(@name,@email,@phoneNumber,@password)
END
IF(@Action=2)
BEGIN
UPDATE signup SET name=@name, email=@email, phoneNumber=@phoneNumber, password=@password where Id=@Id
END
END

create procedure Sp_getsignup 
(@email nvarchar(50))  
as  
begin  
select * from signup where email=@email  
end 


create table adminLogin (name varchar(20), email varchar(50), phoneNumber bigint, password varchar(20));

select * from adminLogin

alter procedure Sp_adminLogin
(
@name varchar(20)=null,
@email varchar(50)=null,
@phoneNumber bigint=null,
@password varchar(20)=null,
@status varchar(20)
)
AS BEGIN
IF @status='Insert'
BEGIN
INSERT INTO adminLogin VALUES(@name,@email,@phoneNumber,@password)
END
END

alter procedure Sp_getadminLogin
(@name varchar(20))  
as  
begin  
select * from adminLogin where name=@name  
end 


create table addcar (Id int identity(1,1) primary key,name varchar(20), CarName varchar(20), description varchar(200), images varchar(20),OrderCar bit null)

select * from addcar

alter procedure Sp_addcar
(
@Id int=null,
@name varchar(20)=null,
@CarName varchar(20)=null,
@description varchar(200)=null,
@images varchar(20)=null,
@OrderCar bit=null,
@Action int
)
AS BEGIN
IF (@Action=1)
BEGIN
INSERT INTO addcar VALUES(@name,@CarName,@description,@images,@OrderCar)
END
IF(@Action=2)
BEGIN
DELETE FROM addcar where Id=@Id
END
IF(@Action=3)
BEGIN
SELECT * FROM addcar
END
IF(@Action=4)
BEGIN
UPDATE addcar SET name=@name,CarName=@CarName,description=@description,OrderCar=@OrderCar where Id=@Id
END
END

select CarName,description from adminLogin join addcar on adminLogin.name = addcar.name where adminLogin.name= 'ram'

insert into adminLogin values('sumanth', 'sumanthtamizhan@gmail.com', 9600631713, 'Sumanth12')
insert into adminLogin values('sumanth12', 'sumanthtamizhan@gmail.com', 9600631714, 'Sumanth13')

insert into adminLogin values('sumanth123', 'sumanthtamizhan@gmail.com', 9600631714, 'Sumanth134')
insert into adminLogin values('sumanth123', 'sumanthtamizhan@gmail.com', 9600631714, 'Sumanth134')

insert into addcar values ('sumanth','bmw', 'good')

update addcar set OrderCar = 0


truncate table signup

truncate table adminLogin

truncate table addcar

select * from signup
select * from adminLogin
select * from addcar

